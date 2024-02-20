namespace FreePin
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
            this.nudFontSize = new System.Windows.Forms.NumericUpDown();
            this.nudSmallFontSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbFont = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.flpRight = new System.Windows.Forms.FlowLayoutPanel();
            this.gbPredefined = new System.Windows.Forms.GroupBox();
            this.flpPredefined = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvProjectFunctions = new System.Windows.Forms.ListView();
            this.btnBrowseFuncDef = new System.Windows.Forms.Button();
            this.gbRendering = new System.Windows.Forms.GroupBox();
            this.cbModule = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSmallFontSize)).BeginInit();
            this.flpRight.SuspendLayout();
            this.gbPredefined.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbRendering.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudFontSize
            // 
            this.nudFontSize.DecimalPlaces = 1;
            this.nudFontSize.Location = new System.Drawing.Point(55, 39);
            this.nudFontSize.Name = "nudFontSize";
            this.nudFontSize.Size = new System.Drawing.Size(46, 20);
            this.nudFontSize.TabIndex = 3;
            this.nudFontSize.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // nudSmallFontSize
            // 
            this.nudSmallFontSize.DecimalPlaces = 1;
            this.nudSmallFontSize.Location = new System.Drawing.Point(168, 39);
            this.nudSmallFontSize.Name = "nudSmallFontSize";
            this.nudSmallFontSize.Size = new System.Drawing.Size(46, 20);
            this.nudSmallFontSize.TabIndex = 4;
            this.nudSmallFontSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Big size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(107, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Small Size";
            // 
            // cbFont
            // 
            this.cbFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFont.FormattingEnabled = true;
            this.cbFont.Location = new System.Drawing.Point(55, 65);
            this.cbFont.Name = "cbFont";
            this.cbFont.Size = new System.Drawing.Size(159, 21);
            this.cbFont.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Font";
            // 
            // flpRight
            // 
            this.flpRight.Controls.Add(this.cbModule);
            this.flpRight.Controls.Add(this.gbPredefined);
            this.flpRight.Controls.Add(this.groupBox1);
            this.flpRight.Controls.Add(this.gbRendering);
            this.flpRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpRight.Location = new System.Drawing.Point(446, 0);
            this.flpRight.Name = "flpRight";
            this.flpRight.Size = new System.Drawing.Size(231, 450);
            this.flpRight.TabIndex = 11;
            // 
            // gbPredefined
            // 
            this.gbPredefined.Controls.Add(this.flpPredefined);
            this.gbPredefined.Location = new System.Drawing.Point(3, 30);
            this.gbPredefined.Name = "gbPredefined";
            this.gbPredefined.Size = new System.Drawing.Size(224, 116);
            this.gbPredefined.TabIndex = 12;
            this.gbPredefined.TabStop = false;
            this.gbPredefined.Text = "Predefined functions";
            // 
            // flpPredefined
            // 
            this.flpPredefined.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPredefined.Location = new System.Drawing.Point(3, 16);
            this.flpPredefined.Name = "flpPredefined";
            this.flpPredefined.Size = new System.Drawing.Size(218, 97);
            this.flpPredefined.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvProjectFunctions);
            this.groupBox1.Controls.Add(this.btnBrowseFuncDef);
            this.groupBox1.Location = new System.Drawing.Point(3, 152);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 134);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Project reservations";
            // 
            // lvProjectFunctions
            // 
            this.lvProjectFunctions.CheckBoxes = true;
            this.lvProjectFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvProjectFunctions.HideSelection = false;
            this.lvProjectFunctions.Location = new System.Drawing.Point(3, 36);
            this.lvProjectFunctions.Name = "lvProjectFunctions";
            this.lvProjectFunctions.Size = new System.Drawing.Size(218, 95);
            this.lvProjectFunctions.TabIndex = 15;
            this.lvProjectFunctions.UseCompatibleStateImageBehavior = false;
            this.lvProjectFunctions.View = System.Windows.Forms.View.Details;
            // 
            // btnBrowseFuncDef
            // 
            this.btnBrowseFuncDef.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBrowseFuncDef.Location = new System.Drawing.Point(3, 16);
            this.btnBrowseFuncDef.Name = "btnBrowseFuncDef";
            this.btnBrowseFuncDef.Size = new System.Drawing.Size(218, 20);
            this.btnBrowseFuncDef.TabIndex = 14;
            this.btnBrowseFuncDef.Text = "...";
            this.btnBrowseFuncDef.UseVisualStyleBackColor = true;
            this.btnBrowseFuncDef.Click += new System.EventHandler(this.btnBrowseFuncDef_Click);
            // 
            // gbRendering
            // 
            this.gbRendering.Controls.Add(this.label5);
            this.gbRendering.Controls.Add(this.cbFont);
            this.gbRendering.Controls.Add(this.label4);
            this.gbRendering.Controls.Add(this.nudSmallFontSize);
            this.gbRendering.Controls.Add(this.label3);
            this.gbRendering.Controls.Add(this.nudFontSize);
            this.gbRendering.Location = new System.Drawing.Point(3, 292);
            this.gbRendering.Name = "gbRendering";
            this.gbRendering.Size = new System.Drawing.Size(224, 96);
            this.gbRendering.TabIndex = 12;
            this.gbRendering.TabStop = false;
            this.gbRendering.Text = "Rendering";
            // 
            // cmboModule
            // 
            this.cbModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModule.FormattingEnabled = true;
            this.cbModule.Location = new System.Drawing.Point(3, 3);
            this.cbModule.Name = "cbModule";
            this.cbModule.Size = new System.Drawing.Size(221, 21);
            this.cbModule.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 450);
            this.Controls.Add(this.flpRight);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSmallFontSize)).EndInit();
            this.flpRight.ResumeLayout(false);
            this.gbPredefined.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.gbRendering.ResumeLayout(false);
            this.gbRendering.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NumericUpDown nudFontSize;
        private System.Windows.Forms.NumericUpDown nudSmallFontSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbFont;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel flpRight;
        private System.Windows.Forms.GroupBox gbPredefined;
        private System.Windows.Forms.GroupBox gbRendering;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvProjectFunctions;
        private System.Windows.Forms.Button btnBrowseFuncDef;
        private System.Windows.Forms.FlowLayoutPanel flpPredefined;
        private System.Windows.Forms.ComboBox cbModule;
    }
}

