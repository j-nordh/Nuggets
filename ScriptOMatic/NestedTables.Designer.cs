namespace ScriptOMatic
{
    partial class NestedTables
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
            this.gbColumn = new System.Windows.Forms.GroupBox();
            this.chkIncludeColumn = new System.Windows.Forms.CheckBox();
            this.gbTable = new System.Windows.Forms.GroupBox();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPropName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tree = new System.Windows.Forms.TreeView();
            this.gbColumn.SuspendLayout();
            this.gbTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbColumn
            // 
            this.gbColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbColumn.Controls.Add(this.chkIncludeColumn);
            this.gbColumn.Location = new System.Drawing.Point(1, 331);
            this.gbColumn.Name = "gbColumn";
            this.gbColumn.Size = new System.Drawing.Size(194, 39);
            this.gbColumn.TabIndex = 6;
            this.gbColumn.TabStop = false;
            this.gbColumn.Text = "Column properties";
            // 
            // chkIncludeColumn
            // 
            this.chkIncludeColumn.AutoSize = true;
            this.chkIncludeColumn.Location = new System.Drawing.Point(6, 19);
            this.chkIncludeColumn.Name = "chkIncludeColumn";
            this.chkIncludeColumn.Size = new System.Drawing.Size(61, 17);
            this.chkIncludeColumn.TabIndex = 0;
            this.chkIncludeColumn.Text = "Include";
            this.chkIncludeColumn.UseVisualStyleBackColor = true;
            this.chkIncludeColumn.CheckedChanged += new System.EventHandler(this.chkIncludeColumn_CheckedChanged);
            // 
            // gbTable
            // 
            this.gbTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTable.Controls.Add(this.txtAlias);
            this.gbTable.Controls.Add(this.label2);
            this.gbTable.Controls.Add(this.txtPropName);
            this.gbTable.Controls.Add(this.label1);
            this.gbTable.Location = new System.Drawing.Point(1, 276);
            this.gbTable.Name = "gbTable";
            this.gbTable.Size = new System.Drawing.Size(194, 49);
            this.gbTable.TabIndex = 5;
            this.gbTable.TabStop = false;
            this.gbTable.Text = "Table properties";
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(88, 25);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(29, 20);
            this.txtAlias.TabIndex = 3;
            this.txtAlias.TextChanged += new System.EventHandler(this.txtAlias_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alias";
            // 
            // txtPropName
            // 
            this.txtPropName.Location = new System.Drawing.Point(6, 25);
            this.txtPropName.Name = "txtPropName";
            this.txtPropName.Size = new System.Drawing.Size(76, 20);
            this.txtPropName.TabIndex = 1;
            this.txtPropName.TextChanged += new System.EventHandler(this.txtPropName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Property";
            // 
            // tree
            // 
            this.tree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tree.Location = new System.Drawing.Point(1, 1);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(194, 269);
            this.tree.TabIndex = 4;
            this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterSelect);
            // 
            // NestedTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbColumn);
            this.Controls.Add(this.gbTable);
            this.Controls.Add(this.tree);
            this.Name = "NestedTables";
            this.Size = new System.Drawing.Size(197, 373);
            this.Load += new System.EventHandler(this.NestedTables_Load);
            this.gbColumn.ResumeLayout(false);
            this.gbColumn.PerformLayout();
            this.gbTable.ResumeLayout(false);
            this.gbTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbColumn;
        private System.Windows.Forms.CheckBox chkIncludeColumn;
        private System.Windows.Forms.GroupBox gbTable;
        private System.Windows.Forms.TextBox txtAlias;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPropName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView tree;
    }
}
