namespace ScriptOMatic
{
    partial class ReadForMaxEdit
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
            this.components = new System.ComponentModel.Container();
            this.lbColumn = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkReturnsList = new System.Windows.Forms.CheckBox();
            this.lvParameters = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsParameters = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiModeParameter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiModeNull = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiModeQualifier = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.cmsParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbColumn
            // 
            this.lbColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbColumn.FormattingEnabled = true;
            this.lbColumn.Location = new System.Drawing.Point(3, 3);
            this.lbColumn.Name = "lbColumn";
            this.lbColumn.Size = new System.Drawing.Size(140, 122);
            this.lbColumn.TabIndex = 28;
            this.lbColumn.SelectedIndexChanged += new System.EventHandler(this.lbColumn_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbColumn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lvParameters, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(293, 157);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.chkReturnsList);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 131);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(287, 23);
            this.flowLayoutPanel1.TabIndex = 30;
            // 
            // chkReturnsList
            // 
            this.chkReturnsList.AutoSize = true;
            this.chkReturnsList.Location = new System.Drawing.Point(3, 3);
            this.chkReturnsList.Name = "chkReturnsList";
            this.chkReturnsList.Size = new System.Drawing.Size(78, 17);
            this.chkReturnsList.TabIndex = 29;
            this.chkReturnsList.Text = "Returns list";
            this.chkReturnsList.UseVisualStyleBackColor = true;
            this.chkReturnsList.CheckedChanged += new System.EventHandler(this.chkReturnsList_CheckedChanged);
            // 
            // lvParameters
            // 
            this.lvParameters.CheckBoxes = true;
            this.lvParameters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chMode});
            this.lvParameters.ContextMenuStrip = this.cmsParameters;
            this.lvParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvParameters.FullRowSelect = true;
            this.lvParameters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvParameters.HideSelection = false;
            this.lvParameters.Location = new System.Drawing.Point(149, 3);
            this.lvParameters.Name = "lvParameters";
            this.lvParameters.Size = new System.Drawing.Size(141, 122);
            this.lvParameters.TabIndex = 27;
            this.lvParameters.UseCompatibleStateImageBehavior = false;
            this.lvParameters.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 81;
            // 
            // chMode
            // 
            this.chMode.Text = "Mode";
            this.chMode.Width = 39;
            // 
            // cmsParameters
            // 
            this.cmsParameters.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMode});
            this.cmsParameters.Name = "cmsParameters";
            this.cmsParameters.Size = new System.Drawing.Size(181, 48);
            // 
            // tsmiMode
            // 
            this.tsmiMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiModeParameter,
            this.tsmiModeNull,
            this.tsmiModeQualifier});
            this.tsmiMode.Name = "tsmiMode";
            this.tsmiMode.Size = new System.Drawing.Size(180, 22);
            this.tsmiMode.Text = "Mode";
            // 
            // tsmiModeParameter
            // 
            this.tsmiModeParameter.Name = "tsmiModeParameter";
            this.tsmiModeParameter.Size = new System.Drawing.Size(180, 22);
            this.tsmiModeParameter.Text = "Parameter";
            // 
            // tsmiModeNull
            // 
            this.tsmiModeNull.Name = "tsmiModeNull";
            this.tsmiModeNull.Size = new System.Drawing.Size(180, 22);
            this.tsmiModeNull.Text = "Null";
            // 
            // tsmiModeQualifier
            // 
            this.tsmiModeQualifier.Name = "tsmiModeQualifier";
            this.tsmiModeQualifier.Size = new System.Drawing.Size(180, 22);
            this.tsmiModeQualifier.Text = "Qualifier";
            // 
            // ReadForMaxEdit
            // 
            this.AcceptButton = null;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = null;
            this.ClientSize = new System.Drawing.Size(293, 187);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ReadForMaxEdit";
            this.ShowIcon = false;
            this.Text = "ReadForMax Editor";
            this.Load += new System.EventHandler(this.ReadForMaxEdit_Load);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.cmsParameters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox lbColumn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView lvParameters;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.CheckBox chkReturnsList;
        private System.Windows.Forms.ColumnHeader chMode;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip cmsParameters;
        private System.Windows.Forms.ToolStripMenuItem tsmiMode;
        private System.Windows.Forms.ToolStripMenuItem tsmiModeParameter;
        private System.Windows.Forms.ToolStripMenuItem tsmiModeNull;
        private System.Windows.Forms.ToolStripMenuItem tsmiModeQualifier;
    }
}