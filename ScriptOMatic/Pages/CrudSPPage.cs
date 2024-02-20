using ScriptOMatic.Generate.Extensions;
using SupplyChain.Dto;
using SupplyChain.Dto.Extensions;
using SupplyChain.Procs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Enums;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Strings;
using UtilClasses.Winforms.Extensions;

namespace ScriptOMatic.Pages
{
    public partial class CrudSpPage : TableSelectPage
    {
        public Dictionary<string, Bundle> Bundles { get; set; }

        private bool _updatingChecks;

        private Dictionary<ListView, ListViewHandler> _listViewHandlers;
        public event Action<PopulatedBundle> NewContent;

        public CrudSpPage()
        {
            InitializeComponent();
            TableChanged += OnTableChanged;
            //_checkBoxes = new List<CheckBox> { chkCreate, chkRead, chkUpdate, chkDelete, chkUpsert, chkIdIn };
            _listViewHandlers = new Dictionary<ListView, ListViewHandler>();
            EnvChanged += OnEnvChanged;
        }

        private void OnEnvChanged()
        {
            lvInterfaces.Items.Clear();
            if (null == Env) return;
            lvInterfaces.Items.AddRange(Env.Interfaces.Select(i => new ListViewItem(i.Name) { Tag = i }).ToArray());
        }

        private ListViewItem NewAggregateListViewItem(Aggregate a) => SetAggregateListViewItem(new ListViewItem(), a);
        private ListViewItem SetAggregateListViewItem(ListViewItem itm, Aggregate a)
        {
            var la = a as LinkedAggregate;
            itm.Text = la == null ? a.Table : la.Table + "*";
            if (itm.SubItems.Count < 2)
                itm.SubItems.Add("");
            itm.SubItems[1].Text = a.Alias;
            if (itm.SubItems.Count < 3)
                itm.SubItems.Add("");
            itm.SubItems[2].Text = a.Type.ToString();
            itm.Tag = a;
            itm.BackColor = a.ReadOnly ? Color.LightGray : SystemColors.Window;
            return itm;
        }
        private void OnTableChanged()
        {
            _updatingChecks = true;

            var bundle = Bundles.GetOrAdd(_tableName);

            bundle = bundle ?? new Bundle()
            {
                Table = new TableInfo()
                {
                    Name = _tableName,
                    Plural = ctxtPluralName.Text,
                    Singular = ctxtSingularName.Text
                }
            };

            var popBundle = bundle.Populate(Env, _rootNode);

            lvReadBetween.Items.Clear();
            lvReadFor.Items.Clear();
            lvAggregates.Items.Clear();
            lvAggregates.BeginUpdate();
            foreach (var ac in _rootNode.AggregateCandidates)
            {
                var existing = bundle.Aggregates.FirstOrDefault(a => a.Table.EqualsOic(ac.Table) && a.Type == ac.Type);
                var item = NewAggregateListViewItem(existing ?? ac);
                item.Checked = null != existing;
                lvAggregates.Items.Add(item);
            }
            lvAggregates.EndUpdate();

            SetCheckBoxes(popBundle);

            SetRWCHandler(lvMaxQueries, "GetForMax", false);
            SetRWCHandler(lvReadFor, "GetFor", true);
            SetRWCHandler(lvReadBetween, "GetBetween", true);
            SetRWCHandler(lvDelFor, "DelFor", false);
            SetJsonFieldHandler(lvJsonFields);

            lvHandCoded.Items.Clear();
            //_checkBoxes.Union(_jsonChecks).ForEach(chk => chk.Checked = false);
            var iDict = lvInterfaces.Items.Cast<ListViewItem>()
                .ForEach(itm => itm.Checked = false)
                .ToDictionary(itm => ((InterfaceDef)itm.Tag).FieldName);

            tlpOperations.Enabled = popBundle.LinkTable == null;


            foreach (var col in _columns)
            {
                var i = iDict.Maybe(col.Name);
                if (null == i) continue;
                i.Checked = true;
                ((InterfaceDef)i.Tag).Type = col.GetDtoType();
            }


            var dict = new Dictionary<string, CheckBox>(StringComparer.OrdinalIgnoreCase){
                { "Create", chkCreate },
                { "Get", chkRead },
                { "Update", chkUpdate },
                { "Delete", chkDelete },
                { "Upsert", chkUpsert } };

            lvMaxQueries.Items.Clear();
            lvReadFor.Items.Clear();
            lvReadBetween.Items.Clear();
            if (null != bundle)
            {
                var lvs = new[] { lvMaxQueries, lvReadFor, lvReadBetween, lvDelFor };
                foreach (var p in bundle.AllProcedures())
                {
                    var chk = dict.Maybe(p.CodeName);
                    if (null != chk)
                    {
                        chk.Checked = true;
                        continue;
                    }
                    lvs.ForEach(lv => MatchesListRwc(lv, p));
                }
                lvHandCoded.Items.AddRange(bundle.HandCoded.Select(p => new ListViewItem(p.Name)
                {
                    Checked = true,
                    Tag = p
                }));
            }
            lvMatchableColumns.Items.Clear();

            lvMatchableColumns.Items.AddRange(popBundle.Columns.Select(c => new ListViewItem(c.Name)));
            chkImplementMatchable.Checked = null != bundle.MatchColumns;
            if (bundle.MatchColumns != null)
            {
                foreach (var c in bundle.MatchColumns)
                {
                    lvMatchableColumns.Items
                        .Cast<ListViewItem>()
                        .Where(l => l.Text.EqualsOic(c))
                        .ForEach(l => l.Checked = true);
                }
            }

            _updatingChecks = false;
            SomethingChanged();
            Generate();
        }

        private void SetCheckBoxes(Bundle b)
        {
            _updatingChecks = true;
            var c = b.Procedures.Maybe(Bundle.SpType.Create);
            chkCreate.Checked = null != c;
            chkCreateInputJson.Checked = c?.InputJson ?? false;
            chkCreateOutputJson.Checked = c?.OutputJson ?? false;


            var r = b.Procedures.Maybe(Bundle.SpType.Read);
            chkRead.Checked = null != r;
            chkReadOutputJson.Checked = r?.OutputJson ?? false;


            chkUpdate.Checked = b.Procedures.ContainsKey(Bundle.SpType.Update);
            var u = b.Procedures.Maybe(Bundle.SpType.Upsert);

            chkUpsert.Checked = u != null;
            chkUpsertInputJson.Checked = u?.InputJson ?? false;

            chkDelete.Checked = b.Procedures.ContainsKey(Bundle.SpType.Delete);
            chkIdIn.Checked = b.Procedures.ContainsKey(Bundle.SpType.IdIn);


            chkRepoImplementMultiUpsertRepo.Checked = b.Repo.ImplementMultiUpsertRepo;
            chkRepoImplementUpsertRepo.Checked = b.Repo.ImplementUpsertRepo;
            chkRepoFilters.Checked = b.Repo.WithFiltering;

            chkCloneable.Checked = b.Dto.Cloneable;
            chkStateful.Checked = b.Dto.Stateful;
            chkHasWriteId.Checked = b.Dto.HasWriteId;
            chkRoundDecimals.Checked = b.Dto.DecimalPlaces != null;
            nudDecimalPlaces.Value = b.Dto.DecimalPlaces ?? 0;
            nudDecimalPlaces.Enabled = chkRoundDecimals.Checked;
            chkMatchIgnoreCase.Checked = b.Dto.MatchIgnoreCase;
            chkIgnoreWhitespace.Checked = b.Dto.MatchIgnoreWhitespace;
            chkNormalizeLineBreaks.Checked = b.Dto.MatchNormalizeLineBreak;

            _updatingChecks = false;
        }
        private bool MatchesListRwc(ListView lv, SpInfo i)
        {
            var marker = lv.Tag.ToString();
            if (!i.Name.ContainsOic(marker))
                return false;
            if (marker.EqualsOic("GetFor") && i.Name.ContainsOic("GetForMax")) return false;

            if (!(i is ReadWithCol rwc))
            {
                MessageBox.Show($"Found a {marker} column that isn't an ReadWithCol");
                return false;
            }

            //var rwc = GetReadWithCol(p.Name, p.ColFields);
            lv.Items.Add(_listViewHandlers[lv].Format(rwc));
            return true;
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {


        }

        private List<ReadWithCol> GetRwcs(Bundle.SpecializedProcedureType t)
        {
            switch (t)
            {
                case Bundle.SpecializedProcedureType.ReadBetween:
                    return GetRwcs(lvReadBetween);
                case Bundle.SpecializedProcedureType.ReadFor:
                    return GetRwcs(lvReadFor);
                case Bundle.SpecializedProcedureType.ReadForMax:
                    return GetRwcs(lvMaxQueries);
                case Bundle.SpecializedProcedureType.DeleteFor:
                    return GetRwcs(lvDelFor);
                default:
                    throw new ArgumentOutOfRangeException(nameof(t), t, null);
            }
        }

        private SpInfo GetSpInfo(Bundle.SpType t)
        {
            switch (t)
            {
                case Bundle.SpType.Create:
                    return GetSpInfo(chkCreate, "Create", chkCreateInputJson.Checked, chkCreateOutputJson.Checked);
                case Bundle.SpType.Read:
                    return GetSpInfo(chkRead, "Get", false, chkReadOutputJson.Checked);
                case Bundle.SpType.IdIn:
                    return GetSpInfo(chkIdIn, "GetForIds");
                case Bundle.SpType.Update:
                    return GetSpInfo(chkUpdate, "Update", chkUpdateInputJson.Checked);
                case Bundle.SpType.Delete:
                    return GetSpInfo(chkDelete, "Delete");
                case Bundle.SpType.Upsert:
                    return GetSpInfo(chkUpsert, "Upsert", chkUpsertInputJson.Checked);
                default:
                    throw new ArgumentOutOfRangeException(nameof(t), t, null);
            }
            /*
            info.Create = GetSpInfo(chkCreate, "Create", chkCreateInputJson.Checked, chkCreateOutputJson.Checked);
            info.Read = GetSpInfo(chkRead, "Get", false, chkReadOutputJson.Checked);
            info.ReadIdIn = GetSpInfo(chkIdIn, "GetForIds");
            info.ReadBetweens = GetRwcs(lvReadBetween);
            info.ReadForMaxs = GetRwcs(lvMaxQueries);
            info.ReadFors = GetRwcs(lvReadFor);
            info.DeleteFor = GetRwcs(lvDelFor);
            info.Update = GetSpInfo(chkUpdate, "Update", chkUpdateInputJson.Checked, false);
            info.Upsert = GetEnumSpInfo<UpsertMode>(chkUpsert, "Upsert", cbUpsertMode);
            info.Delete = GetSpInfo(chkDelete, "Delete", false, false);
             */
        }
        private SpInfo GetSpInfo(CheckBox chk, string name, bool inputJson = false, bool outputJson = false) =>
            GetMode(chk) == SpInfo.Modes.None
                ? null
                : new SpInfo(GetMode(chk), $"sp{ctxtPluralName.Text}_{name}", name, inputJson, outputJson);

        private SpInfo.Modes GetMode(CheckBox chk)
        {
            var mode = SpInfo.Modes.None;
            if (chk.Checked)
                mode = SpInfo.Modes.DropCreate;
            if (!chk.Checked && chkDropUnused.Checked)
                mode = SpInfo.Modes.Drop;
            return mode;
        }
        private void Generate()
        {
            if (null == Env) return;
            var bundle = GetBundle();
            if (null == bundle) return;
            NewContent?.Invoke(bundle);
        }
        //private CrudSpsInfo GetInfo()
        //{
        //    var info = new CrudSpsInfo()
        //    {
        //        Columns = _columns,
        //        NameBase = ctxtPluralName.Text,
        //        TableName = _tableName,
        //        Typename = ctxtSingularName.Text,
        //        RootNode = _rootNode,
        //        SubQueries =GetSubQueries(),
        //        Aggregates = GetSelectedAggregates(),
        //        DropUnused = chkDropUnused.Checked,
        //        RepoInfo = new RepoInfo
        //        {
        //            ImplementUpsertRepo = chkRepoImplementUpsertRepo.Checked
        //        },
        //        EnumFields = Env.GetEnumFields(),
        //        HandCoded = lvHandCoded.Items.Cast<ListViewItem>().Where(lvi => lvi.Checked).Select(lvi => lvi.Tag as ProcDef).ToList(),
        //    };

        //    info.Create = GetSpInfo(chkCreate, "Create", chkCreateInputJson.Checked, chkCreateOutputJson.Checked);
        //    info.Read = GetSpInfo(chkRead, "Get", false, chkReadOutputJson.Checked);
        //    info.ReadIdIn = GetSpInfo(chkIdIn, "GetForIds");
        //    info.ReadBetweens = GetRwcs(lvReadBetween);
        //    info.ReadForMaxs = GetRwcs(lvMaxQueries);
        //    info.ReadFors = GetRwcs(lvReadFor);
        //    info.DeleteFor = GetRwcs(lvDelFor);
        //    info.Update = GetSpInfo(chkUpdate, "Update", chkUpdateInputJson.Checked, false);
        //    info.Upsert = GetEnumSpInfo<UpsertMode>(chkUpsert, "Upsert", cbUpsertMode);
        //    info.Delete = GetSpInfo(chkDelete, "Delete", false, false);

        //    return info;
        //}



        private PopulatedBundle GetBundle()
        {
            if (_tableName.IsNullOrEmpty()) return null;
            return new Bundle
            {
                Table = new TableInfo { Name = _tableName, Singular = ctxtSingularName.Text, Plural = ctxtPluralName.Text },
                Procedures = Enum<Bundle.SpType>.Values.Select(t => (t, GetSpInfo(t))).Where(t => t.Item2 != null).ToDictionary(),
                SpecializedProcedures = Enum<Bundle.SpecializedProcedureType>.Values.ToDictionary(t => t, GetRwcs),
                Aggregates = GetSelectedAggregates(),
                HandCoded = lvHandCoded.Items.Cast<ListViewItem>().Where(lvi => lvi.Checked).Select(lvi => lvi.Tag as ProcDef).ToList(),
                Repo = new RepoInfo
                {
                    ImplementUpsertRepo = chkRepoImplementUpsertRepo.Checked,
                    ImplementMultiUpsertRepo = chkRepoImplementMultiUpsertRepo.Checked,
                    WithFiltering = chkRepoFilters.Checked

                },
                Dto = GetDtoInfo(),
                MatchColumns = chkImplementMatchable.Checked
                    ? lvMatchableColumns.Items.GetChecked().Select(lvi => lvi.Text).ToList()
                    : null
            }
                .Populate(Env, _columns, _rootNode);
        }

        private DtoInfo GetDtoInfo() =>
            new DtoInfo
            {
                Cloneable = chkCloneable.Checked,
                HasWriteId = chkHasWriteId.Checked,
                Properties = chkProperties.Checked,
                Stateful = chkStateful.Checked,
                JsonFields = lvJsonFields.Items.GetTags<JsonField>().ToDictionary(jf => jf.Column),
                Implements = lvInterfaces.Items.GetCheckedTags<InterfaceDef>().ToList(),
                DecimalPlaces = chkRoundDecimals.Checked ? (int?)nudDecimalPlaces.Value : null,
                MatchIgnoreCase = chkMatchIgnoreCase.Checked,
                MatchIgnoreWhitespace = chkIgnoreWhitespace.Checked,
                MatchNormalizeLineBreak = chkNormalizeLineBreaks.Checked
            };
        private List<ReadWithCol> GetRwcs(ListView lv)
        {
            var rwcs = lv.Items.GetTags<ReadWithCol>().NotNull().AsSorted(rwc => rwc.AllParameters.Count);
            var dict = new Dictionary<string, int>();
            foreach (var rwc in rwcs)
            {
                rwc.Number = dict.Increment(rwc.Name);
                rwc.OutputJson = chkReadOutputJson.Checked;
            }

            return rwcs.AsSorted(rwc => rwc.Name);
        }

        private List<Aggregate> GetSelectedAggregates() => new List<Aggregate>(SelectedAggregateItems.Select(lvi => lvi.Tag as Aggregate).NotNull());
        private IEnumerable<ListViewItem> SelectedAggregateItems => lvAggregates.Items.Cast<ListViewItem>().Where(i => null != i && i.Checked);

        private void SomethingChanged() => SomethingChanged(null, null);
        private void SomethingChanged(object sender, EventArgs e)
        {
            if (_updatingChecks) return;
            _updatingChecks = true;

            chkCreateOutputJson.Checked = chkReadOutputJson.Checked;
            lvReadBetween.Enabled = true;
            lvReadFor.Enabled = true;
            chkRepoImplementUpsertRepo.Enabled = chkUpsert.Checked;
            if (!chkUpsert.Checked)
                chkRepoImplementUpsertRepo.Checked = false;
            if (sender == chkUpsert)
                chkRepoImplementUpsertRepo.Checked = chkUpsert.Checked;
            Generate();
            _updatingChecks = false;
        }

        private void CrudSPPage_Load(object sender, EventArgs e)
        {
            //_rsChecks = new List<CheckBox> { chkCreate, chkRead, chkUpdate, chkDelete };
            //_jsonChecks = new List<CheckBox> { chkCreateInputJson, chkReadOutputJson, chkCreateOutputJson, chkUpdateInputJson };
            SomethingChanged();
        }

        private void lvAggregates_Resize(object sender, EventArgs e)
        {
            chAggAlias.Width = lvAggregates.ClientRectangle.Width;
        }

        private void lvItemChecked(object sender, ItemCheckedEventArgs e)
        {
            SomethingChanged();
        }


        private void lvJsonFields_MouseDoubleClick(object sender, MouseEventArgs e)
         => HandleKey(lvJsonFields, Keys.Enter);



        private void lvHandCoded_DoubleClick(object sender, EventArgs e)
        {
            var indeces = lvHandCoded.SelectedIndices?.Cast<int>();
            if (indeces.IsNullOrEmpty())
                return;
        }

        private void lvKeyDown(object sender, KeyEventArgs e)
        {
            HandleKey(sender as ListView, e.KeyCode);
        }

        private void HandleKey(ListView lv, Keys key)
        {
            if (null == lv) return;
            if (_columns.IsNullOrEmpty()) return;
            var handler = _listViewHandlers.Maybe(lv);
            if (null == handler) return;
            object res;
            switch (key)
            {
                case Keys.Delete:
                    lv
                        .SelectedItems
                        .Cast<ListViewItem>()
                        .ForEach(lv.Items.Remove);
                    break;
                case Keys.Add:

                    res = handler.Edit(null);
                    if (null == res) return;
                    lv.Items.Add(handler.Format(new ListViewItem(), res));
                    break;
                case Keys.Enter:
                    var lvi = lv.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
                    if (null == lvi)
                    {
                        HandleKey(lv, Keys.Add);
                        return;
                    }
                    res = handler.Edit(lvi.Tag);
                    if (null == res) return;
                    handler.Format(lvi, res);
                    break;
                default:
                    return;
            }
            SomethingChanged();
        }

        private abstract class ListViewHandler
        {
            public abstract ListViewItem Format(ListViewItem lvi, object obj);
            public virtual ListViewItem Format(object o) => Format(new ListViewItem(), o);
            public abstract object Edit(object obj);

        }

        private abstract class ListViewHandler<T> : ListViewHandler where T : class, new()
        {
            public abstract ListViewItem Format(ListViewItem lvi, T obj);
            public abstract T Edit(T obj);
            public virtual T Edit() => Edit(new T());
            public override object Edit(object obj) => Edit(obj as T);
            public override ListViewItem Format(ListViewItem lvi, object obj) => Format(lvi, obj as T);

        }

        private void SetRWCHandler(ListView lv, string marker, bool defaultReturnsList, bool showValue = false)
        {
            _listViewHandlers[lv] = new ReadWithColHandler(_columns, ctxtPluralName.Text, marker, this, defaultReturnsList, showValue);
        }
        private void SetJsonFieldHandler(ListView lv)
        {
            _listViewHandlers[lv] = new JsonFieldListViewHandler(Env, _columns, this);
        }
        private class ReadWithColHandler : ListViewHandler<ReadWithCol>
        {
            private readonly List<ColumnProperties> _columns;
            private readonly bool _returnsList;
            private readonly bool _showValue;
            private readonly string _pluralName;
            private readonly string _marker;
            private readonly IWin32Window _owner;

            public ReadWithColHandler(List<ColumnProperties> columns, string pluralName, string marker, IWin32Window owner, bool returnsList, bool showValue)
            {
                _columns = columns;
                _pluralName = pluralName;
                _marker = marker;
                _owner = owner;
                _returnsList = returnsList;
                _showValue = showValue;
            }

            public override ReadWithCol Edit(ReadWithCol d)
            {
                if (d != null) d = new ReadWithCol(d);
                var frm = new ReadForMaxEdit(_columns, _marker, d, _returnsList, _showValue) { };
                if (frm.ShowDialog(_owner) != DialogResult.OK) return null;

                d = frm.Def;
                var name = $"{_marker}{frm.Def.Column}";
                if (d.GetParameters(ParameterMode.Null).Any())
                {
                    var p = d.GetParameters(ParameterMode.Null).SingleOrDefault();
                    name = p == null ? $"Mixed{d.Column}" : name + "Without" + p.Name;
                }
                if (name.EndsWith("Id"))
                    name = name.Substring(0, name.Length - 2);
                d.CodeName = name;
                d.Name = $"sp{_pluralName}_{d.CodeName}";
                d.Mode = SpInfo.Modes.DropCreate;
                d.OutputJson = false;
                return d;
            }

            public override ListViewItem Format(ListViewItem lvi, ReadWithCol def)
            {
                lvi.Text = def.Column;
                if (lvi.SubItems.Count < 2)
                    lvi.SubItems.Add("");
                lvi.SubItems[1].Text = def.AllParameters.Select(p => $"{p.Name} ({p.Mode.GetShortName()})").Join(", ");
                lvi.Tag = def;
                return lvi;
            }
        }

        private class JsonFieldListViewHandler : ListViewHandler<JsonField>
        {
            private readonly CodeEnvironment _env;
            private readonly List<ColumnProperties> _columns;
            private readonly IWin32Window _owner;

            public JsonFieldListViewHandler(CodeEnvironment env, List<ColumnProperties> columns, IWin32Window owner)
            {
                _env = env;
                _columns = columns;
                _owner = owner;
            }

            public override JsonField Edit(JsonField obj)
            {
                if (null == obj) obj = new JsonField();
                var frm = new JsonFieldForm(_env, _columns, obj);
                if (frm.ShowDialog(_owner) == DialogResult.Cancel) return null;
                return frm.Field;
            }

            public override ListViewItem Format(ListViewItem lvi, JsonField obj)
            {

                lvi.SubItems[0].Text = obj.Column;
                if (lvi.SubItems.Count < 2)
                    lvi.SubItems.Add("");
                lvi.SubItems[1].Text = obj.Type;
                lvi.Tag = obj;
                return lvi;
            }
        }
        private void lvMouseDoubleClick(object sender, MouseEventArgs e) => HandleKey(sender as ListView, Keys.Enter);
        private void tsmiAdd_Click(object sender, EventArgs e) => HandleKey(GetSource(sender), Keys.Add);
        private void tsmiEdit_Click(object sender, EventArgs e) => HandleKey(GetSource(sender), Keys.Enter);
        private void tsmiDelete_Click(object sender, EventArgs e) => HandleKey(GetSource(sender), Keys.Delete);
        private ListView GetSource(object o)
        {
            var mi = o.As<ToolStripMenuItem>();
            var p = mi?.Owner;
            var strip = p.As<ContextMenuStrip>();
            var source = strip.SourceControl.As<ListView>();
            return source;
        }

        private void lvAggregates_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var lv = sender as ListView;
            lv.Items.Update().Selected().With<Aggregate>(agg =>
                {
                    new AggregateEditor(agg).ShowDialog();
                })
            .With(SetAggregateListViewItem)
            .Run();
            SomethingChanged();
        }

        private void btnImportClassDef_Click(object sender, EventArgs e)
        {
            //var frm = new ClassDefImportForm();
            //if (frm.ShowDialog() != DialogResult.OK) return;
            //var b = frm.Bundle;
            //var tableName = b.Table.Name;
            //Bundles[tableName] = b;
            //LoadTable(tableName);
        }

        private void chkImplementMatchable_CheckedChanged(object sender, EventArgs e)
        {
            lvMatchableColumns.Enabled = chkImplementMatchable.Checked;
            SomethingChanged();
        }

        private void chkRoundDecimals_CheckedChanged(object sender, EventArgs e)
        {
            nudDecimalPlaces.Enabled = chkRoundDecimals.Checked;
            SomethingChanged();
        }
    }
}
