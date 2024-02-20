namespace ScriptOMatic.Pages
{
    partial class CrudSpPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test");
            this.cmLv = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.gbImplement = new System.Windows.Forms.GroupBox();
            this.chkStateful = new System.Windows.Forms.CheckBox();
            this.chkCloneable = new System.Windows.Forms.CheckBox();
            this.chkHasWriteId = new System.Windows.Forms.CheckBox();
            this.chkProperties = new System.Windows.Forms.CheckBox();
            this.flpDto = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lvInterfaces = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.chkRepoImplementUpsertRepo = new System.Windows.Forms.CheckBox();
            this.chkRepoImplementMultiUpsertRepo = new System.Windows.Forms.CheckBox();
            this.chkRepoFilters = new System.Windows.Forms.CheckBox();
            this.gbMatchable = new System.Windows.Forms.GroupBox();
            this.lvMatchableColumns = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkImplementMatchable = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudDecimalPlaces = new System.Windows.Forms.NumericUpDown();
            this.chkRoundDecimals = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkMatchIgnoreCase = new System.Windows.Forms.CheckBox();
            this.chkIgnoreWhitespace = new System.Windows.Forms.CheckBox();
            this.chkNormalizeLineBreaks = new System.Windows.Forms.CheckBox();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpOperations = new System.Windows.Forms.TabPage();
            this.flpOps = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpOperations = new System.Windows.Forms.TableLayoutPanel();
            this.chkIdIn = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblJsonIn = new System.Windows.Forms.Label();
            this.lblOp = new System.Windows.Forms.Label();
            this.chkCreate = new System.Windows.Forms.CheckBox();
            this.chkCreateInputJson = new System.Windows.Forms.CheckBox();
            this.chkCreateOutputJson = new System.Windows.Forms.CheckBox();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.chkUpdate = new System.Windows.Forms.CheckBox();
            this.chkUpdateInputJson = new System.Windows.Forms.CheckBox();
            this.chkReadOutputJson = new System.Windows.Forms.CheckBox();
            this.chkRead = new System.Windows.Forms.CheckBox();
            this.chkUpsert = new System.Windows.Forms.CheckBox();
            this.chkUpsertInputJson = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpReadFor = new System.Windows.Forms.TabPage();
            this.lvReadFor = new System.Windows.Forms.ListView();
            this.chForColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chForParameters = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpReadBetween = new System.Windows.Forms.TabPage();
            this.lvReadBetween = new System.Windows.Forms.ListView();
            this.chBetweenName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBetweenParams = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpReadForMax = new System.Windows.Forms.TabPage();
            this.lvMaxQueries = new System.Windows.Forms.ListView();
            this.chMaxMax = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMaxParams = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpDelFor = new System.Windows.Forms.TabPage();
            this.lvDelFor = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkDropUnused = new System.Windows.Forms.CheckBox();
            this.gbHandCoded = new System.Windows.Forms.GroupBox();
            this.lvHandCoded = new System.Windows.Forms.ListView();
            this.chHandCodedName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnImportClassDef = new System.Windows.Forms.Button();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.tpAttached = new System.Windows.Forms.TabPage();
            this.flpExtras = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.lvAggregates = new System.Windows.Forms.ListView();
            this.chAggTable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAggAlias = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAggType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.lvJsonFields = new System.Windows.Forms.ListView();
            this.chJsonFieldColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chJsonFieldType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.cmLv.SuspendLayout();
            this.gbImplement.SuspendLayout();
            this.flpDto.SuspendLayout();
            this.gbMatchable.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDecimalPlaces)).BeginInit();
            this.tcMain.SuspendLayout();
            this.tpOperations.SuspendLayout();
            this.flpOps.SuspendLayout();
            this.tlpOperations.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpReadFor.SuspendLayout();
            this.tpReadBetween.SuspendLayout();
            this.tpReadForMax.SuspendLayout();
            this.tpDelFor.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbHandCoded.SuspendLayout();
            this.tpOptions.SuspendLayout();
            this.tpAttached.SuspendLayout();
            this.flpExtras.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxtPluralName
            // 
            this.ctxtPluralName.Location = new System.Drawing.Point(166, 31);
            // 
            // cmLv
            // 
            this.cmLv.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd,
            this.tsmiEdit,
            this.tsmiDelete});
            this.cmLv.Name = "cmLv";
            this.cmLv.Size = new System.Drawing.Size(108, 70);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(107, 22);
            this.tsmiAdd.Text = "Add";
            this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(107, 22);
            this.tsmiEdit.Text = "Edit";
            this.tsmiEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(107, 22);
            this.tsmiDelete.Text = "Delete";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // gbImplement
            // 
            this.gbImplement.Controls.Add(this.chkStateful);
            this.gbImplement.Controls.Add(this.chkCloneable);
            this.gbImplement.Controls.Add(this.chkHasWriteId);
            this.gbImplement.Location = new System.Drawing.Point(2, 39);
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
            this.chkStateful.CheckedChanged += new System.EventHandler(this.SomethingChanged);
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
            this.chkCloneable.CheckedChanged += new System.EventHandler(this.SomethingChanged);
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
            this.chkHasWriteId.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkProperties
            // 
            this.chkProperties.AutoSize = true;
            this.chkProperties.Checked = true;
            this.chkProperties.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProperties.Location = new System.Drawing.Point(2, 18);
            this.chkProperties.Margin = new System.Windows.Forms.Padding(2);
            this.chkProperties.Name = "chkProperties";
            this.chkProperties.Size = new System.Drawing.Size(170, 17);
            this.chkProperties.TabIndex = 9;
            this.chkProperties.Text = "Use properties instead of fields";
            this.chkProperties.UseVisualStyleBackColor = true;
            this.chkProperties.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // flpDto
            // 
            this.flpDto.Controls.Add(this.label6);
            this.flpDto.Controls.Add(this.chkProperties);
            this.flpDto.Controls.Add(this.gbImplement);
            this.flpDto.Controls.Add(this.label7);
            this.flpDto.Controls.Add(this.lvInterfaces);
            this.flpDto.Controls.Add(this.label5);
            this.flpDto.Controls.Add(this.chkRepoImplementUpsertRepo);
            this.flpDto.Controls.Add(this.chkRepoImplementMultiUpsertRepo);
            this.flpDto.Controls.Add(this.chkRepoFilters);
            this.flpDto.Controls.Add(this.gbMatchable);
            this.flpDto.Controls.Add(this.panel1);
            this.flpDto.Controls.Add(this.chkMatchIgnoreCase);
            this.flpDto.Controls.Add(this.chkIgnoreWhitespace);
            this.flpDto.Controls.Add(this.chkNormalizeLineBreaks);
            this.flpDto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDto.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpDto.Location = new System.Drawing.Point(3, 3);
            this.flpDto.Margin = new System.Windows.Forms.Padding(0);
            this.flpDto.Name = "flpDto";
            this.flpDto.Size = new System.Drawing.Size(245, 860);
            this.flpDto.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 16);
            this.label6.TabIndex = 29;
            this.label6.Text = "Data transfer object";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Interfaces to implement";
            // 
            // lvInterfaces
            // 
            this.lvInterfaces.CheckBoxes = true;
            this.lvInterfaces.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5});
            this.lvInterfaces.FullRowSelect = true;
            this.lvInterfaces.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvInterfaces.HideSelection = false;
            listViewItem1.Checked = true;
            listViewItem1.StateImageIndex = 1;
            this.lvInterfaces.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvInterfaces.Location = new System.Drawing.Point(3, 121);
            this.lvInterfaces.Name = "lvInterfaces";
            this.lvInterfaces.Size = new System.Drawing.Size(212, 111);
            this.lvInterfaces.TabIndex = 27;
            this.lvInterfaces.UseCompatibleStateImageBehavior = false;
            this.lvInterfaces.View = System.Windows.Forms.View.Details;
            this.lvInterfaces.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvItemChecked);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Width = 100;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 16);
            this.label5.TabIndex = 28;
            this.label5.Text = "Repository";
            // 
            // chkRepoImplementUpsertRepo
            // 
            this.chkRepoImplementUpsertRepo.AutoSize = true;
            this.chkRepoImplementUpsertRepo.Location = new System.Drawing.Point(3, 254);
            this.chkRepoImplementUpsertRepo.Name = "chkRepoImplementUpsertRepo";
            this.chkRepoImplementUpsertRepo.Size = new System.Drawing.Size(137, 17);
            this.chkRepoImplementUpsertRepo.TabIndex = 0;
            this.chkRepoImplementUpsertRepo.Text = "Implement IUpsertRepo";
            this.chkRepoImplementUpsertRepo.UseVisualStyleBackColor = true;
            this.chkRepoImplementUpsertRepo.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkRepoImplementMultiUpsertRepo
            // 
            this.chkRepoImplementMultiUpsertRepo.AutoSize = true;
            this.chkRepoImplementMultiUpsertRepo.Location = new System.Drawing.Point(3, 277);
            this.chkRepoImplementMultiUpsertRepo.Name = "chkRepoImplementMultiUpsertRepo";
            this.chkRepoImplementMultiUpsertRepo.Size = new System.Drawing.Size(159, 17);
            this.chkRepoImplementMultiUpsertRepo.TabIndex = 31;
            this.chkRepoImplementMultiUpsertRepo.Text = "Implement IMultiUpsertRepo";
            this.chkRepoImplementMultiUpsertRepo.UseVisualStyleBackColor = true;
            this.chkRepoImplementMultiUpsertRepo.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkRepoFilters
            // 
            this.chkRepoFilters.AutoSize = true;
            this.chkRepoFilters.Location = new System.Drawing.Point(3, 300);
            this.chkRepoFilters.Name = "chkRepoFilters";
            this.chkRepoFilters.Size = new System.Drawing.Size(81, 17);
            this.chkRepoFilters.TabIndex = 36;
            this.chkRepoFilters.Text = "Add filtering";
            this.chkRepoFilters.UseVisualStyleBackColor = true;
            // 
            // gbMatchable
            // 
            this.gbMatchable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMatchable.Controls.Add(this.lvMatchableColumns);
            this.gbMatchable.Controls.Add(this.chkImplementMatchable);
            this.gbMatchable.Location = new System.Drawing.Point(3, 323);
            this.gbMatchable.Name = "gbMatchable";
            this.gbMatchable.Size = new System.Drawing.Size(227, 153);
            this.gbMatchable.TabIndex = 32;
            this.gbMatchable.TabStop = false;
            this.gbMatchable.Text = "IMatchable";
            // 
            // lvMatchableColumns
            // 
            this.lvMatchableColumns.CheckBoxes = true;
            this.lvMatchableColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lvMatchableColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMatchableColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMatchableColumns.HideSelection = false;
            this.lvMatchableColumns.Location = new System.Drawing.Point(3, 33);
            this.lvMatchableColumns.Name = "lvMatchableColumns";
            this.lvMatchableColumns.Size = new System.Drawing.Size(221, 117);
            this.lvMatchableColumns.TabIndex = 0;
            this.lvMatchableColumns.UseCompatibleStateImageBehavior = false;
            this.lvMatchableColumns.View = System.Windows.Forms.View.Details;
            this.lvMatchableColumns.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.SomethingChanged);
            this.lvMatchableColumns.SelectedIndexChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 180;
            // 
            // chkImplementMatchable
            // 
            this.chkImplementMatchable.AutoSize = true;
            this.chkImplementMatchable.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkImplementMatchable.Location = new System.Drawing.Point(3, 16);
            this.chkImplementMatchable.Name = "chkImplementMatchable";
            this.chkImplementMatchable.Size = new System.Drawing.Size(221, 17);
            this.chkImplementMatchable.TabIndex = 1;
            this.chkImplementMatchable.Text = "Implement";
            this.chkImplementMatchable.UseVisualStyleBackColor = true;
            this.chkImplementMatchable.CheckedChanged += new System.EventHandler(this.chkImplementMatchable_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nudDecimalPlaces);
            this.panel1.Controls.Add(this.chkRoundDecimals);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(3, 482);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 29);
            this.panel1.TabIndex = 35;
            // 
            // nudDecimalPlaces
            // 
            this.nudDecimalPlaces.Enabled = false;
            this.nudDecimalPlaces.Location = new System.Drawing.Point(144, 2);
            this.nudDecimalPlaces.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDecimalPlaces.Name = "nudDecimalPlaces";
            this.nudDecimalPlaces.Size = new System.Drawing.Size(40, 20);
            this.nudDecimalPlaces.TabIndex = 34;
            this.nudDecimalPlaces.ValueChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkRoundDecimals
            // 
            this.chkRoundDecimals.AutoSize = true;
            this.chkRoundDecimals.Location = new System.Drawing.Point(4, 3);
            this.chkRoundDecimals.Name = "chkRoundDecimals";
            this.chkRoundDecimals.Size = new System.Drawing.Size(143, 17);
            this.chkRoundDecimals.TabIndex = 33;
            this.chkRoundDecimals.Text = "Round decimal values to";
            this.chkRoundDecimals.UseVisualStyleBackColor = true;
            this.chkRoundDecimals.CheckedChanged += new System.EventHandler(this.chkRoundDecimals_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "places";
            // 
            // chkMatchIgnoreCase
            // 
            this.chkMatchIgnoreCase.AutoSize = true;
            this.chkMatchIgnoreCase.Location = new System.Drawing.Point(3, 517);
            this.chkMatchIgnoreCase.Name = "chkMatchIgnoreCase";
            this.chkMatchIgnoreCase.Size = new System.Drawing.Size(83, 17);
            this.chkMatchIgnoreCase.TabIndex = 37;
            this.chkMatchIgnoreCase.Text = "Ignore Case";
            this.chkMatchIgnoreCase.UseVisualStyleBackColor = true;
            this.chkMatchIgnoreCase.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkIgnoreWhitespace
            // 
            this.chkIgnoreWhitespace.AutoSize = true;
            this.chkIgnoreWhitespace.Location = new System.Drawing.Point(3, 540);
            this.chkIgnoreWhitespace.Name = "chkIgnoreWhitespace";
            this.chkIgnoreWhitespace.Size = new System.Drawing.Size(116, 17);
            this.chkIgnoreWhitespace.TabIndex = 38;
            this.chkIgnoreWhitespace.Text = "Ignore Whitespace";
            this.chkIgnoreWhitespace.UseVisualStyleBackColor = true;
            this.chkIgnoreWhitespace.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkNormalizeLineBreaks
            // 
            this.chkNormalizeLineBreaks.AutoSize = true;
            this.chkNormalizeLineBreaks.Location = new System.Drawing.Point(3, 563);
            this.chkNormalizeLineBreaks.Name = "chkNormalizeLineBreaks";
            this.chkNormalizeLineBreaks.Size = new System.Drawing.Size(131, 17);
            this.chkNormalizeLineBreaks.TabIndex = 39;
            this.chkNormalizeLineBreaks.Text = "Normalize Line Breaks";
            this.chkNormalizeLineBreaks.UseVisualStyleBackColor = true;
            this.chkNormalizeLineBreaks.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpOperations);
            this.tcMain.Controls.Add(this.tpOptions);
            this.tcMain.Controls.Add(this.tpAttached);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(259, 892);
            this.tcMain.TabIndex = 34;
            // 
            // tpOperations
            // 
            this.tpOperations.Controls.Add(this.flpOps);
            this.tpOperations.Location = new System.Drawing.Point(4, 22);
            this.tpOperations.Name = "tpOperations";
            this.tpOperations.Padding = new System.Windows.Forms.Padding(3);
            this.tpOperations.Size = new System.Drawing.Size(251, 866);
            this.tpOperations.TabIndex = 0;
            this.tpOperations.Text = "Operations";
            this.tpOperations.UseVisualStyleBackColor = true;
            // 
            // flpOps
            // 
            this.flpOps.AutoSize = true;
            this.flpOps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpOps.Controls.Add(this.tlpOperations);
            this.flpOps.Controls.Add(this.tabControl1);
            this.flpOps.Controls.Add(this.flowLayoutPanel1);
            this.flpOps.Controls.Add(this.gbHandCoded);
            this.flpOps.Controls.Add(this.btnImportClassDef);
            this.flpOps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpOps.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpOps.Location = new System.Drawing.Point(3, 3);
            this.flpOps.Margin = new System.Windows.Forms.Padding(0);
            this.flpOps.Name = "flpOps";
            this.flpOps.Size = new System.Drawing.Size(245, 860);
            this.flpOps.TabIndex = 21;
            this.flpOps.WrapContents = false;
            // 
            // tlpOperations
            // 
            this.tlpOperations.AutoSize = true;
            this.tlpOperations.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpOperations.ColumnCount = 3;
            this.tlpOperations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOperations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tlpOperations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tlpOperations.Controls.Add(this.chkIdIn, 0, 9);
            this.tlpOperations.Controls.Add(this.label1, 2, 0);
            this.tlpOperations.Controls.Add(this.lblJsonIn, 1, 0);
            this.tlpOperations.Controls.Add(this.lblOp, 0, 0);
            this.tlpOperations.Controls.Add(this.chkCreate, 0, 1);
            this.tlpOperations.Controls.Add(this.chkCreateInputJson, 1, 1);
            this.tlpOperations.Controls.Add(this.chkCreateOutputJson, 2, 1);
            this.tlpOperations.Controls.Add(this.chkDelete, 0, 4);
            this.tlpOperations.Controls.Add(this.chkUpdate, 0, 3);
            this.tlpOperations.Controls.Add(this.chkUpdateInputJson, 1, 3);
            this.tlpOperations.Controls.Add(this.chkReadOutputJson, 2, 2);
            this.tlpOperations.Controls.Add(this.chkRead, 0, 2);
            this.tlpOperations.Controls.Add(this.chkUpsert, 0, 7);
            this.tlpOperations.Controls.Add(this.chkUpsertInputJson, 1, 7);
            this.tlpOperations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOperations.Location = new System.Drawing.Point(0, 0);
            this.tlpOperations.Margin = new System.Windows.Forms.Padding(0);
            this.tlpOperations.Name = "tlpOperations";
            this.tlpOperations.RowCount = 10;
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperations.Size = new System.Drawing.Size(239, 168);
            this.tlpOperations.TabIndex = 17;
            // 
            // chkIdIn
            // 
            this.chkIdIn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIdIn.AutoSize = true;
            this.chkIdIn.Location = new System.Drawing.Point(3, 148);
            this.chkIdIn.Name = "chkIdIn";
            this.chkIdIn.Size = new System.Drawing.Size(44, 17);
            this.chkIdIn.TabIndex = 23;
            this.chkIdIn.Text = "IdIn";
            this.chkIdIn.UseVisualStyleBackColor = true;
            this.chkIdIn.Click += new System.EventHandler(this.SomethingChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(172, 0);
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
            this.lblJsonIn.Location = new System.Drawing.Point(121, 0);
            this.lblJsonIn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblJsonIn.Name = "lblJsonIn";
            this.lblJsonIn.Size = new System.Drawing.Size(41, 30);
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
            this.chkCreate.Location = new System.Drawing.Point(3, 33);
            this.chkCreate.Name = "chkCreate";
            this.chkCreate.Size = new System.Drawing.Size(57, 17);
            this.chkCreate.TabIndex = 2;
            this.chkCreate.Text = "Create";
            this.chkCreate.UseVisualStyleBackColor = true;
            this.chkCreate.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkCreateInputJson
            // 
            this.chkCreateInputJson.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkCreateInputJson.AutoSize = true;
            this.chkCreateInputJson.Location = new System.Drawing.Point(137, 34);
            this.chkCreateInputJson.Name = "chkCreateInputJson";
            this.chkCreateInputJson.Size = new System.Drawing.Size(15, 14);
            this.chkCreateInputJson.TabIndex = 2;
            this.chkCreateInputJson.UseVisualStyleBackColor = true;
            this.chkCreateInputJson.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkCreateOutputJson
            // 
            this.chkCreateOutputJson.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkCreateOutputJson.AutoSize = true;
            this.chkCreateOutputJson.Enabled = false;
            this.chkCreateOutputJson.Location = new System.Drawing.Point(197, 34);
            this.chkCreateOutputJson.Name = "chkCreateOutputJson";
            this.chkCreateOutputJson.Size = new System.Drawing.Size(15, 14);
            this.chkCreateOutputJson.TabIndex = 15;
            this.chkCreateOutputJson.UseVisualStyleBackColor = true;
            this.chkCreateOutputJson.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkDelete
            // 
            this.chkDelete.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(3, 102);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(57, 17);
            this.chkDelete.TabIndex = 5;
            this.chkDelete.Text = "Delete";
            this.chkDelete.UseVisualStyleBackColor = true;
            this.chkDelete.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkUpdate
            // 
            this.chkUpdate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkUpdate.AutoSize = true;
            this.chkUpdate.Location = new System.Drawing.Point(3, 79);
            this.chkUpdate.Name = "chkUpdate";
            this.chkUpdate.Size = new System.Drawing.Size(61, 17);
            this.chkUpdate.TabIndex = 4;
            this.chkUpdate.Text = "Update";
            this.chkUpdate.UseVisualStyleBackColor = true;
            this.chkUpdate.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkUpdateInputJson
            // 
            this.chkUpdateInputJson.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkUpdateInputJson.AutoSize = true;
            this.chkUpdateInputJson.Location = new System.Drawing.Point(137, 80);
            this.chkUpdateInputJson.Name = "chkUpdateInputJson";
            this.chkUpdateInputJson.Size = new System.Drawing.Size(15, 14);
            this.chkUpdateInputJson.TabIndex = 16;
            this.chkUpdateInputJson.UseVisualStyleBackColor = true;
            this.chkUpdateInputJson.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkReadOutputJson
            // 
            this.chkReadOutputJson.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkReadOutputJson.AutoSize = true;
            this.chkReadOutputJson.Location = new System.Drawing.Point(197, 57);
            this.chkReadOutputJson.Name = "chkReadOutputJson";
            this.chkReadOutputJson.Size = new System.Drawing.Size(15, 14);
            this.chkReadOutputJson.TabIndex = 3;
            this.chkReadOutputJson.UseVisualStyleBackColor = true;
            this.chkReadOutputJson.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkRead
            // 
            this.chkRead.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkRead.AutoSize = true;
            this.chkRead.Location = new System.Drawing.Point(3, 56);
            this.chkRead.Name = "chkRead";
            this.chkRead.Size = new System.Drawing.Size(52, 17);
            this.chkRead.TabIndex = 3;
            this.chkRead.Text = "Read";
            this.chkRead.UseVisualStyleBackColor = true;
            this.chkRead.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkUpsert
            // 
            this.chkUpsert.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkUpsert.AutoSize = true;
            this.chkUpsert.Location = new System.Drawing.Point(3, 125);
            this.chkUpsert.Name = "chkUpsert";
            this.chkUpsert.Size = new System.Drawing.Size(57, 17);
            this.chkUpsert.TabIndex = 21;
            this.chkUpsert.Text = "Upsert";
            this.chkUpsert.UseVisualStyleBackColor = true;
            this.chkUpsert.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkUpsertInputJson
            // 
            this.chkUpsertInputJson.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkUpsertInputJson.AutoSize = true;
            this.chkUpsertInputJson.Location = new System.Drawing.Point(137, 126);
            this.chkUpsertInputJson.Name = "chkUpsertInputJson";
            this.chkUpsertInputJson.Size = new System.Drawing.Size(15, 14);
            this.chkUpsertInputJson.TabIndex = 24;
            this.chkUpsertInputJson.UseVisualStyleBackColor = true;
            this.chkUpsertInputJson.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpReadFor);
            this.tabControl1.Controls.Add(this.tpReadBetween);
            this.tabControl1.Controls.Add(this.tpReadForMax);
            this.tabControl1.Controls.Add(this.tpDelFor);
            this.tabControl1.Location = new System.Drawing.Point(3, 171);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(233, 161);
            this.tabControl1.TabIndex = 22;
            this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvKeyDown);
            // 
            // tpReadFor
            // 
            this.tpReadFor.Controls.Add(this.lvReadFor);
            this.tpReadFor.Location = new System.Drawing.Point(4, 22);
            this.tpReadFor.Name = "tpReadFor";
            this.tpReadFor.Size = new System.Drawing.Size(225, 135);
            this.tpReadFor.TabIndex = 0;
            this.tpReadFor.Text = "R: For";
            this.tpReadFor.UseVisualStyleBackColor = true;
            // 
            // lvReadFor
            // 
            this.lvReadFor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chForColumn,
            this.chForParameters});
            this.lvReadFor.ContextMenuStrip = this.cmLv;
            this.lvReadFor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvReadFor.FullRowSelect = true;
            this.lvReadFor.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvReadFor.HideSelection = false;
            this.lvReadFor.Location = new System.Drawing.Point(0, 0);
            this.lvReadFor.Margin = new System.Windows.Forms.Padding(0);
            this.lvReadFor.Name = "lvReadFor";
            this.lvReadFor.Size = new System.Drawing.Size(225, 135);
            this.lvReadFor.TabIndex = 25;
            this.lvReadFor.Tag = "GetFor";
            this.lvReadFor.UseCompatibleStateImageBehavior = false;
            this.lvReadFor.View = System.Windows.Forms.View.Details;
            this.lvReadFor.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvItemChecked);
            this.lvReadFor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvKeyDown);
            this.lvReadFor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMouseDoubleClick);
            // 
            // chForColumn
            // 
            this.chForColumn.Text = "For";
            // 
            // chForParameters
            // 
            this.chForParameters.Text = "Parameters";
            this.chForParameters.Width = 135;
            // 
            // tpReadBetween
            // 
            this.tpReadBetween.Controls.Add(this.lvReadBetween);
            this.tpReadBetween.Location = new System.Drawing.Point(4, 22);
            this.tpReadBetween.Name = "tpReadBetween";
            this.tpReadBetween.Size = new System.Drawing.Size(225, 135);
            this.tpReadBetween.TabIndex = 1;
            this.tpReadBetween.Text = "R: Between";
            this.tpReadBetween.UseVisualStyleBackColor = true;
            // 
            // lvReadBetween
            // 
            this.lvReadBetween.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chBetweenName,
            this.chBetweenParams});
            this.lvReadBetween.ContextMenuStrip = this.cmLv;
            this.lvReadBetween.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvReadBetween.FullRowSelect = true;
            this.lvReadBetween.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvReadBetween.HideSelection = false;
            this.lvReadBetween.Location = new System.Drawing.Point(0, 0);
            this.lvReadBetween.Name = "lvReadBetween";
            this.lvReadBetween.Size = new System.Drawing.Size(225, 135);
            this.lvReadBetween.TabIndex = 26;
            this.lvReadBetween.Tag = "GetBetween";
            this.lvReadBetween.UseCompatibleStateImageBehavior = false;
            this.lvReadBetween.View = System.Windows.Forms.View.Details;
            this.lvReadBetween.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvItemChecked);
            this.lvReadBetween.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvKeyDown);
            this.lvReadBetween.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMouseDoubleClick);
            // 
            // chBetweenName
            // 
            this.chBetweenName.Text = "Between";
            this.chBetweenName.Width = 62;
            // 
            // chBetweenParams
            // 
            this.chBetweenParams.Text = "Parameters";
            this.chBetweenParams.Width = 132;
            // 
            // tpReadForMax
            // 
            this.tpReadForMax.Controls.Add(this.lvMaxQueries);
            this.tpReadForMax.Location = new System.Drawing.Point(4, 22);
            this.tpReadForMax.Name = "tpReadForMax";
            this.tpReadForMax.Size = new System.Drawing.Size(225, 135);
            this.tpReadForMax.TabIndex = 2;
            this.tpReadForMax.Text = "R: Max";
            this.tpReadForMax.UseVisualStyleBackColor = true;
            // 
            // lvMaxQueries
            // 
            this.lvMaxQueries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMaxMax,
            this.chMaxParams});
            this.lvMaxQueries.ContextMenuStrip = this.cmLv;
            this.lvMaxQueries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMaxQueries.FullRowSelect = true;
            this.lvMaxQueries.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMaxQueries.HideSelection = false;
            this.lvMaxQueries.Location = new System.Drawing.Point(0, 0);
            this.lvMaxQueries.Name = "lvMaxQueries";
            this.lvMaxQueries.Size = new System.Drawing.Size(225, 135);
            this.lvMaxQueries.TabIndex = 29;
            this.lvMaxQueries.Tag = "GetForMax";
            this.lvMaxQueries.UseCompatibleStateImageBehavior = false;
            this.lvMaxQueries.View = System.Windows.Forms.View.Details;
            this.lvMaxQueries.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvItemChecked);
            this.lvMaxQueries.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvKeyDown);
            this.lvMaxQueries.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMouseDoubleClick);
            // 
            // chMaxMax
            // 
            this.chMaxMax.Text = "Max";
            this.chMaxMax.Width = 65;
            // 
            // chMaxParams
            // 
            this.chMaxParams.Text = "Parameters";
            this.chMaxParams.Width = 132;
            // 
            // tpDelFor
            // 
            this.tpDelFor.Controls.Add(this.lvDelFor);
            this.tpDelFor.Location = new System.Drawing.Point(4, 22);
            this.tpDelFor.Name = "tpDelFor";
            this.tpDelFor.Size = new System.Drawing.Size(225, 135);
            this.tpDelFor.TabIndex = 3;
            this.tpDelFor.Text = "D: For";
            this.tpDelFor.UseVisualStyleBackColor = true;
            // 
            // lvDelFor
            // 
            this.lvDelFor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvDelFor.ContextMenuStrip = this.cmLv;
            this.lvDelFor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDelFor.FullRowSelect = true;
            this.lvDelFor.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDelFor.HideSelection = false;
            this.lvDelFor.Location = new System.Drawing.Point(0, 0);
            this.lvDelFor.Name = "lvDelFor";
            this.lvDelFor.Size = new System.Drawing.Size(225, 135);
            this.lvDelFor.TabIndex = 30;
            this.lvDelFor.Tag = "DelFor";
            this.lvDelFor.UseCompatibleStateImageBehavior = false;
            this.lvDelFor.View = System.Windows.Forms.View.Details;
            this.lvDelFor.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvItemChecked);
            this.lvDelFor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvKeyDown);
            this.lvDelFor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Max";
            this.columnHeader1.Width = 65;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Parameters";
            this.columnHeader2.Width = 132;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chkDropUnused);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 335);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(219, 28);
            this.flowLayoutPanel1.TabIndex = 21;
            // 
            // chkDropUnused
            // 
            this.chkDropUnused.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkDropUnused.AutoSize = true;
            this.chkDropUnused.Location = new System.Drawing.Point(3, 3);
            this.chkDropUnused.Name = "chkDropUnused";
            this.chkDropUnused.Size = new System.Drawing.Size(87, 17);
            this.chkDropUnused.TabIndex = 18;
            this.chkDropUnused.Text = "Drop unused";
            this.chkDropUnused.UseVisualStyleBackColor = true;
            this.chkDropUnused.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // gbHandCoded
            // 
            this.gbHandCoded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHandCoded.Controls.Add(this.lvHandCoded);
            this.gbHandCoded.Location = new System.Drawing.Point(3, 366);
            this.gbHandCoded.Name = "gbHandCoded";
            this.gbHandCoded.Size = new System.Drawing.Size(233, 112);
            this.gbHandCoded.TabIndex = 19;
            this.gbHandCoded.TabStop = false;
            this.gbHandCoded.Text = "Hand coded procedures";
            // 
            // lvHandCoded
            // 
            this.lvHandCoded.CheckBoxes = true;
            this.lvHandCoded.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chHandCodedName});
            this.lvHandCoded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvHandCoded.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvHandCoded.HideSelection = false;
            this.lvHandCoded.Location = new System.Drawing.Point(3, 16);
            this.lvHandCoded.Name = "lvHandCoded";
            this.lvHandCoded.Size = new System.Drawing.Size(227, 93);
            this.lvHandCoded.TabIndex = 0;
            this.lvHandCoded.UseCompatibleStateImageBehavior = false;
            this.lvHandCoded.View = System.Windows.Forms.View.Details;
            this.lvHandCoded.DoubleClick += new System.EventHandler(this.lvHandCoded_DoubleClick);
            this.lvHandCoded.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvKeyDown);
            this.lvHandCoded.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMouseDoubleClick);
            // 
            // chHandCodedName
            // 
            this.chHandCodedName.Text = "Name";
            this.chHandCodedName.Width = 180;
            // 
            // btnImportClassDef
            // 
            this.btnImportClassDef.Location = new System.Drawing.Point(3, 484);
            this.btnImportClassDef.Name = "btnImportClassDef";
            this.btnImportClassDef.Size = new System.Drawing.Size(121, 24);
            this.btnImportClassDef.TabIndex = 23;
            this.btnImportClassDef.Text = "Import Class defintion";
            this.btnImportClassDef.UseVisualStyleBackColor = true;
            this.btnImportClassDef.Click += new System.EventHandler(this.btnImportClassDef_Click);
            // 
            // tpOptions
            // 
            this.tpOptions.Controls.Add(this.flpDto);
            this.tpOptions.Location = new System.Drawing.Point(4, 22);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptions.Size = new System.Drawing.Size(251, 866);
            this.tpOptions.TabIndex = 1;
            this.tpOptions.Text = "Options";
            this.tpOptions.UseVisualStyleBackColor = true;
            // 
            // tpAttached
            // 
            this.tpAttached.Controls.Add(this.flpExtras);
            this.tpAttached.Location = new System.Drawing.Point(4, 22);
            this.tpAttached.Name = "tpAttached";
            this.tpAttached.Size = new System.Drawing.Size(251, 866);
            this.tpAttached.TabIndex = 3;
            this.tpAttached.Text = "Attached";
            this.tpAttached.UseVisualStyleBackColor = true;
            // 
            // flpExtras
            // 
            this.flpExtras.Controls.Add(this.label2);
            this.flpExtras.Controls.Add(this.lvAggregates);
            this.flpExtras.Controls.Add(this.label3);
            this.flpExtras.Controls.Add(this.lvJsonFields);
            this.flpExtras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpExtras.Location = new System.Drawing.Point(0, 0);
            this.flpExtras.Name = "flpExtras";
            this.flpExtras.Size = new System.Drawing.Size(251, 866);
            this.flpExtras.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Aggregates";
            // 
            // lvAggregates
            // 
            this.lvAggregates.CheckBoxes = true;
            this.lvAggregates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAggTable,
            this.chAggAlias,
            this.chAggType});
            this.lvAggregates.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvAggregates.HideSelection = false;
            this.lvAggregates.Location = new System.Drawing.Point(3, 19);
            this.lvAggregates.Name = "lvAggregates";
            this.lvAggregates.Size = new System.Drawing.Size(237, 120);
            this.lvAggregates.TabIndex = 3;
            this.lvAggregates.UseCompatibleStateImageBehavior = false;
            this.lvAggregates.View = System.Windows.Forms.View.Details;
            this.lvAggregates.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.SomethingChanged);
            this.lvAggregates.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvAggregates_MouseDoubleClick);
            // 
            // chAggTable
            // 
            this.chAggTable.Text = "Table";
            this.chAggTable.Width = 100;
            // 
            // chAggAlias
            // 
            this.chAggAlias.Text = "Alias";
            this.chAggAlias.Width = 75;
            // 
            // chAggType
            // 
            this.chAggType.Text = "Type";
            this.chAggType.Width = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 16);
            this.label3.TabIndex = 24;
            this.label3.Text = "JSON Fields";
            // 
            // lvJsonFields
            // 
            this.lvJsonFields.CheckBoxes = true;
            this.lvJsonFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chJsonFieldColumn,
            this.chJsonFieldType});
            this.lvJsonFields.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvJsonFields.HideSelection = false;
            this.lvJsonFields.Location = new System.Drawing.Point(3, 161);
            this.lvJsonFields.Name = "lvJsonFields";
            this.lvJsonFields.Size = new System.Drawing.Size(237, 120);
            this.lvJsonFields.TabIndex = 25;
            this.lvJsonFields.UseCompatibleStateImageBehavior = false;
            this.lvJsonFields.View = System.Windows.Forms.View.Details;
            this.lvJsonFields.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.SomethingChanged);
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
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.Controls.Add(this.tcMain);
            this.pnlMain.Location = new System.Drawing.Point(0, 57);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(259, 892);
            this.pnlMain.TabIndex = 35;
            // 
            // CrudSpPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CrudSpPage";
            this.Size = new System.Drawing.Size(259, 949);
            this.Load += new System.EventHandler(this.CrudSPPage_Load);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.Controls.SetChildIndex(this.ctxtSingularName, 0);
            this.Controls.SetChildIndex(this.ctxtPluralName, 0);
            this.cmLv.ResumeLayout(false);
            this.gbImplement.ResumeLayout(false);
            this.gbImplement.PerformLayout();
            this.flpDto.ResumeLayout(false);
            this.flpDto.PerformLayout();
            this.gbMatchable.ResumeLayout(false);
            this.gbMatchable.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDecimalPlaces)).EndInit();
            this.tcMain.ResumeLayout(false);
            this.tpOperations.ResumeLayout(false);
            this.tpOperations.PerformLayout();
            this.flpOps.ResumeLayout(false);
            this.flpOps.PerformLayout();
            this.tlpOperations.ResumeLayout(false);
            this.tlpOperations.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpReadFor.ResumeLayout(false);
            this.tpReadBetween.ResumeLayout(false);
            this.tpReadForMax.ResumeLayout(false);
            this.tpDelFor.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.gbHandCoded.ResumeLayout(false);
            this.tpOptions.ResumeLayout(false);
            this.tpAttached.ResumeLayout(false);
            this.flpExtras.ResumeLayout(false);
            this.flpExtras.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbImplement;
        private System.Windows.Forms.CheckBox chkStateful;
        private System.Windows.Forms.CheckBox chkCloneable;
        private System.Windows.Forms.CheckBox chkHasWriteId;
        private System.Windows.Forms.CheckBox chkProperties;
        private System.Windows.Forms.FlowLayoutPanel flpDto;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpOptions;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.CheckBox chkRepoImplementUpsertRepo;
        private System.Windows.Forms.ListView lvInterfaces;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ContextMenuStrip cmLv;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.TabPage tpAttached;
        private System.Windows.Forms.FlowLayoutPanel flpExtras;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvAggregates;
        private System.Windows.Forms.ColumnHeader chAggTable;
        private System.Windows.Forms.ColumnHeader chAggAlias;
        private System.Windows.Forms.ColumnHeader chAggType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvJsonFields;
        private System.Windows.Forms.ColumnHeader chJsonFieldColumn;
        private System.Windows.Forms.ColumnHeader chJsonFieldType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tpOperations;
        private System.Windows.Forms.FlowLayoutPanel flpOps;
        private System.Windows.Forms.TableLayoutPanel tlpOperations;
        private System.Windows.Forms.CheckBox chkIdIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblJsonIn;
        private System.Windows.Forms.Label lblOp;
        private System.Windows.Forms.CheckBox chkCreate;
        private System.Windows.Forms.CheckBox chkCreateInputJson;
        private System.Windows.Forms.CheckBox chkCreateOutputJson;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.CheckBox chkUpdate;
        private System.Windows.Forms.CheckBox chkUpdateInputJson;
        private System.Windows.Forms.CheckBox chkReadOutputJson;
        private System.Windows.Forms.CheckBox chkRead;
        private System.Windows.Forms.CheckBox chkUpsert;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpReadFor;
        private System.Windows.Forms.ListView lvReadFor;
        private System.Windows.Forms.ColumnHeader chForColumn;
        private System.Windows.Forms.ColumnHeader chForParameters;
        private System.Windows.Forms.TabPage tpReadBetween;
        private System.Windows.Forms.ListView lvReadBetween;
        private System.Windows.Forms.ColumnHeader chBetweenName;
        private System.Windows.Forms.ColumnHeader chBetweenParams;
        private System.Windows.Forms.TabPage tpReadForMax;
        private System.Windows.Forms.ListView lvMaxQueries;
        private System.Windows.Forms.ColumnHeader chMaxMax;
        private System.Windows.Forms.ColumnHeader chMaxParams;
        private System.Windows.Forms.TabPage tpDelFor;
        private System.Windows.Forms.ListView lvDelFor;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox chkDropUnused;
        private System.Windows.Forms.GroupBox gbHandCoded;
        private System.Windows.Forms.ListView lvHandCoded;
        private System.Windows.Forms.ColumnHeader chHandCodedName;
        private System.Windows.Forms.Button btnImportClassDef;
        private System.Windows.Forms.CheckBox chkUpsertInputJson;
        private System.Windows.Forms.CheckBox chkRepoImplementMultiUpsertRepo;
        private System.Windows.Forms.GroupBox gbMatchable;
        private System.Windows.Forms.ListView lvMatchableColumns;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.CheckBox chkImplementMatchable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nudDecimalPlaces;
        private System.Windows.Forms.CheckBox chkRoundDecimals;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkRepoFilters;
        private System.Windows.Forms.CheckBox chkMatchIgnoreCase;
        private System.Windows.Forms.CheckBox chkIgnoreWhitespace;
        private System.Windows.Forms.CheckBox chkNormalizeLineBreaks;
    }
}
