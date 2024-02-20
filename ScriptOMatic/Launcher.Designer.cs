namespace ScriptOMatic
{
    partial class Launcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            this.btnGenerator = new System.Windows.Forms.Button();
            this.btnCallCompare = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGenerator
            // 
            this.btnGenerator.Image = global::ScriptOMatic.Properties.Resources.Generator;
            this.btnGenerator.Location = new System.Drawing.Point(3, 220);
            this.btnGenerator.Name = "btnGenerator";
            this.btnGenerator.Size = new System.Drawing.Size(240, 70);
            this.btnGenerator.TabIndex = 0;
            this.btnGenerator.UseVisualStyleBackColor = true;
            this.btnGenerator.Click += new System.EventHandler(this.btnGenerator_Click);
            // 
            // btnCallCompare
            // 
            this.btnCallCompare.Image = global::ScriptOMatic.Properties.Resources.Compare;
            this.btnCallCompare.Location = new System.Drawing.Point(249, 220);
            this.btnCallCompare.Name = "btnCallCompare";
            this.btnCallCompare.Size = new System.Drawing.Size(240, 70);
            this.btnCallCompare.TabIndex = 1;
            this.btnCallCompare.UseVisualStyleBackColor = true;
            this.btnCallCompare.Click += new System.EventHandler(this.btnCallCompare_Click);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ScriptOMatic.Properties.Resources.Logo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(493, 295);
            this.Controls.Add(this.btnCallCompare);
            this.Controls.Add(this.btnGenerator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Launcher";
            this.Text = "Loader";
            this.Load += new System.EventHandler(this.Launcher_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Launcher_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenerator;
        private System.Windows.Forms.Button btnCallCompare;
    }
}