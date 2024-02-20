namespace ScriptOMatic
{
    partial class CallCompare
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
            this.lvResult = new System.Windows.Forms.ListView();
            this.chParameter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDbType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVal1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVal2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbHeading = new System.Windows.Forms.Label();
            this.chkHideSame = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtCall2 = new ScriptOMatic.LabeledTextbox();
            this.txtCall1 = new ScriptOMatic.LabeledTextbox();
            this.txtDefinition = new ScriptOMatic.LabeledTextbox();
            this.chkHideNull = new System.Windows.Forms.CheckBox();
            this.chkHideDefault = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvResult
            // 
            this.lvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chParameter,
            this.chDbType,
            this.chVal1,
            this.chVal2});
            this.lvResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.lvResult.Location = new System.Drawing.Point(2, 22);
            this.lvResult.Margin = new System.Windows.Forms.Padding(2);
            this.lvResult.Name = "lvResult";
            this.lvResult.Size = new System.Drawing.Size(532, 503);
            this.lvResult.TabIndex = 3;
            this.lvResult.UseCompatibleStateImageBehavior = false;
            this.lvResult.View = System.Windows.Forms.View.Details;
            // 
            // chParameter
            // 
            this.chParameter.Text = "Parameter";
            this.chParameter.Width = 236;
            // 
            // chDbType
            // 
            this.chDbType.Text = "Type";
            // 
            // chVal1
            // 
            this.chVal1.Text = "Value 1";
            this.chVal1.Width = 129;
            // 
            // chVal2
            // 
            this.chVal2.Text = "Value 2";
            this.chVal2.Width = 142;
            // 
            // lbHeading
            // 
            this.lbHeading.Font = new System.Drawing.Font("Comic Sans MS", 18F);
            this.lbHeading.Location = new System.Drawing.Point(512, 11);
            this.lbHeading.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHeading.Name = "lbHeading";
            this.lbHeading.Size = new System.Drawing.Size(556, 33);
            this.lbHeading.TabIndex = 4;
            this.lbHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkHideSame
            // 
            this.chkHideSame.AutoSize = true;
            this.chkHideSame.Location = new System.Drawing.Point(2, 5);
            this.chkHideSame.Margin = new System.Windows.Forms.Padding(2);
            this.chkHideSame.Name = "chkHideSame";
            this.chkHideSame.Size = new System.Drawing.Size(124, 17);
            this.chkHideSame.TabIndex = 5;
            this.chkHideSame.Text = "Hide identical values";
            this.chkHideSame.UseVisualStyleBackColor = true;
            this.chkHideSame.CheckedChanged += new System.EventHandler(this.chkHide_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(9, 11);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chkHideDefault);
            this.splitContainer1.Panel2.Controls.Add(this.chkHideNull);
            this.splitContainer1.Panel2.Controls.Add(this.lvResult);
            this.splitContainer1.Panel2.Controls.Add(this.chkHideSame);
            this.splitContainer1.Size = new System.Drawing.Size(1059, 526);
            this.splitContainer1.SplitterDistance = 523;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtCall2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtCall1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtDefinition, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(518, 522);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // txtCall2
            // 
            this.txtCall2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCall2.Caption = "Call 2";
            this.txtCall2.Enabled = false;
            this.txtCall2.Location = new System.Drawing.Point(2, 350);
            this.txtCall2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCall2.Name = "txtCall2";
            this.txtCall2.Size = new System.Drawing.Size(514, 170);
            this.txtCall2.TabIndex = 9;
            // 
            // txtCall1
            // 
            this.txtCall1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCall1.Caption = "Call 1";
            this.txtCall1.Enabled = false;
            this.txtCall1.Location = new System.Drawing.Point(2, 176);
            this.txtCall1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCall1.Name = "txtCall1";
            this.txtCall1.Size = new System.Drawing.Size(514, 170);
            this.txtCall1.TabIndex = 8;
            // 
            // txtDefinition
            // 
            this.txtDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDefinition.Caption = "Definition";
            this.txtDefinition.Location = new System.Drawing.Point(2, 2);
            this.txtDefinition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDefinition.Name = "txtDefinition";
            this.txtDefinition.Size = new System.Drawing.Size(514, 170);
            this.txtDefinition.TabIndex = 7;
            // 
            // chkHideNull
            // 
            this.chkHideNull.AutoSize = true;
            this.chkHideNull.Location = new System.Drawing.Point(130, 5);
            this.chkHideNull.Margin = new System.Windows.Forms.Padding(2);
            this.chkHideNull.Name = "chkHideNull";
            this.chkHideNull.Size = new System.Drawing.Size(79, 17);
            this.chkHideNull.TabIndex = 6;
            this.chkHideNull.Text = "Hide NULL";
            this.chkHideNull.UseVisualStyleBackColor = true;
            this.chkHideNull.CheckedChanged += new System.EventHandler(this.chkHide_CheckedChanged);
            // 
            // chkHideDefault
            // 
            this.chkHideDefault.AutoSize = true;
            this.chkHideDefault.Location = new System.Drawing.Point(213, 5);
            this.chkHideDefault.Margin = new System.Windows.Forms.Padding(2);
            this.chkHideDefault.Name = "chkHideDefault";
            this.chkHideDefault.Size = new System.Drawing.Size(100, 17);
            this.chkHideDefault.TabIndex = 7;
            this.chkHideDefault.Text = "Hide DEFAULT";
            this.chkHideDefault.UseVisualStyleBackColor = true;
            this.chkHideDefault.CheckedChanged += new System.EventHandler(this.chkHide_CheckedChanged);
            // 
            // CallCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 569);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lbHeading);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CallCompare";
            this.Text = "SQL Stored procedure call comparer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvResult;
        private System.Windows.Forms.ColumnHeader chParameter;
        private System.Windows.Forms.ColumnHeader chVal1;
        private System.Windows.Forms.ColumnHeader chVal2;
        private System.Windows.Forms.Label lbHeading;
        private System.Windows.Forms.ColumnHeader chDbType;
        private System.Windows.Forms.CheckBox chkHideSame;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LabeledTextbox txtDefinition;
        private LabeledTextbox txtCall2;
        private LabeledTextbox txtCall1;
        private System.Windows.Forms.CheckBox chkHideNull;
        private System.Windows.Forms.CheckBox chkHideDefault;
    }
}

