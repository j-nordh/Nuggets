namespace ScriptOMatic
{
    partial class ProcDefEditor
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
            this.lblSpName = new UtilClasses.Winforms.LabelledLabel();
            this.ltxtCodeName = new UtilClasses.Winforms.LabelledTextbox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lblSpName
            // 
            this.lblSpName.AutoSize = true;
            this.lblSpName.Caption = "Stored Procedure:";
            this.lblSpName.Location = new System.Drawing.Point(12, 12);
            this.lblSpName.Name = "lblSpName";
            this.lblSpName.Size = new System.Drawing.Size(98, 37);
            this.lblSpName.TabIndex = 0;
            // 
            // ltxtCodeName
            // 
            this.ltxtCodeName.Caption = "Code name:";
            this.ltxtCodeName.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.ltxtCodeName.Location = new System.Drawing.Point(129, 12);
            this.ltxtCodeName.Multiline = false;
            this.ltxtCodeName.Name = "ltxtCodeName";
            this.ltxtCodeName.ReadOnly = false;
            this.ltxtCodeName.Size = new System.Drawing.Size(150, 37);
            this.ltxtCodeName.TabIndex = 1;
            this.ltxtCodeName.UnitText = "";
            this.ltxtCodeName.UsePasswordChar = false;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 55);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(267, 120);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // ProcDefEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.ltxtCodeName);
            this.Controls.Add(this.lblSpName);
            this.Name = "ProcDefEditor";
            this.Text = "Procedure editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UtilClasses.Winforms.LabelledLabel lblSpName;
        private UtilClasses.Winforms.LabelledTextbox ltxtCodeName;
        private System.Windows.Forms.ListView listView1;
    }
}