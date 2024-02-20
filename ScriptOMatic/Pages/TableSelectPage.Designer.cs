namespace ScriptOMatic.Pages
{
    partial class TableSelectPage
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
            this.lblTable = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.ctxtPluralName = new UtilClasses.Winforms.CueTextBox();
            this.ctxtSingularName = new UtilClasses.Winforms.CueTextBox();
            this.SuspendLayout();
            // 
            // lblTable
            // 
            this.lblTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTable.BackColor = System.Drawing.SystemColors.Window;
            this.lblTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTable.Location = new System.Drawing.Point(3, 4);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(156, 24);
            this.lblTable.TabIndex = 31;
            this.lblTable.Text = "Table name";
            this.lblTable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(165, 3);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(72, 26);
            this.btnSelect.TabIndex = 28;
            this.btnSelect.Text = "Select table";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // ctxtPluralName
            // 
            this.ctxtPluralName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctxtPluralName.Cue = "Plural name";
            this.ctxtPluralName.Location = new System.Drawing.Point(147, 31);
            this.ctxtPluralName.Name = "ctxtPluralName";
            this.ctxtPluralName.Size = new System.Drawing.Size(90, 20);
            this.ctxtPluralName.TabIndex = 30;
            // 
            // ctxtSingularName
            // 
            this.ctxtSingularName.Cue = "Singular name";
            this.ctxtSingularName.Location = new System.Drawing.Point(3, 31);
            this.ctxtSingularName.Name = "ctxtSingularName";
            this.ctxtSingularName.Size = new System.Drawing.Size(90, 20);
            this.ctxtSingularName.TabIndex = 29;
            // 
            // TableSelectPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTable);
            this.Controls.Add(this.ctxtPluralName);
            this.Controls.Add(this.ctxtSingularName);
            this.Controls.Add(this.btnSelect);
            this.Name = "TableSelectPage";
            this.Size = new System.Drawing.Size(240, 300);
            this.Load += new System.EventHandler(this.TableSelectPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTable;
        protected UtilClasses.Winforms.CueTextBox ctxtPluralName;
        protected UtilClasses.Winforms.CueTextBox ctxtSingularName;
        private System.Windows.Forms.Button btnSelect;
    }
}
