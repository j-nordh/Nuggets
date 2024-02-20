namespace ScriptOMatic
{
    partial class AttributeEditor
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
            this.gbAttributeStyle = new System.Windows.Forms.GroupBox();
            this.rbPresentIfTrue = new System.Windows.Forms.RadioButton();
            this.rbStyleParent = new System.Windows.Forms.RadioButton();
            this.ctxtAttributeName = new UtilClasses.Winforms.CueTextBox();
            this.lcbAttributeColumn = new UtilClasses.Winforms.LabelledCombo();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ctxtType = new UtilClasses.Winforms.CueTextBox();
            this.gbAttributeStyle.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAttributeStyle
            // 
            this.gbAttributeStyle.Controls.Add(this.rbPresentIfTrue);
            this.gbAttributeStyle.Controls.Add(this.rbStyleParent);
            this.gbAttributeStyle.Location = new System.Drawing.Point(12, 77);
            this.gbAttributeStyle.Name = "gbAttributeStyle";
            this.gbAttributeStyle.Size = new System.Drawing.Size(218, 39);
            this.gbAttributeStyle.TabIndex = 39;
            this.gbAttributeStyle.TabStop = false;
            this.gbAttributeStyle.Text = "Style";
            // 
            // rbPresentIfTrue
            // 
            this.rbPresentIfTrue.AutoSize = true;
            this.rbPresentIfTrue.Location = new System.Drawing.Point(63, 19);
            this.rbPresentIfTrue.Name = "rbPresentIfTrue";
            this.rbPresentIfTrue.Size = new System.Drawing.Size(89, 17);
            this.rbPresentIfTrue.TabIndex = 1;
            this.rbPresentIfTrue.TabStop = true;
            this.rbPresentIfTrue.Text = "PresentIfTrue";
            this.rbPresentIfTrue.UseVisualStyleBackColor = true;
            this.rbPresentIfTrue.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // rbStyleParent
            // 
            this.rbStyleParent.AutoSize = true;
            this.rbStyleParent.Location = new System.Drawing.Point(6, 18);
            this.rbStyleParent.Name = "rbStyleParent";
            this.rbStyleParent.Size = new System.Drawing.Size(56, 17);
            this.rbStyleParent.TabIndex = 0;
            this.rbStyleParent.TabStop = true;
            this.rbStyleParent.Text = "Parent";
            this.rbStyleParent.UseVisualStyleBackColor = true;
            this.rbStyleParent.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // ctxtAttributeName
            // 
            this.ctxtAttributeName.Cue = "Attribute name";
            this.ctxtAttributeName.Location = new System.Drawing.Point(142, 25);
            this.ctxtAttributeName.Name = "ctxtAttributeName";
            this.ctxtAttributeName.Size = new System.Drawing.Size(88, 20);
            this.ctxtAttributeName.TabIndex = 37;
            // 
            // lcbAttributeColumn
            // 
            this.lcbAttributeColumn.Caption = "Column";
            this.lcbAttributeColumn.CaptionDockStyle = System.Windows.Forms.DockStyle.Top;
            this.lcbAttributeColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.lcbAttributeColumn.Location = new System.Drawing.Point(12, 12);
            this.lcbAttributeColumn.Name = "lcbAttributeColumn";
            this.lcbAttributeColumn.SelectedObject = null;
            this.lcbAttributeColumn.Size = new System.Drawing.Size(107, 38);
            this.lcbAttributeColumn.TabIndex = 38;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(147, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOk.Location = new System.Drawing.Point(24, 6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 40;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btnOk, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 123);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(246, 35);
            this.tableLayoutPanel1.TabIndex = 42;
            // 
            // ctxtType
            // 
            this.ctxtType.Cue = "Type name";
            this.ctxtType.Location = new System.Drawing.Point(142, 51);
            this.ctxtType.Name = "ctxtType";
            this.ctxtType.Size = new System.Drawing.Size(88, 20);
            this.ctxtType.TabIndex = 43;
            // 
            // AttributeEditor
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(246, 158);
            this.Controls.Add(this.ctxtType);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.gbAttributeStyle);
            this.Controls.Add(this.ctxtAttributeName);
            this.Controls.Add(this.lcbAttributeColumn);
            this.Name = "AttributeEditor";
            this.Text = "AttributeEditor";
            this.gbAttributeStyle.ResumeLayout(false);
            this.gbAttributeStyle.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbAttributeStyle;
        private System.Windows.Forms.RadioButton rbPresentIfTrue;
        private System.Windows.Forms.RadioButton rbStyleParent;
        private UtilClasses.Winforms.CueTextBox ctxtAttributeName;
        private UtilClasses.Winforms.LabelledCombo lcbAttributeColumn;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private UtilClasses.Winforms.CueTextBox ctxtType;
    }
}