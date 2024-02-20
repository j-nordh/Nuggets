namespace EspStackTracer
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
            this.txtElfPath = new System.Windows.Forms.TextBox();
            this.rtxt = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpIO = new System.Windows.Forms.TabPage();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWorkDir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtToolPath = new System.Windows.Forms.TextBox();
            this.txtPythonPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpIO.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtElfPath
            // 
            this.txtElfPath.Location = new System.Drawing.Point(3, 7);
            this.txtElfPath.Name = "txtElfPath";
            this.txtElfPath.Size = new System.Drawing.Size(597, 20);
            this.txtElfPath.TabIndex = 0;
            this.txtElfPath.Text = "C:\\source\\Flow\\MACS\\ModuleControllers\\CounterControl\\.pio\\build\\esp32-poe-iso\\fir" +
    "mware.elf";
            // 
            // rtxt
            // 
            this.rtxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxt.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxt.Location = new System.Drawing.Point(3, 3);
            this.rtxt.Name = "rtxt";
            this.rtxt.Size = new System.Drawing.Size(650, 388);
            this.rtxt.TabIndex = 1;
            this.rtxt.Text = "";
            this.rtxt.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtElfPath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(664, 30);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpIO);
            this.tabControl1.Controls.Add(this.tpSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 30);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(664, 420);
            this.tabControl1.TabIndex = 3;
            // 
            // tpIO
            // 
            this.tpIO.Controls.Add(this.rtxt);
            this.tpIO.Location = new System.Drawing.Point(4, 22);
            this.tpIO.Name = "tpIO";
            this.tpIO.Padding = new System.Windows.Forms.Padding(3);
            this.tpIO.Size = new System.Drawing.Size(656, 394);
            this.tpIO.TabIndex = 0;
            this.tpIO.Text = "I/O";
            this.tpIO.UseVisualStyleBackColor = true;
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.label3);
            this.tpSettings.Controls.Add(this.txtWorkDir);
            this.tpSettings.Controls.Add(this.label2);
            this.tpSettings.Controls.Add(this.txtToolPath);
            this.tpSettings.Controls.Add(this.txtPythonPath);
            this.tpSettings.Controls.Add(this.label1);
            this.tpSettings.Location = new System.Drawing.Point(4, 22);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(656, 394);
            this.tpSettings.TabIndex = 1;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Work dir:";
            // 
            // txtWorkDir
            // 
            this.txtWorkDir.Location = new System.Drawing.Point(90, 58);
            this.txtWorkDir.Name = "txtWorkDir";
            this.txtWorkDir.Size = new System.Drawing.Size(506, 20);
            this.txtWorkDir.TabIndex = 4;
            this.txtWorkDir.Text = "C:\\Temp\\EspStackTracer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Toolchain path:";
            // 
            // txtToolPath
            // 
            this.txtToolPath.Location = new System.Drawing.Point(90, 32);
            this.txtToolPath.Name = "txtToolPath";
            this.txtToolPath.Size = new System.Drawing.Size(506, 20);
            this.txtToolPath.TabIndex = 2;
            this.txtToolPath.Text = "C:\\Users\\johannesno\\.platformio\\packages\\toolchain-xtensa32";
            // 
            // txtPythonPath
            // 
            this.txtPythonPath.Location = new System.Drawing.Point(90, 6);
            this.txtPythonPath.Name = "txtPythonPath";
            this.txtPythonPath.Size = new System.Drawing.Size(506, 20);
            this.txtPythonPath.TabIndex = 1;
            this.txtPythonPath.Text = "C:\\Program Files\\Python311\\python.exe";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Python path:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpIO.ResumeLayout(false);
            this.tpSettings.ResumeLayout(false);
            this.tpSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtElfPath;
        private System.Windows.Forms.RichTextBox rtxt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpIO;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.TextBox txtPythonPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtToolPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWorkDir;
    }
}

