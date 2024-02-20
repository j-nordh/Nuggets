namespace ScriptOMatic
{
    partial class Generator
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
            this.components = new System.ComponentModel.Container();
            this.ttGenerator = new System.Windows.Forms.ToolTip(this.components);
            this.MainSplit = new System.Windows.Forms.SplitContainer();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.tcContent = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flpBottom = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.cbEnvironment = new System.Windows.Forms.ComboBox();
            this.lblEnvironment = new System.Windows.Forms.Label();
            this.btnUpdateLinkSps = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnCompile = new System.Windows.Forms.Button();
            this.btnUpdateCreator = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.gbDrawSql = new System.Windows.Forms.GroupBox();
            this.chkDrawSqlOutputPath = new System.Windows.Forms.CheckBox();
            this.btnDrawSqlBrowse = new System.Windows.Forms.Button();
            this.chkDrawSqlWithSPs = new System.Windows.Forms.CheckBox();
            this.ctxtDrawSqlPath = new UtilClasses.Winforms.CueTextBox();
            this.btnDrawSql = new System.Windows.Forms.Button();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).BeginInit();
            this.MainSplit.Panel2.SuspendLayout();
            this.MainSplit.SuspendLayout();
            this.tcContent.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flpBottom.SuspendLayout();
            this.gbDrawSql.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            this.SuspendLayout();
            // 
            // MainSplit
            // 
            this.MainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplit.Location = new System.Drawing.Point(0, 0);
            this.MainSplit.Name = "MainSplit";
            // 
            // MainSplit.Panel2
            // 
            this.MainSplit.Panel2.Controls.Add(this.txtResult);
            this.MainSplit.Panel2.Controls.Add(this.tcContent);
            this.MainSplit.Panel2.Controls.Add(this.panel1);
            this.MainSplit.Size = new System.Drawing.Size(887, 684);
            this.MainSplit.SplitterDistance = 267;
            this.MainSplit.TabIndex = 18;
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(0, 21);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(616, 588);
            this.txtResult.TabIndex = 8;
            this.txtResult.Text = "";
            // 
            // tcContent
            // 
            this.tcContent.Controls.Add(this.tabPage1);
            this.tcContent.Controls.Add(this.tabPage2);
            this.tcContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcContent.Location = new System.Drawing.Point(0, 0);
            this.tcContent.Name = "tcContent";
            this.tcContent.SelectedIndex = 0;
            this.tcContent.Size = new System.Drawing.Size(616, 21);
            this.tcContent.TabIndex = 21;
            this.tcContent.SelectedIndexChanged += new System.EventHandler(this.tcContent_SelectedIndexChanged);
            this.tcContent.TabIndexChanged += new System.EventHandler(this.tcContent_TabIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(608, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(608, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flpBottom);
            this.panel1.Controls.Add(this.gbDrawSql);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 609);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 75);
            this.panel1.TabIndex = 20;
            // 
            // flpBottom
            // 
            this.flpBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flpBottom.Controls.Add(this.btnSettings);
            this.flpBottom.Controls.Add(this.cbEnvironment);
            this.flpBottom.Controls.Add(this.lblEnvironment);
            this.flpBottom.Controls.Add(this.btnUpdateLinkSps);
            this.flpBottom.Controls.Add(this.btnRun);
            this.flpBottom.Controls.Add(this.btnCompile);
            this.flpBottom.Controls.Add(this.btnUpdateCreator);
            this.flpBottom.Controls.Add(this.btnCheck);
            this.flpBottom.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBottom.Location = new System.Drawing.Point(239, 3);
            this.flpBottom.Name = "flpBottom";
            this.flpBottom.Size = new System.Drawing.Size(377, 70);
            this.flpBottom.TabIndex = 19;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(299, 3);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // cbEnvironment
            // 
            this.cbEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEnvironment.FormattingEnabled = true;
            this.cbEnvironment.Location = new System.Drawing.Point(195, 3);
            this.cbEnvironment.Name = "cbEnvironment";
            this.cbEnvironment.Size = new System.Drawing.Size(98, 21);
            this.cbEnvironment.TabIndex = 4;
            this.cbEnvironment.SelectedIndexChanged += new System.EventHandler(this.cbEnvironment_SelectedIndexChanged);
            // 
            // lblEnvironment
            // 
            this.lblEnvironment.Location = new System.Drawing.Point(110, 0);
            this.lblEnvironment.Name = "lblEnvironment";
            this.lblEnvironment.Size = new System.Drawing.Size(79, 24);
            this.lblEnvironment.TabIndex = 5;
            this.lblEnvironment.Text = "Environment";
            this.lblEnvironment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUpdateLinkSps
            // 
            this.flpBottom.SetFlowBreak(this.btnUpdateLinkSps, true);
            this.btnUpdateLinkSps.Location = new System.Drawing.Point(20, 3);
            this.btnUpdateLinkSps.Name = "btnUpdateLinkSps";
            this.btnUpdateLinkSps.Size = new System.Drawing.Size(84, 22);
            this.btnUpdateLinkSps.TabIndex = 9;
            this.btnUpdateLinkSps.Text = "Link procs";
            this.btnUpdateLinkSps.UseVisualStyleBackColor = true;
            this.btnUpdateLinkSps.Click += new System.EventHandler(this.btnUpdateLinkSps_Click);
            // 
            // btnRun
            // 
            this.btnRun.Enabled = false;
            this.btnRun.Location = new System.Drawing.Point(290, 32);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(84, 22);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run!";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnCompile
            // 
            this.btnCompile.Location = new System.Drawing.Point(200, 32);
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Size = new System.Drawing.Size(84, 22);
            this.btnCompile.TabIndex = 3;
            this.btnCompile.Text = "SupplyChain";
            this.btnCompile.UseVisualStyleBackColor = true;
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
            // 
            // btnUpdateCreator
            // 
            this.btnUpdateCreator.Location = new System.Drawing.Point(110, 32);
            this.btnUpdateCreator.Name = "btnUpdateCreator";
            this.btnUpdateCreator.Size = new System.Drawing.Size(84, 22);
            this.btnUpdateCreator.TabIndex = 7;
            this.btnUpdateCreator.Text = "Run All";
            this.btnUpdateCreator.UseVisualStyleBackColor = true;
            this.btnUpdateCreator.Click += new System.EventHandler(this.btnUpdateCreator_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(20, 32);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(84, 22);
            this.btnCheck.TabIndex = 8;
            this.btnCheck.Text = "Update SPs";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // gbDrawSql
            // 
            this.gbDrawSql.Controls.Add(this.chkDrawSqlOutputPath);
            this.gbDrawSql.Controls.Add(this.btnDrawSqlBrowse);
            this.gbDrawSql.Controls.Add(this.chkDrawSqlWithSPs);
            this.gbDrawSql.Controls.Add(this.ctxtDrawSqlPath);
            this.gbDrawSql.Controls.Add(this.btnDrawSql);
            this.gbDrawSql.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbDrawSql.Location = new System.Drawing.Point(0, 0);
            this.gbDrawSql.Name = "gbDrawSql";
            this.gbDrawSql.Size = new System.Drawing.Size(233, 75);
            this.gbDrawSql.TabIndex = 9;
            this.gbDrawSql.TabStop = false;
            this.gbDrawSql.Text = "DrawSql";
            // 
            // chkDrawSqlOutputPath
            // 
            this.chkDrawSqlOutputPath.AutoSize = true;
            this.chkDrawSqlOutputPath.Location = new System.Drawing.Point(3, 22);
            this.chkDrawSqlOutputPath.Name = "chkDrawSqlOutputPath";
            this.chkDrawSqlOutputPath.Size = new System.Drawing.Size(15, 14);
            this.chkDrawSqlOutputPath.TabIndex = 10;
            this.chkDrawSqlOutputPath.UseVisualStyleBackColor = true;
            this.chkDrawSqlOutputPath.CheckedChanged += new System.EventHandler(this.chkDrawSqlOutputPath_CheckedChanged);
            // 
            // btnDrawSqlBrowse
            // 
            this.btnDrawSqlBrowse.Location = new System.Drawing.Point(200, 19);
            this.btnDrawSqlBrowse.Name = "btnDrawSqlBrowse";
            this.btnDrawSqlBrowse.Size = new System.Drawing.Size(26, 20);
            this.btnDrawSqlBrowse.TabIndex = 9;
            this.btnDrawSqlBrowse.Text = "...";
            this.btnDrawSqlBrowse.UseVisualStyleBackColor = true;
            this.btnDrawSqlBrowse.Click += new System.EventHandler(this.btnDrawSqlBrowse_Click);
            // 
            // chkDrawSqlWithSPs
            // 
            this.chkDrawSqlWithSPs.AutoSize = true;
            this.chkDrawSqlWithSPs.Location = new System.Drawing.Point(3, 49);
            this.chkDrawSqlWithSPs.Name = "chkDrawSqlWithSPs";
            this.chkDrawSqlWithSPs.Size = new System.Drawing.Size(70, 17);
            this.chkDrawSqlWithSPs.TabIndex = 8;
            this.chkDrawSqlWithSPs.Text = "With SPs";
            this.chkDrawSqlWithSPs.UseVisualStyleBackColor = true;
            // 
            // ctxtDrawSqlPath
            // 
            this.ctxtDrawSqlPath.Cue = "Output path";
            this.ctxtDrawSqlPath.Location = new System.Drawing.Point(24, 19);
            this.ctxtDrawSqlPath.Name = "ctxtDrawSqlPath";
            this.ctxtDrawSqlPath.Size = new System.Drawing.Size(174, 20);
            this.ctxtDrawSqlPath.TabIndex = 7;
            this.ctxtDrawSqlPath.TextChanged += new System.EventHandler(this.ctxtDrawSqlPath_TextChanged);
            // 
            // btnDrawSql
            // 
            this.btnDrawSql.Location = new System.Drawing.Point(143, 45);
            this.btnDrawSql.Name = "btnDrawSql";
            this.btnDrawSql.Size = new System.Drawing.Size(84, 22);
            this.btnDrawSql.TabIndex = 6;
            this.btnDrawSql.Text = "DrawSql";
            this.btnDrawSql.UseVisualStyleBackColor = true;
            this.btnDrawSql.Click += new System.EventHandler(this.btnDrawSql_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(87, 30);
            this.Root.TextVisible = false;
            // 
            // Generator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 684);
            this.Controls.Add(this.MainSplit);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Generator";
            this.Text = "Script-o-matic";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).EndInit();
            this.MainSplit.ResumeLayout(false);
            this.tcContent.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.flpBottom.ResumeLayout(false);
            this.gbDrawSql.ResumeLayout(false);
            this.gbDrawSql.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip ttGenerator;
        private System.Windows.Forms.SplitContainer MainSplit;
        private System.Windows.Forms.FlowLayoutPanel flpBottom;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnCompile;
        private System.Windows.Forms.ComboBox cbEnvironment;
        private System.Windows.Forms.Label lblEnvironment;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.Button btnDrawSql;
        private System.Windows.Forms.Button btnUpdateCreator;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbDrawSql;
        private System.Windows.Forms.Button btnDrawSqlBrowse;
        private System.Windows.Forms.CheckBox chkDrawSqlWithSPs;
        private UtilClasses.Winforms.CueTextBox ctxtDrawSqlPath;
        private System.Windows.Forms.CheckBox chkDrawSqlOutputPath;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.TabControl tcContent;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnUpdateLinkSps;
    }
}

