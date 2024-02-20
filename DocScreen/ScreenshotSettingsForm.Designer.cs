namespace DocScreen
{
    partial class ScreenshotSettingsForm
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
            this.gbPath = new System.Windows.Forms.GroupBox();
            this.btnPathBrowse = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.gbName = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.flpOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.gbPath.SuspendLayout();
            this.gbName.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPath
            // 
            this.gbPath.Controls.Add(this.txtPath);
            this.gbPath.Controls.Add(this.btnPathBrowse);
            this.gbPath.Location = new System.Drawing.Point(12, 12);
            this.gbPath.Name = "gbPath";
            this.gbPath.Size = new System.Drawing.Size(279, 42);
            this.gbPath.TabIndex = 0;
            this.gbPath.TabStop = false;
            this.gbPath.Text = "Path";
            // 
            // btnPathBrowse
            // 
            this.btnPathBrowse.Location = new System.Drawing.Point(234, 13);
            this.btnPathBrowse.Name = "btnPathBrowse";
            this.btnPathBrowse.Size = new System.Drawing.Size(39, 21);
            this.btnPathBrowse.TabIndex = 0;
            this.btnPathBrowse.Text = "...";
            this.btnPathBrowse.UseVisualStyleBackColor = true;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(6, 14);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(222, 20);
            this.txtPath.TabIndex = 1;
            // 
            // gbName
            // 
            this.gbName.Controls.Add(this.txtName);
            this.gbName.Location = new System.Drawing.Point(12, 60);
            this.gbName.Name = "gbName";
            this.gbName.Size = new System.Drawing.Size(279, 42);
            this.gbName.TabIndex = 2;
            this.gbName.TabStop = false;
            this.gbName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(6, 14);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(222, 20);
            this.txtName.TabIndex = 1;
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.flpOptions);
            this.gbOptions.Location = new System.Drawing.Point(298, 13);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(148, 89);
            this.gbOptions.TabIndex = 3;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // flpOptions
            // 
            this.flpOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpOptions.Location = new System.Drawing.Point(3, 16);
            this.flpOptions.Name = "flpOptions";
            this.flpOptions.Size = new System.Drawing.Size(142, 70);
            this.flpOptions.TabIndex = 0;
            // 
            // ScreenshotSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 149);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.gbName);
            this.Controls.Add(this.gbPath);
            this.Name = "ScreenshotSettingsForm";
            this.Text = "Screenshot Settings Form";
            this.Load += new System.EventHandler(this.ScreenshotSettingsForm_Load);
            this.gbPath.ResumeLayout(false);
            this.gbPath.PerformLayout();
            this.gbName.ResumeLayout(false);
            this.gbName.PerformLayout();
            this.gbOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnPathBrowse;
        private System.Windows.Forms.GroupBox gbName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.FlowLayoutPanel flpOptions;
    }
}