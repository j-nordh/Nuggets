namespace ScriptOMatic
{
    partial class SubQueryBuilderForm
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.gbProperties = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkHidden = new System.Windows.Forms.CheckBox();
            this.gbQueryProperties = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtxtQuery = new System.Windows.Forms.RichTextBox();
            this.llblTable = new UtilClasses.Winforms.LabelledLabel();
            this.ltxtAlias = new UtilClasses.Winforms.LabelledTextbox();
            this.lcmboSelectedTable = new UtilClasses.Winforms.LabelledCombo();
            this.ltxtQueryName = new UtilClasses.Winforms.LabelledTextbox();
            this.gbProperties.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbQueryProperties.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(168, 450);
            this.treeView1.TabIndex = 0;
            // 
            // gbProperties
            // 
            this.gbProperties.Controls.Add(this.flowLayoutPanel1);
            this.gbProperties.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbProperties.Location = new System.Drawing.Point(0, 0);
            this.gbProperties.Name = "gbProperties";
            this.gbProperties.Size = new System.Drawing.Size(106, 384);
            this.gbProperties.TabIndex = 1;
            this.gbProperties.TabStop = false;
            this.gbProperties.Text = "Table Properties";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.llblTable);
            this.flowLayoutPanel1.Controls.Add(this.ltxtAlias);
            this.flowLayoutPanel1.Controls.Add(this.chkHidden);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(100, 365);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // chkHidden
            // 
            this.chkHidden.AutoSize = true;
            this.chkHidden.Location = new System.Drawing.Point(3, 76);
            this.chkHidden.Name = "chkHidden";
            this.chkHidden.Size = new System.Drawing.Size(60, 17);
            this.chkHidden.TabIndex = 4;
            this.chkHidden.Text = "Hidden";
            this.chkHidden.UseVisualStyleBackColor = true;
            // 
            // gbQueryProperties
            // 
            this.gbQueryProperties.Controls.Add(this.lcmboSelectedTable);
            this.gbQueryProperties.Controls.Add(this.btnCancel);
            this.gbQueryProperties.Controls.Add(this.btnOK);
            this.gbQueryProperties.Controls.Add(this.ltxtQueryName);
            this.gbQueryProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbQueryProperties.Location = new System.Drawing.Point(168, 0);
            this.gbQueryProperties.Name = "gbQueryProperties";
            this.gbQueryProperties.Size = new System.Drawing.Size(632, 66);
            this.gbQueryProperties.TabIndex = 4;
            this.gbQueryProperties.TabStop = false;
            this.gbQueryProperties.Text = "Query properties";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(470, 27);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(551, 27);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtxtQuery);
            this.panel1.Controls.Add(this.gbProperties);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(168, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(632, 384);
            this.panel1.TabIndex = 5;
            // 
            // rtxtQuery
            // 
            this.rtxtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtQuery.Location = new System.Drawing.Point(106, 0);
            this.rtxtQuery.Name = "rtxtQuery";
            this.rtxtQuery.Size = new System.Drawing.Size(526, 384);
            this.rtxtQuery.TabIndex = 2;
            this.rtxtQuery.Text = "";
            // 
            // llblTable
            // 
            this.llblTable.AccessibleRole = System.Windows.Forms.AccessibleRole.Diagram;
            this.llblTable.AutoSize = true;
            this.llblTable.Caption = "Table";
            this.llblTable.Location = new System.Drawing.Point(3, 3);
            this.llblTable.Name = "llblTable";
            this.llblTable.Size = new System.Drawing.Size(34, 29);
            this.llblTable.TabIndex = 0;
            // 
            // ltxtAlias
            // 
            this.ltxtAlias.Caption = "Alias";
            this.ltxtAlias.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.ltxtAlias.Location = new System.Drawing.Point(3, 38);
            this.ltxtAlias.Multiline = false;
            this.ltxtAlias.Name = "ltxtAlias";
            this.ltxtAlias.ReadOnly = false;
            this.ltxtAlias.Size = new System.Drawing.Size(40, 32);
            this.ltxtAlias.TabIndex = 3;
            this.ltxtAlias.UnitText = "";
            this.ltxtAlias.UsePasswordChar = false;
            // 
            // lcmboSelectedTable
            // 
            this.lcmboSelectedTable.Caption = "Output table";
            this.lcmboSelectedTable.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.lcmboSelectedTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lcmboSelectedTable.Location = new System.Drawing.Point(306, 18);
            this.lcmboSelectedTable.Name = "lcmboSelectedTable";
            this.lcmboSelectedTable.SelectedObject = null;
            this.lcmboSelectedTable.Size = new System.Drawing.Size(150, 31);
            this.lcmboSelectedTable.TabIndex = 6;
            this.lcmboSelectedTable.SelectedIndexChanged += new System.Action(this.lcmboSelectedTable_SelectedIndexChanged);
            this.lcmboSelectedTable.Load += new System.EventHandler(this.lcmboSelectedTable_Load);
            // 
            // ltxtQueryName
            // 
            this.ltxtQueryName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ltxtQueryName.Caption = "Name";
            this.ltxtQueryName.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.ltxtQueryName.Location = new System.Drawing.Point(6, 18);
            this.ltxtQueryName.Multiline = false;
            this.ltxtQueryName.Name = "ltxtQueryName";
            this.ltxtQueryName.ReadOnly = false;
            this.ltxtQueryName.Size = new System.Drawing.Size(293, 32);
            this.ltxtQueryName.TabIndex = 3;
            this.ltxtQueryName.UnitText = "";
            this.ltxtQueryName.UsePasswordChar = false;
            this.ltxtQueryName.TextChanged += new System.EventHandler(this.ltxtQueryName_TextChanged);
            this.ltxtQueryName.Load += new System.EventHandler(this.ltxtQueryName_Load);
            // 
            // SubQueryBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbQueryProperties);
            this.Controls.Add(this.treeView1);
            this.Name = "SubQueryBuilderForm";
            this.Text = "SubQueryBuilderForm";
            this.Load += new System.EventHandler(this.SubQueryBuilderForm_Load);
            this.gbProperties.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.gbQueryProperties.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox gbProperties;
        private UtilClasses.Winforms.LabelledLabel llblTable;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private UtilClasses.Winforms.LabelledTextbox ltxtAlias;
        private System.Windows.Forms.CheckBox chkHidden;
        private System.Windows.Forms.GroupBox gbQueryProperties;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private UtilClasses.Winforms.LabelledTextbox ltxtQueryName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rtxtQuery;
        private UtilClasses.Winforms.LabelledCombo lcmboSelectedTable;
    }
}