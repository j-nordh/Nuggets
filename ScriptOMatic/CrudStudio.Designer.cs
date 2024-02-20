namespace ScriptOMatic
{
    partial class CrudStudio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test");
            this.lddlEnvironment = new UtilClasses.Winforms.LabelledCombo();
            this.tvTables = new System.Windows.Forms.TreeView();
            this.flpTop = new System.Windows.Forms.FlowLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ltxtPlural = new UtilClasses.Winforms.LabelledTextbox();
            this.flpTableTop = new System.Windows.Forms.FlowLayoutPanel();
            this.ltxtSingular = new UtilClasses.Winforms.LabelledTextbox();
            this.gbTableMode = new System.Windows.Forms.GroupBox();
            this.rbCRUD = new System.Windows.Forms.RadioButton();
            this.rbEnum = new System.Windows.Forms.RadioButton();
            this.tlpOperations = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblJsonIn = new System.Windows.Forms.Label();
            this.lblOp = new System.Windows.Forms.Label();
            this.chkCreate = new System.Windows.Forms.CheckBox();
            this.chkCreateInputJson = new System.Windows.Forms.CheckBox();
            this.chkCreateOutputJson = new System.Windows.Forms.CheckBox();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.chkUpdate = new System.Windows.Forms.CheckBox();
            this.chkUpdateInputJson = new System.Windows.Forms.CheckBox();
            this.lvReadFor = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkReadFor = new System.Windows.Forms.CheckBox();
            this.chkReadOutputJson = new System.Windows.Forms.CheckBox();
            this.cbReadBetweenColumn = new System.Windows.Forms.ComboBox();
            this.chkRead = new System.Windows.Forms.CheckBox();
            this.chkReadBetween = new System.Windows.Forms.CheckBox();
            this.chkUpsert = new System.Windows.Forms.CheckBox();
            this.gbHandCoded = new System.Windows.Forms.GroupBox();
            this.lvHandCoded = new System.Windows.Forms.ListView();
            this.chHandCodedName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHandCodedReturns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbDto = new System.Windows.Forms.GroupBox();
            this.gbImplement = new System.Windows.Forms.GroupBox();
            this.chkStateful = new System.Windows.Forms.CheckBox();
            this.chkCloneable = new System.Windows.Forms.CheckBox();
            this.chkHasWriteId = new System.Windows.Forms.CheckBox();
            this.chkProperties = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvJsonFields = new System.Windows.Forms.ListView();
            this.chJsonFieldColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chJsonFieldType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnAddJsonField = new System.Windows.Forms.Button();
            this.btnDelJsonField = new System.Windows.Forms.Button();
            this.flpTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flpTableTop.SuspendLayout();
            this.gbTableMode.SuspendLayout();
            this.tlpOperations.SuspendLayout();
            this.gbHandCoded.SuspendLayout();
            this.gbDto.SuspendLayout();
            this.gbImplement.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lddlEnvironment
            // 
            this.lddlEnvironment.Caption = "Environment";
            this.lddlEnvironment.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.lddlEnvironment.DisplayMember = "Name";
            this.lddlEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.lddlEnvironment.Location = new System.Drawing.Point(3, 3);
            this.lddlEnvironment.Name = "lddlEnvironment";
            this.lddlEnvironment.SelectedObject = null;
            this.lddlEnvironment.Size = new System.Drawing.Size(96, 44);
            this.lddlEnvironment.TabIndex = 0;
            this.lddlEnvironment.SelectedIndexChanged += new System.Action(this.lddlEnvironment_SelectedIndexChanged);
            // 
            // tvTables
            // 
            this.tvTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTables.Location = new System.Drawing.Point(0, 0);
            this.tvTables.Name = "tvTables";
            this.tvTables.Size = new System.Drawing.Size(193, 453);
            this.tvTables.TabIndex = 1;
            // 
            // flpTop
            // 
            this.flpTop.Controls.Add(this.lddlEnvironment);
            this.flpTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpTop.Location = new System.Drawing.Point(0, 0);
            this.flpTop.Name = "flpTop";
            this.flpTop.Size = new System.Drawing.Size(800, 43);
            this.flpTop.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 43);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvTables);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.gbDto);
            this.splitContainer1.Panel2.Controls.Add(this.gbHandCoded);
            this.splitContainer1.Panel2.Controls.Add(this.tlpOperations);
            this.splitContainer1.Panel2.Controls.Add(this.flpTableTop);
            this.splitContainer1.Size = new System.Drawing.Size(800, 465);
            this.splitContainer1.SplitterDistance = 193;
            this.splitContainer1.TabIndex = 3;
            // 
            // ltxtPlural
            // 
            this.ltxtPlural.Caption = "Plural";
            this.ltxtPlural.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.ltxtPlural.Location = new System.Drawing.Point(3, 3);
            this.ltxtPlural.Multiline = false;
            this.ltxtPlural.Name = "ltxtPlural";
            this.ltxtPlural.ReadOnly = false;
            this.ltxtPlural.Size = new System.Drawing.Size(101, 37);
            this.ltxtPlural.TabIndex = 0;
            this.ltxtPlural.UnitText = "";
            this.ltxtPlural.UsePasswordChar = false;
            // 
            // flpTableTop
            // 
            this.flpTableTop.Controls.Add(this.ltxtPlural);
            this.flpTableTop.Controls.Add(this.ltxtSingular);
            this.flpTableTop.Controls.Add(this.gbTableMode);
            this.flpTableTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpTableTop.Location = new System.Drawing.Point(0, 0);
            this.flpTableTop.Name = "flpTableTop";
            this.flpTableTop.Size = new System.Drawing.Size(616, 43);
            this.flpTableTop.TabIndex = 1;
            // 
            // ltxtSingular
            // 
            this.ltxtSingular.Caption = "Singular";
            this.ltxtSingular.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.ltxtSingular.Location = new System.Drawing.Point(110, 3);
            this.ltxtSingular.Multiline = false;
            this.ltxtSingular.Name = "ltxtSingular";
            this.ltxtSingular.ReadOnly = false;
            this.ltxtSingular.Size = new System.Drawing.Size(101, 37);
            this.ltxtSingular.TabIndex = 1;
            this.ltxtSingular.UnitText = "";
            this.ltxtSingular.UsePasswordChar = false;
            // 
            // gbTableMode
            // 
            this.gbTableMode.Controls.Add(this.rbEnum);
            this.gbTableMode.Controls.Add(this.rbCRUD);
            this.gbTableMode.Location = new System.Drawing.Point(217, 3);
            this.gbTableMode.Name = "gbTableMode";
            this.gbTableMode.Size = new System.Drawing.Size(123, 37);
            this.gbTableMode.TabIndex = 2;
            this.gbTableMode.TabStop = false;
            this.gbTableMode.Text = "Mode";
            // 
            // rbCRUD
            // 
            this.rbCRUD.AutoSize = true;
            this.rbCRUD.Location = new System.Drawing.Point(7, 14);
            this.rbCRUD.Name = "rbCRUD";
            this.rbCRUD.Size = new System.Drawing.Size(56, 17);
            this.rbCRUD.TabIndex = 0;
            this.rbCRUD.TabStop = true;
            this.rbCRUD.Text = "CRUD";
            this.rbCRUD.UseVisualStyleBackColor = true;
            // 
            // rbEnum
            // 
            this.rbEnum.AutoSize = true;
            this.rbEnum.Location = new System.Drawing.Point(69, 14);
            this.rbEnum.Name = "rbEnum";
            this.rbEnum.Size = new System.Drawing.Size(52, 17);
            this.rbEnum.TabIndex = 1;
            this.rbEnum.TabStop = true;
            this.rbEnum.Text = "Enum";
            this.rbEnum.UseVisualStyleBackColor = true;
            // 
            // tlpOperations
            // 
            this.tlpOperations.AutoSize = true;
            this.tlpOperations.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpOperations.ColumnCount = 3;
            this.tlpOperations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOperations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tlpOperations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tlpOperations.Controls.Add(this.label1, 2, 0);
            this.tlpOperations.Controls.Add(this.lblJsonIn, 1, 0);
            this.tlpOperations.Controls.Add(this.lblOp, 0, 0);
            this.tlpOperations.Controls.Add(this.chkCreate, 0, 1);
            this.tlpOperations.Controls.Add(this.chkCreateInputJson, 1, 1);
            this.tlpOperations.Controls.Add(this.chkCreateOutputJson, 2, 1);
            this.tlpOperations.Controls.Add(this.chkDelete, 0, 4);
            this.tlpOperations.Controls.Add(this.chkUpdate, 0, 3);
            this.tlpOperations.Controls.Add(this.chkUpdateInputJson, 1, 3);
            this.tlpOperations.Controls.Add(this.lvReadFor, 1, 5);
            this.tlpOperations.Controls.Add(this.chkReadFor, 0, 5);
            this.tlpOperations.Controls.Add(this.chkReadOutputJson, 2, 2);
            this.tlpOperations.Controls.Add(this.cbReadBetweenColumn, 1, 6);
            this.tlpOperations.Controls.Add(this.chkRead, 0, 2);
            this.tlpOperations.Controls.Add(this.chkReadBetween, 0, 6);
            this.tlpOperations.Controls.Add(this.chkUpsert, 0, 7);
            this.tlpOperations.Location = new System.Drawing.Point(3, 43);
            this.tlpOperations.Margin = new System.Windows.Forms.Padding(0);
            this.tlpOperations.Name = "tlpOperations";
            this.tlpOperations.RowCount = 8;
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpOperations.Size = new System.Drawing.Size(229, 249);
            this.tlpOperations.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(162, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Json Out";
            // 
            // lblJsonIn
            // 
            this.lblJsonIn.AutoSize = true;
            this.lblJsonIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJsonIn.Location = new System.Drawing.Point(104, 0);
            this.lblJsonIn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblJsonIn.Name = "lblJsonIn";
            this.lblJsonIn.Size = new System.Drawing.Size(53, 15);
            this.lblJsonIn.TabIndex = 19;
            this.lblJsonIn.Text = "Json In";
            // 
            // lblOp
            // 
            this.lblOp.AutoSize = true;
            this.lblOp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOp.Location = new System.Drawing.Point(2, 0);
            this.lblOp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOp.Name = "lblOp";
            this.lblOp.Size = new System.Drawing.Size(70, 15);
            this.lblOp.TabIndex = 18;
            this.lblOp.Text = "Operation";
            // 
            // chkCreate
            // 
            this.chkCreate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkCreate.AutoSize = true;
            this.chkCreate.Location = new System.Drawing.Point(3, 18);
            this.chkCreate.Name = "chkCreate";
            this.chkCreate.Size = new System.Drawing.Size(57, 17);
            this.chkCreate.TabIndex = 2;
            this.chkCreate.Text = "Create";
            this.chkCreate.UseVisualStyleBackColor = true;
            // 
            // chkCreateInputJson
            // 
            this.chkCreateInputJson.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkCreateInputJson.AutoSize = true;
            this.chkCreateInputJson.Location = new System.Drawing.Point(123, 19);
            this.chkCreateInputJson.Name = "chkCreateInputJson";
            this.chkCreateInputJson.Size = new System.Drawing.Size(15, 14);
            this.chkCreateInputJson.TabIndex = 2;
            this.chkCreateInputJson.UseVisualStyleBackColor = true;
            // 
            // chkCreateOutputJson
            // 
            this.chkCreateOutputJson.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkCreateOutputJson.AutoSize = true;
            this.chkCreateOutputJson.Enabled = false;
            this.chkCreateOutputJson.Location = new System.Drawing.Point(187, 19);
            this.chkCreateOutputJson.Name = "chkCreateOutputJson";
            this.chkCreateOutputJson.Size = new System.Drawing.Size(15, 14);
            this.chkCreateOutputJson.TabIndex = 15;
            this.chkCreateOutputJson.UseVisualStyleBackColor = true;
            // 
            // chkDelete
            // 
            this.chkDelete.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(3, 87);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(57, 17);
            this.chkDelete.TabIndex = 5;
            this.chkDelete.Text = "Delete";
            this.chkDelete.UseVisualStyleBackColor = true;
            // 
            // chkUpdate
            // 
            this.chkUpdate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkUpdate.AutoSize = true;
            this.chkUpdate.Location = new System.Drawing.Point(3, 64);
            this.chkUpdate.Name = "chkUpdate";
            this.chkUpdate.Size = new System.Drawing.Size(61, 17);
            this.chkUpdate.TabIndex = 4;
            this.chkUpdate.Text = "Update";
            this.chkUpdate.UseVisualStyleBackColor = true;
            // 
            // chkUpdateInputJson
            // 
            this.chkUpdateInputJson.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkUpdateInputJson.AutoSize = true;
            this.chkUpdateInputJson.Location = new System.Drawing.Point(123, 65);
            this.chkUpdateInputJson.Name = "chkUpdateInputJson";
            this.chkUpdateInputJson.Size = new System.Drawing.Size(15, 14);
            this.chkUpdateInputJson.TabIndex = 16;
            this.chkUpdateInputJson.UseVisualStyleBackColor = true;
            // 
            // lvReadFor
            // 
            this.lvReadFor.CheckBoxes = true;
            this.lvReadFor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.tlpOperations.SetColumnSpan(this.lvReadFor, 2);
            this.lvReadFor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvReadFor.FullRowSelect = true;
            this.lvReadFor.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.Checked = true;
            listViewItem1.StateImageIndex = 1;
            this.lvReadFor.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvReadFor.Location = new System.Drawing.Point(105, 110);
            this.lvReadFor.Name = "lvReadFor";
            this.lvReadFor.Size = new System.Drawing.Size(121, 86);
            this.lvReadFor.TabIndex = 25;
            this.lvReadFor.UseCompatibleStateImageBehavior = false;
            this.lvReadFor.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 100;
            // 
            // chkReadFor
            // 
            this.chkReadFor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkReadFor.AutoSize = true;
            this.chkReadFor.Location = new System.Drawing.Point(3, 144);
            this.chkReadFor.Name = "chkReadFor";
            this.chkReadFor.Size = new System.Drawing.Size(67, 17);
            this.chkReadFor.TabIndex = 24;
            this.chkReadFor.Text = "Read for";
            this.chkReadFor.UseVisualStyleBackColor = true;
            // 
            // chkReadOutputJson
            // 
            this.chkReadOutputJson.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkReadOutputJson.AutoSize = true;
            this.chkReadOutputJson.Location = new System.Drawing.Point(187, 42);
            this.chkReadOutputJson.Name = "chkReadOutputJson";
            this.chkReadOutputJson.Size = new System.Drawing.Size(15, 14);
            this.chkReadOutputJson.TabIndex = 3;
            this.chkReadOutputJson.UseVisualStyleBackColor = true;
            // 
            // cbReadBetweenColumn
            // 
            this.tlpOperations.SetColumnSpan(this.cbReadBetweenColumn, 2);
            this.cbReadBetweenColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReadBetweenColumn.FormattingEnabled = true;
            this.cbReadBetweenColumn.Location = new System.Drawing.Point(105, 202);
            this.cbReadBetweenColumn.Name = "cbReadBetweenColumn";
            this.cbReadBetweenColumn.Size = new System.Drawing.Size(114, 21);
            this.cbReadBetweenColumn.TabIndex = 23;
            // 
            // chkRead
            // 
            this.chkRead.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkRead.AutoSize = true;
            this.chkRead.Location = new System.Drawing.Point(3, 41);
            this.chkRead.Name = "chkRead";
            this.chkRead.Size = new System.Drawing.Size(52, 17);
            this.chkRead.TabIndex = 3;
            this.chkRead.Text = "Read";
            this.chkRead.UseVisualStyleBackColor = true;
            // 
            // chkReadBetween
            // 
            this.chkReadBetween.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkReadBetween.AutoSize = true;
            this.chkReadBetween.Location = new System.Drawing.Point(3, 204);
            this.chkReadBetween.Name = "chkReadBetween";
            this.chkReadBetween.Size = new System.Drawing.Size(96, 17);
            this.chkReadBetween.TabIndex = 22;
            this.chkReadBetween.Text = "Read between";
            this.chkReadBetween.UseVisualStyleBackColor = true;
            // 
            // chkUpsert
            // 
            this.chkUpsert.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkUpsert.AutoSize = true;
            this.chkUpsert.Location = new System.Drawing.Point(3, 229);
            this.chkUpsert.Name = "chkUpsert";
            this.chkUpsert.Size = new System.Drawing.Size(57, 17);
            this.chkUpsert.TabIndex = 21;
            this.chkUpsert.Text = "Upsert";
            this.chkUpsert.UseVisualStyleBackColor = true;
            // 
            // gbHandCoded
            // 
            this.gbHandCoded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHandCoded.Controls.Add(this.lvHandCoded);
            this.gbHandCoded.Location = new System.Drawing.Point(3, 295);
            this.gbHandCoded.Name = "gbHandCoded";
            this.gbHandCoded.Size = new System.Drawing.Size(259, 143);
            this.gbHandCoded.TabIndex = 20;
            this.gbHandCoded.TabStop = false;
            this.gbHandCoded.Text = "Hand coded procedures";
            // 
            // lvHandCoded
            // 
            this.lvHandCoded.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chHandCodedName,
            this.chHandCodedReturns});
            this.lvHandCoded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvHandCoded.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvHandCoded.Location = new System.Drawing.Point(3, 16);
            this.lvHandCoded.Name = "lvHandCoded";
            this.lvHandCoded.Size = new System.Drawing.Size(288, 124);
            this.lvHandCoded.TabIndex = 0;
            this.lvHandCoded.UseCompatibleStateImageBehavior = false;
            this.lvHandCoded.View = System.Windows.Forms.View.Details;
            // 
            // chHandCodedName
            // 
            this.chHandCodedName.Text = "Name";
            this.chHandCodedName.Width = 100;
            // 
            // chHandCodedReturns
            // 
            this.chHandCodedReturns.Text = "Returns";
            this.chHandCodedReturns.Width = 100;
            // 
            // gbDto
            // 
            this.gbDto.Controls.Add(this.gbImplement);
            this.gbDto.Controls.Add(this.chkProperties);
            this.gbDto.Location = new System.Drawing.Point(234, 45);
            this.gbDto.Margin = new System.Windows.Forms.Padding(2);
            this.gbDto.Name = "gbDto";
            this.gbDto.Padding = new System.Windows.Forms.Padding(2);
            this.gbDto.Size = new System.Drawing.Size(230, 95);
            this.gbDto.TabIndex = 21;
            this.gbDto.TabStop = false;
            this.gbDto.Text = "Data Transfer Object";
            // 
            // gbImplement
            // 
            this.gbImplement.Controls.Add(this.chkStateful);
            this.gbImplement.Controls.Add(this.chkCloneable);
            this.gbImplement.Controls.Add(this.chkHasWriteId);
            this.gbImplement.Location = new System.Drawing.Point(0, 32);
            this.gbImplement.Margin = new System.Windows.Forms.Padding(2);
            this.gbImplement.Name = "gbImplement";
            this.gbImplement.Padding = new System.Windows.Forms.Padding(2);
            this.gbImplement.Size = new System.Drawing.Size(227, 64);
            this.gbImplement.TabIndex = 11;
            this.gbImplement.TabStop = false;
            this.gbImplement.Text = "Implement";
            // 
            // chkStateful
            // 
            this.chkStateful.AutoSize = true;
            this.chkStateful.Location = new System.Drawing.Point(4, 17);
            this.chkStateful.Margin = new System.Windows.Forms.Padding(2);
            this.chkStateful.Name = "chkStateful";
            this.chkStateful.Size = new System.Drawing.Size(65, 17);
            this.chkStateful.TabIndex = 4;
            this.chkStateful.Text = "IStateful";
            this.chkStateful.UseVisualStyleBackColor = true;
            // 
            // chkCloneable
            // 
            this.chkCloneable.AutoSize = true;
            this.chkCloneable.Location = new System.Drawing.Point(99, 17);
            this.chkCloneable.Margin = new System.Windows.Forms.Padding(2);
            this.chkCloneable.Name = "chkCloneable";
            this.chkCloneable.Size = new System.Drawing.Size(76, 17);
            this.chkCloneable.TabIndex = 7;
            this.chkCloneable.Text = "ICloneable";
            this.chkCloneable.UseVisualStyleBackColor = true;
            // 
            // chkHasWriteId
            // 
            this.chkHasWriteId.AutoSize = true;
            this.chkHasWriteId.Location = new System.Drawing.Point(4, 39);
            this.chkHasWriteId.Margin = new System.Windows.Forms.Padding(2);
            this.chkHasWriteId.Name = "chkHasWriteId";
            this.chkHasWriteId.Size = new System.Drawing.Size(82, 17);
            this.chkHasWriteId.TabIndex = 6;
            this.chkHasWriteId.Text = "IHasWriteId";
            this.chkHasWriteId.UseVisualStyleBackColor = true;
            // 
            // chkProperties
            // 
            this.chkProperties.AutoSize = true;
            this.chkProperties.Checked = true;
            this.chkProperties.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProperties.Location = new System.Drawing.Point(1, 17);
            this.chkProperties.Margin = new System.Windows.Forms.Padding(2);
            this.chkProperties.Name = "chkProperties";
            this.chkProperties.Size = new System.Drawing.Size(73, 17);
            this.chkProperties.TabIndex = 9;
            this.chkProperties.Text = "Properties";
            this.chkProperties.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvJsonFields);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Location = new System.Drawing.Point(235, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 139);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "JSON fields";
            // 
            // lvJsonFields
            // 
            this.lvJsonFields.CheckBoxes = true;
            this.lvJsonFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chJsonFieldColumn,
            this.chJsonFieldType});
            this.lvJsonFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvJsonFields.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvJsonFields.Location = new System.Drawing.Point(3, 16);
            this.lvJsonFields.Name = "lvJsonFields";
            this.lvJsonFields.Size = new System.Drawing.Size(181, 120);
            this.lvJsonFields.TabIndex = 24;
            this.lvJsonFields.UseCompatibleStateImageBehavior = false;
            this.lvJsonFields.View = System.Windows.Forms.View.Details;
            // 
            // chJsonFieldColumn
            // 
            this.chJsonFieldColumn.Text = "Column";
            this.chJsonFieldColumn.Width = 80;
            // 
            // chJsonFieldType
            // 
            this.chJsonFieldType.Text = "Type";
            this.chJsonFieldType.Width = 80;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnAddJsonField);
            this.panel4.Controls.Add(this.btnDelJsonField);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(184, 16);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(42, 120);
            this.panel4.TabIndex = 25;
            // 
            // btnAddJsonField
            // 
            this.btnAddJsonField.Location = new System.Drawing.Point(1, 3);
            this.btnAddJsonField.Name = "btnAddJsonField";
            this.btnAddJsonField.Size = new System.Drawing.Size(41, 22);
            this.btnAddJsonField.TabIndex = 2;
            this.btnAddJsonField.Text = "Add";
            this.btnAddJsonField.UseVisualStyleBackColor = true;
            // 
            // btnDelJsonField
            // 
            this.btnDelJsonField.Location = new System.Drawing.Point(1, 31);
            this.btnDelJsonField.Name = "btnDelJsonField";
            this.btnDelJsonField.Size = new System.Drawing.Size(41, 22);
            this.btnDelJsonField.TabIndex = 3;
            this.btnDelJsonField.Text = "Del";
            this.btnDelJsonField.UseVisualStyleBackColor = true;
            // 
            // CrudStudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 508);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.flpTop);
            this.Name = "CrudStudio";
            this.Text = "CrudStudio";
            this.Load += new System.EventHandler(this.CrudStudio_Load);
            this.flpTop.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flpTableTop.ResumeLayout(false);
            this.gbTableMode.ResumeLayout(false);
            this.gbTableMode.PerformLayout();
            this.tlpOperations.ResumeLayout(false);
            this.tlpOperations.PerformLayout();
            this.gbHandCoded.ResumeLayout(false);
            this.gbDto.ResumeLayout(false);
            this.gbDto.PerformLayout();
            this.gbImplement.ResumeLayout(false);
            this.gbImplement.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UtilClasses.Winforms.LabelledCombo lddlEnvironment;
        private System.Windows.Forms.TreeView tvTables;
        private System.Windows.Forms.FlowLayoutPanel flpTop;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flpTableTop;
        private UtilClasses.Winforms.LabelledTextbox ltxtPlural;
        private UtilClasses.Winforms.LabelledTextbox ltxtSingular;
        private System.Windows.Forms.GroupBox gbTableMode;
        private System.Windows.Forms.RadioButton rbEnum;
        private System.Windows.Forms.RadioButton rbCRUD;
        private System.Windows.Forms.TableLayoutPanel tlpOperations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblJsonIn;
        private System.Windows.Forms.Label lblOp;
        private System.Windows.Forms.CheckBox chkCreate;
        private System.Windows.Forms.CheckBox chkCreateInputJson;
        private System.Windows.Forms.CheckBox chkCreateOutputJson;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.CheckBox chkUpdate;
        private System.Windows.Forms.CheckBox chkUpdateInputJson;
        private System.Windows.Forms.ListView lvReadFor;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.CheckBox chkReadFor;
        private System.Windows.Forms.CheckBox chkReadOutputJson;
        private System.Windows.Forms.ComboBox cbReadBetweenColumn;
        private System.Windows.Forms.CheckBox chkRead;
        private System.Windows.Forms.CheckBox chkReadBetween;
        private System.Windows.Forms.CheckBox chkUpsert;
        private System.Windows.Forms.GroupBox gbHandCoded;
        private System.Windows.Forms.ListView lvHandCoded;
        private System.Windows.Forms.ColumnHeader chHandCodedName;
        private System.Windows.Forms.ColumnHeader chHandCodedReturns;
        private System.Windows.Forms.GroupBox gbDto;
        private System.Windows.Forms.GroupBox gbImplement;
        private System.Windows.Forms.CheckBox chkStateful;
        private System.Windows.Forms.CheckBox chkCloneable;
        private System.Windows.Forms.CheckBox chkHasWriteId;
        private System.Windows.Forms.CheckBox chkProperties;
        private System.Windows.Forms.ListView lvJsonFields;
        private System.Windows.Forms.ColumnHeader chJsonFieldColumn;
        private System.Windows.Forms.ColumnHeader chJsonFieldType;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnAddJsonField;
        private System.Windows.Forms.Button btnDelJsonField;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}