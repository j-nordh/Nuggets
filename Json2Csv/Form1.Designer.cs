namespace Json2Csv
{
    partial class Form1
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
            this.rtxtInput = new System.Windows.Forms.RichTextBox();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.gbParsers = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.flpParsers = new System.Windows.Forms.FlowLayoutPanel();
            this.rightPanel.SuspendLayout();
            this.gbParsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxtInput
            // 
            this.rtxtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtInput.Location = new System.Drawing.Point(0, 0);
            this.rtxtInput.Name = "rtxtInput";
            this.rtxtInput.Size = new System.Drawing.Size(800, 450);
            this.rtxtInput.TabIndex = 0;
            this.rtxtInput.Text = "";
            this.rtxtInput.TextChanged += new System.EventHandler(this.rtxtInput_TextChanged);
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.gbParsers);
            this.rightPanel.Controls.Add(this.btnSave);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(656, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(144, 450);
            this.rightPanel.TabIndex = 1;
            // 
            // gbParsers
            // 
            this.gbParsers.Controls.Add(this.flpParsers);
            this.gbParsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbParsers.Location = new System.Drawing.Point(0, 0);
            this.gbParsers.Name = "gbParsers";
            this.gbParsers.Size = new System.Drawing.Size(144, 427);
            this.gbParsers.TabIndex = 0;
            this.gbParsers.TabStop = false;
            this.gbParsers.Text = "Parsers";
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(0, 427);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(144, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // flpParsers
            // 
            this.flpParsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpParsers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpParsers.Location = new System.Drawing.Point(3, 16);
            this.flpParsers.Name = "flpParsers";
            this.flpParsers.Size = new System.Drawing.Size(138, 408);
            this.flpParsers.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.rtxtInput);
            this.Name = "Form1";
            this.Text = "Form1";
            this.rightPanel.ResumeLayout(false);
            this.gbParsers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtInput;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.GroupBox gbParsers;
        private System.Windows.Forms.FlowLayoutPanel flpParsers;
        private System.Windows.Forms.Button btnSave;
    }
}

