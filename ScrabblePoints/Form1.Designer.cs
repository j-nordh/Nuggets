namespace ScrabblePoints
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
            this.ctxtWord = new UtilClasses.Winforms.CueTextBox();
            this.lblPoints = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSwedish = new System.Windows.Forms.RadioButton();
            this.rbEnglish = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxtWord
            // 
            this.ctxtWord.Cue = "Word";
            this.ctxtWord.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctxtWord.Location = new System.Drawing.Point(0, 0);
            this.ctxtWord.Name = "ctxtWord";
            this.ctxtWord.Size = new System.Drawing.Size(268, 22);
            this.ctxtWord.TabIndex = 0;
            this.ctxtWord.TextChanged += new System.EventHandler(this.ctxtWord_TextChanged);
            // 
            // lblPoints
            // 
            this.lblPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoints.Location = new System.Drawing.Point(100, 22);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(168, 95);
            this.lblPoints.TabIndex = 1;
            this.lblPoints.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbEnglish);
            this.groupBox1.Controls.Add(this.rbSwedish);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(100, 95);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Language";
            // 
            // rbSwedish
            // 
            this.rbSwedish.AutoSize = true;
            this.rbSwedish.Checked = true;
            this.rbSwedish.Location = new System.Drawing.Point(5, 32);
            this.rbSwedish.Name = "rbSwedish";
            this.rbSwedish.Size = new System.Drawing.Size(81, 21);
            this.rbSwedish.TabIndex = 0;
            this.rbSwedish.TabStop = true;
            this.rbSwedish.Text = "Swedish";
            this.rbSwedish.UseVisualStyleBackColor = true;
            // 
            // rbEnglish
            // 
            this.rbEnglish.AutoSize = true;
            this.rbEnglish.Location = new System.Drawing.Point(5, 59);
            this.rbEnglish.Name = "rbEnglish";
            this.rbEnglish.Size = new System.Drawing.Size(75, 21);
            this.rbEnglish.TabIndex = 1;
            this.rbEnglish.Text = "English";
            this.rbEnglish.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 117);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ctxtWord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Form1";
            this.Text = "Scrabble points";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UtilClasses.Winforms.CueTextBox ctxtWord;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbEnglish;
        private System.Windows.Forms.RadioButton rbSwedish;
    }
}

