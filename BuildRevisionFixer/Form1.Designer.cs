namespace BuildRevisionFixer
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
            this.inputLabel = new System.Windows.Forms.Label();
            this.txtFileDestination = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.majorTextbox = new UtilClasses.Winforms.NumericTextbox();
            this.minorTextbox = new UtilClasses.Winforms.NumericTextbox();
            this.buildTextbox = new UtilClasses.Winforms.NumericTextbox();
            this.label1 = new System.Windows.Forms.Label();
            this.runBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(27, 57);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(138, 17);
            this.inputLabel.TabIndex = 0;
            this.inputLabel.Text = "Input file destination:";
            // 
            // txtFileDestination
            // 
            this.txtFileDestination.Location = new System.Drawing.Point(171, 54);
            this.txtFileDestination.Name = "txtFileDestination";
            this.txtFileDestination.Size = new System.Drawing.Size(496, 22);
            this.txtFileDestination.TabIndex = 1;
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(673, 53);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(76, 23);
            this.browseBtn.TabIndex = 2;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // majorTextbox
            // 
            this.majorTextbox.Cue = null;
            this.majorTextbox.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.majorTextbox.Location = new System.Drawing.Point(171, 83);
            this.majorTextbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.majorTextbox.Max = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.majorTextbox.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.majorTextbox.Name = "majorTextbox";
            this.majorTextbox.OnlyIntegers = true;
            this.majorTextbox.Size = new System.Drawing.Size(61, 25);
            this.majorTextbox.TabIndex = 3;
            this.majorTextbox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // minorTextbox
            // 
            this.minorTextbox.Cue = null;
            this.minorTextbox.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.minorTextbox.Location = new System.Drawing.Point(240, 83);
            this.minorTextbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.minorTextbox.Max = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.minorTextbox.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.minorTextbox.Name = "minorTextbox";
            this.minorTextbox.OnlyIntegers = true;
            this.minorTextbox.Size = new System.Drawing.Size(61, 25);
            this.minorTextbox.TabIndex = 4;
            this.minorTextbox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // buildTextbox
            // 
            this.buildTextbox.Cue = null;
            this.buildTextbox.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.buildTextbox.Location = new System.Drawing.Point(309, 83);
            this.buildTextbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buildTextbox.Max = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.buildTextbox.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.buildTextbox.Name = "buildTextbox";
            this.buildTextbox.OnlyIntegers = true;
            this.buildTextbox.Size = new System.Drawing.Size(61, 25);
            this.buildTextbox.TabIndex = 5;
            this.buildTextbox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Version:";
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(673, 115);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(75, 23);
            this.runBtn.TabIndex = 7;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(592, 115);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 8;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 182);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buildTextbox);
            this.Controls.Add(this.minorTextbox);
            this.Controls.Add(this.majorTextbox);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.txtFileDestination);
            this.Controls.Add(this.inputLabel);
            this.Name = "Form1";
            this.Text = "BuildRevisionFixer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.TextBox txtFileDestination;
        private System.Windows.Forms.Button browseBtn;
        private UtilClasses.Winforms.NumericTextbox majorTextbox;
        private UtilClasses.Winforms.NumericTextbox minorTextbox;
        private UtilClasses.Winforms.NumericTextbox buildTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}

