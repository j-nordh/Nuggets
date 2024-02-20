using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using SupplyChain.Procs;
using UtilClasses;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Strings;
using JsonSubTypes;
namespace SupplyChain.Dto
{
    [Serializable]
    
    public class Bundle
    {
        public TableInfo Table { get; set; }
        public enum SpType
        {
            Create,
            [Key("Get")]
            Read,
            IdIn,
            Update,
            Delete,
            Upsert
        }
        public enum SpecializedProcedureType
        {
            [Key("GetBetween")]
            ReadBetween,
            [Key("GetFor")]
            ReadFor,
            [Key("GetForMax")]
            ReadForMax,
            DeleteFor
        }

        public Bundle()
        {
            Dto= new DtoInfo();
            HandCoded = new List<ProcDef>();
            Repo = new RepoInfo();
            Aggregates = new List<Aggregate>();
            SpecializedProcedures = new Dictionary<SpecializedProcedureType, List<ReadWithCol>>();
            Procedures = new Dictionary<SpType, SpInfo>();
            Table = new TableInfo();
        }

        public Bundle(Bundle x)
        {
            Table = x.Table.Clone();
            Procedures = x.Procedures.Clone();
            SpecializedProcedures = x.SpecializedProcedures.Clone();
            Aggregates = x.Aggregates.ToList();
            Repo = x.Repo;
            HandCoded = x.HandCoded;
            Dto = x.Dto?.Clone();
            MatchColumns = x.MatchColumns?.ToList();
        }

        public Dictionary<SpType, SpInfo> Procedures { get; set; }
        public Dictionary<SpecializedProcedureType, List<ReadWithCol>> SpecializedProcedures { get; set; }

        public List<Aggregate> Aggregates { get; set; }
        public RepoInfo Repo { get; set; }
        public List<ProcDef> HandCoded { get; set; }
        public DtoInfo Dto { get; set; }
        public List<string> MatchColumns { get; set; }
    }

    public class PopulatedBundle : Bundle
    {
        public TableNode RootNode { get; set; }
        public List<ColumnProperties> Columns { get; set; }
        public Dictionary<string, JsonField> JsonFields { get; set; }
        public Dictionary<string, string> EnumFields { get; set; }
        public PopulatedDtoInfo DtoPop { get; set; }
        public PopulatedRepoInfo RepoPop { get; set; }
        [UsedImplicitly]
        public PopulatedBundle()
        {
            Init();
        }
        public PopulatedBundle(Bundle x) : base(x)
        {
            Init();
        }

        private void Init()
        {
            Specialized = new SpecProcWrapper(this);
        }


        public SpInfo Create
        {
            get => Procedures.Maybe(SpType.Create);
            set => Procedures[SpType.Create] = value;
        }
        public SpInfo Read
        {
            get => Procedures.Maybe(SpType.Read);
            set => Procedures[SpType.Read] = value;
        }

        public SpInfo ReadIdIn
        {
            get => Procedures.Maybe(SpType.IdIn);
            set => Procedures[SpType.IdIn] = value;
        }

        public SpInfo Update
        {
            get => Procedures.Maybe(SpType.Update);
            set => Procedures[SpType.Update] = value;
        }

        public SpInfo Upsert
        {
            get => Procedures.Maybe(SpType.Upsert);
            set => Procedures[SpType.Upsert] = value;
        }

        public SpInfo Delete
        {
            get => Procedures.Maybe(SpType.Delete);
            set => Procedures[SpType.Delete] = value;
        }

        public PopulatedLinkTable LinkTable { get; set; }

        public bool IsCrud => Create.ShouldCreate() &&
                              Read.ShouldCreate() &&
                              Update.ShouldCreate() &&
                              Delete.ShouldCreate() &&
                              Columns.Any(c => c.Name.EqualsOic("Id"));
        public SpecProcWrapper Specialized { get; private set; }

        public class SpecProcWrapper
        {
            private readonly PopulatedBundle _parent;

            public SpecProcWrapper(PopulatedBundle parent)
            {
                _parent = parent;
            }

            public List<ReadWithCol> ReadBetween => _parent.SpecializedProcedures[SpecializedProcedureType.ReadBetween];
            public List<ReadWithCol> ReadFor => _parent.SpecializedProcedures[SpecializedProcedureType.ReadFor];
            public List<ReadWithCol> ReadForMax => _parent.SpecializedProcedures[SpecializedProcedureType.ReadForMax];
            public List<ReadWithCol> DeleteFor => _parent.SpecializedProcedures[SpecializedProcedureType.DeleteFor];
        }
    }


}

