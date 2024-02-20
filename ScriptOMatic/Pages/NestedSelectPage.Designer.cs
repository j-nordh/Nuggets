namespace ScriptOMatic.Pages
{
    partial class NestedSelectPage
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtMasterPrefix = new System.Windows.Forms.TextBox();
            this.lstvChildren = new System.Windows.Forms.ListView();
            this.chNestedTable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNestedPrefix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.txtMasterTable = new System.Windows.Forms.TextBox();
            this.txtRootName = new System.Windows.Forms.TextBox();
            this.txtElementName = new System.Windows.Forms.TextBox();
            this.chkForXml = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Master prefix";
            // 
            // txtMasterPrefix
            // 
            this.txtMasterPrefix.Location = new System.Drawing.Point(4, 50);
            this.txtMasterPrefix.Margin = new System.Windows.Forms.Padding(2);
            this.txtMasterPrefix.Name = "txtMasterPrefix";
            this.txtMasterPrefix.Size = new System.Drawing.Size(56, 20);
            this.txtMasterPrefix.TabIndex = 17;
            this.txtMasterPrefix.Text = "WO";
            // 
            // lstvChildren
            // 
            this.lstvChildren.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNestedTable,
            this.chNestedPrefix});
            this.lstvChildren.Location = new System.Drawing.Point(5, 80);
            this.lstvChildren.Name = "lstvChildren";
            this.lstvChildren.Size = new System.Drawing.Size(192, 99);
            this.lstvChildren.TabIndex = 16;
            this.lstvChildren.UseCompatibleStateImageBehavior = false;
            this.lstvChildren.View = System.Windows.Forms.View.Details;
            this.lstvChildren.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstvChildren_KeyDown);
            // 
            // chNestedTable
            // 
            this.chNestedTable.Text = "Table";
            this.chNestedTable.Width = 138;
            // 
            // chNestedPrefix
            // 
            this.chNestedPrefix.Text = "Prefix";
            this.chNestedPrefix.Width = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Master table";
            // 
            // txtMasterTable
            // 
            this.txtMasterTable.Location = new System.Drawing.Point(3, 14);
            this.txtMasterTable.Margin = new System.Windows.Forms.Padding(2);
            this.txtMasterTable.Name = "txtMasterTable";
            this.txtMasterTable.Size = new System.Drawing.Size(56, 20);
            this.txtMasterTable.TabIndex = 14;
            this.txtMasterTable.Text = "vWorkorders";
            // 
            // txtRootName
            // 
            this.txtRootName.Location = new System.Drawing.Point(49, 267);
            this.txtRootName.Name = "txtRootName";
            this.txtRootName.Size = new System.Drawing.Size(109, 20);
            this.txtRootName.TabIndex = 21;
            this.txtRootName.Text = "WORKORDERS";
            this.txtRootName.TextChanged += new System.EventHandler(this.txtRootName_TextChanged);
            // 
            // txtElementName
            // 
            this.txtElementName.Location = new System.Drawing.Point(49, 250);
            this.txtElementName.Name = "txtElementName";
            this.txtElementName.Size = new System.Drawing.Size(109, 20);
            this.txtElementName.TabIndex = 20;
            this.txtElementName.Text = "WORKORDER";
            this.txtElementName.TextChanged += new System.EventHandler(this.txtElementName_TextChanged);
            // 
            // chkForXml
            // 
            this.chkForXml.Checked = true;
            this.chkForXml.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkForXml.Location = new System.Drawing.Point(3, 250);
            this.chkForXml.Name = "chkForXml";
            this.chkForXml.Size = new System.Drawing.Size(55, 37);
            this.chkForXml.TabIndex = 19;
            this.chkForXml.Text = "FOR XML";
            this.chkForXml.UseVisualStyleBackColor = true;
            this.chkForXml.CheckedChanged += new System.EventHandler(this.chkForXml_CheckedChanged);
            // 
            // NestedSelectPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtRootName);
            this.Controls.Add(this.txtElementName);
            this.Controls.Add(this.chkForXml);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMasterPrefix);
            this.Controls.Add(this.lstvChildren);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMasterTable);
            this.Name = "NestedSelectPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMasterPrefix;
        private System.Windows.Forms.ListView lstvChildren;
        private System.Windows.Forms.ColumnHeader chNestedTable;
        private System.Windows.Forms.ColumnHeader chNestedPrefix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMasterTable;
        private System.Windows.Forms.TextBox txtRootName;
        private System.Windows.Forms.TextBox txtElementName;
        private System.Windows.Forms.CheckBox chkForXml;
    }
}
