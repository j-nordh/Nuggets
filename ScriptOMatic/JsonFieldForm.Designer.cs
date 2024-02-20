namespace ScriptOMatic
{
    partial class JsonFieldForm
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
            this.lddColumn = new UtilClasses.Winforms.LabelledCombo();
            this.lddType = new UtilClasses.Winforms.LabelledCombo();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkList = new System.Windows.Forms.CheckBox();
            this.ltxtAlias = new UtilClasses.Winforms.LabelledTextbox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lddColumn
            // 
            this.lddColumn.Caption = "Column";
            this.lddColumn.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.lddColumn.DisplayMember = "";
            this.lddColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lddColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.lddColumn.Location = new System.Drawing.Point(3, 3);
            this.lddColumn.Name = "lddColumn";
            this.lddColumn.SelectedObject = null;
            this.lddColumn.Size = new System.Drawing.Size(116, 34);
            this.lddColumn.TabIndex = 0;
            this.lddColumn.Load += new System.EventHandler(this.lddColumn_Load);
            // 
            // lddType
            // 
            this.lddType.Caption = "Type";
            this.lddType.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.lddType.DisplayMember = "";
            this.lddType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lddType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.lddType.Location = new System.Drawing.Point(125, 3);
            this.lddType.Name = "lddType";
            this.lddType.SelectedObject = null;
            this.lddType.Size = new System.Drawing.Size(116, 34);
            this.lddType.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOk.Location = new System.Drawing.Point(23, 83);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(145, 83);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lddType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lddColumn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkList, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnOk, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ltxtAlias, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(244, 112);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // chkList
            // 
            this.chkList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkList.AutoSize = true;
            this.chkList.Location = new System.Drawing.Point(125, 50);
            this.chkList.Name = "chkList";
            this.chkList.Size = new System.Drawing.Size(58, 17);
            this.chkList.TabIndex = 4;
            this.chkList.Text = "Is a list";
            this.chkList.UseVisualStyleBackColor = true;
            // 
            // ltxtAlias
            // 
            this.ltxtAlias.Caption = "Alias";
            this.ltxtAlias.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.ltxtAlias.Location = new System.Drawing.Point(3, 43);
            this.ltxtAlias.Multiline = false;
            this.ltxtAlias.Name = "ltxtAlias";
            this.ltxtAlias.ReadOnly = false;
            this.ltxtAlias.Size = new System.Drawing.Size(116, 32);
            this.ltxtAlias.TabIndex = 5;
            this.ltxtAlias.UnitText = "";
            this.ltxtAlias.UsePasswordChar = false;
            // 
            // JsonFieldForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(244, 112);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(999, 160);
            this.MinimumSize = new System.Drawing.Size(200, 120);
            this.Name = "JsonFieldForm";
            this.Text = "Json Field";
            this.Load += new System.EventHandler(this.JsonFieldForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UtilClasses.Winforms.LabelledCombo lddColumn;
        private UtilClasses.Winforms.LabelledCombo lddType;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chkList;
        private UtilClasses.Winforms.LabelledTextbox ltxtAlias;
    }
}