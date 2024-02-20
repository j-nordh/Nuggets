namespace ScriptOMatic
{
    partial class AggregateEditor
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
            this.ltxtTable = new UtilClasses.Winforms.LabelledTextbox();
            this.ltxtAlias = new UtilClasses.Winforms.LabelledTextbox();
            this.ltxtRepo = new UtilClasses.Winforms.LabelledTextbox();
            this.chkReadonly = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ltxtTargetType = new UtilClasses.Winforms.LabelledTextbox();
            this.ltxtShortName = new UtilClasses.Winforms.LabelledTextbox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ltxtTable
            // 
            this.ltxtTable.Caption = "Table";
            this.ltxtTable.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.ltxtTable.Location = new System.Drawing.Point(3, 3);
            this.ltxtTable.Multiline = false;
            this.ltxtTable.Name = "ltxtTable";
            this.ltxtTable.ReadOnly = true;
            this.ltxtTable.Size = new System.Drawing.Size(150, 33);
            this.ltxtTable.TabIndex = 1;
            this.ltxtTable.UnitText = "";
            this.ltxtTable.UsePasswordChar = false;
            // 
            // ltxtAlias
            // 
            this.ltxtAlias.Caption = "Alias";
            this.ltxtAlias.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.ltxtAlias.Location = new System.Drawing.Point(3, 42);
            this.ltxtAlias.Multiline = false;
            this.ltxtAlias.Name = "ltxtAlias";
            this.ltxtAlias.ReadOnly = false;
            this.ltxtAlias.Size = new System.Drawing.Size(150, 33);
            this.ltxtAlias.TabIndex = 2;
            this.ltxtAlias.UnitText = "";
            this.ltxtAlias.UsePasswordChar = false;
            // 
            // ltxtRepo
            // 
            this.ltxtRepo.Caption = "Repo";
            this.ltxtRepo.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.ltxtRepo.Location = new System.Drawing.Point(3, 81);
            this.ltxtRepo.Multiline = false;
            this.ltxtRepo.Name = "ltxtRepo";
            this.ltxtRepo.ReadOnly = false;
            this.ltxtRepo.Size = new System.Drawing.Size(150, 33);
            this.ltxtRepo.TabIndex = 3;
            this.ltxtRepo.UnitText = "";
            this.ltxtRepo.UsePasswordChar = false;
            // 
            // chkReadonly
            // 
            this.chkReadonly.AutoSize = true;
            this.chkReadonly.Location = new System.Drawing.Point(3, 120);
            this.chkReadonly.Name = "chkReadonly";
            this.chkReadonly.Size = new System.Drawing.Size(71, 17);
            this.chkReadonly.TabIndex = 5;
            this.chkReadonly.Text = "Readonly";
            this.chkReadonly.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ltxtTable);
            this.flowLayoutPanel1.Controls.Add(this.ltxtAlias);
            this.flowLayoutPanel1.Controls.Add(this.ltxtRepo);
            this.flowLayoutPanel1.Controls.Add(this.chkReadonly);
            this.flowLayoutPanel1.Controls.Add(this.ltxtTargetType);
            this.flowLayoutPanel1.Controls.Add(this.ltxtShortName);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(183, 225);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // ltxtTargetType
            // 
            this.ltxtTargetType.Caption = "TargetType";
            this.ltxtTargetType.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.ltxtTargetType.Location = new System.Drawing.Point(3, 143);
            this.ltxtTargetType.Multiline = false;
            this.ltxtTargetType.Name = "ltxtTargetType";
            this.ltxtTargetType.ReadOnly = false;
            this.ltxtTargetType.Size = new System.Drawing.Size(150, 33);
            this.ltxtTargetType.TabIndex = 6;
            this.ltxtTargetType.UnitText = "";
            this.ltxtTargetType.UsePasswordChar = false;
            // 
            // ltxtShortName
            // 
            this.ltxtShortName.Caption = "Short name";
            this.ltxtShortName.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.ltxtShortName.Location = new System.Drawing.Point(3, 182);
            this.ltxtShortName.Multiline = false;
            this.ltxtShortName.Name = "ltxtShortName";
            this.ltxtShortName.ReadOnly = false;
            this.ltxtShortName.Size = new System.Drawing.Size(150, 33);
            this.ltxtShortName.TabIndex = 7;
            this.ltxtShortName.UnitText = "";
            this.ltxtShortName.UsePasswordChar = false;
            // 
            // AggregateEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(183, 255);
            this.Controls.Add(this.flowLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AggregateEditor";
            this.Text = "Aggregate Editor";
            this.Load += new System.EventHandler(this.AggregateEditor_Load);
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UtilClasses.Winforms.LabelledTextbox ltxtTable;
        private UtilClasses.Winforms.LabelledTextbox ltxtAlias;
        private UtilClasses.Winforms.LabelledTextbox ltxtRepo;
        private System.Windows.Forms.CheckBox chkReadonly;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private UtilClasses.Winforms.LabelledTextbox ltxtTargetType;
        private UtilClasses.Winforms.LabelledTextbox ltxtShortName;
    }
}