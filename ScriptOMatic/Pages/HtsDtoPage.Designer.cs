namespace ScriptOMatic.Pages
{
    partial class HtsDtoPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.lblClassName = new System.Windows.Forms.Label();
            this.btnSelectTable = new System.Windows.Forms.Button();
            this.chkProperties = new System.Windows.Forms.CheckBox();
            this.chkStateful = new System.Windows.Forms.CheckBox();
            this.ctxtNamespace = new UtilClasses.Winforms.CueTextBox();
            this.chkHasWriteId = new System.Windows.Forms.CheckBox();
            this.chkCloneable = new System.Windows.Forms.CheckBox();
            this.gbImplement = new System.Windows.Forms.GroupBox();
            this.gbImplement.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(8, 47);
            this.txtClassName.Margin = new System.Windows.Forms.Padding(4);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(131, 22);
            this.txtClassName.TabIndex = 0;
            this.txtClassName.TextChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.Location = new System.Drawing.Point(4, 27);
            this.lblClassName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(85, 17);
            this.lblClassName.TabIndex = 1;
            this.lblClassName.Text = "Class name:";
            // 
            // btnSelectTable
            // 
            this.btnSelectTable.Location = new System.Drawing.Point(147, 41);
            this.btnSelectTable.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectTable.Name = "btnSelectTable";
            this.btnSelectTable.Size = new System.Drawing.Size(91, 28);
            this.btnSelectTable.TabIndex = 2;
            this.btnSelectTable.Text = "Select table";
            this.btnSelectTable.UseVisualStyleBackColor = true;
            this.btnSelectTable.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkProperties
            // 
            this.chkProperties.AutoSize = true;
            this.chkProperties.Checked = true;
            this.chkProperties.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProperties.Location = new System.Drawing.Point(8, 104);
            this.chkProperties.Name = "chkProperties";
            this.chkProperties.Size = new System.Drawing.Size(95, 21);
            this.chkProperties.TabIndex = 3;
            this.chkProperties.Text = "Properties";
            this.chkProperties.UseVisualStyleBackColor = true;
            this.chkProperties.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkStateful
            // 
            this.chkStateful.AutoSize = true;
            this.chkStateful.Location = new System.Drawing.Point(6, 21);
            this.chkStateful.Name = "chkStateful";
            this.chkStateful.Size = new System.Drawing.Size(81, 21);
            this.chkStateful.TabIndex = 4;
            this.chkStateful.Text = "IStateful";
            this.chkStateful.UseVisualStyleBackColor = true;
            this.chkStateful.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // ctxtNamespace
            // 
            this.ctxtNamespace.Cue = "Namespace";
            this.ctxtNamespace.Location = new System.Drawing.Point(8, 76);
            this.ctxtNamespace.Name = "ctxtNamespace";
            this.ctxtNamespace.Size = new System.Drawing.Size(131, 22);
            this.ctxtNamespace.TabIndex = 5;
            this.ctxtNamespace.Text = "Recs.Dto";
            this.ctxtNamespace.TextChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkHasWriteId
            // 
            this.chkHasWriteId.AutoSize = true;
            this.chkHasWriteId.Location = new System.Drawing.Point(6, 48);
            this.chkHasWriteId.Name = "chkHasWriteId";
            this.chkHasWriteId.Size = new System.Drawing.Size(102, 21);
            this.chkHasWriteId.TabIndex = 6;
            this.chkHasWriteId.Text = "IHasWriteId";
            this.chkHasWriteId.UseVisualStyleBackColor = true;
            this.chkHasWriteId.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // chkCloneable
            // 
            this.chkCloneable.AutoSize = true;
            this.chkCloneable.Location = new System.Drawing.Point(6, 75);
            this.chkCloneable.Name = "chkCloneable";
            this.chkCloneable.Size = new System.Drawing.Size(96, 21);
            this.chkCloneable.TabIndex = 7;
            this.chkCloneable.Text = "ICloneable";
            this.chkCloneable.UseVisualStyleBackColor = true;
            this.chkCloneable.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // gbImplement
            // 
            this.gbImplement.Controls.Add(this.chkStateful);
            this.gbImplement.Controls.Add(this.chkCloneable);
            this.gbImplement.Controls.Add(this.chkHasWriteId);
            this.gbImplement.Location = new System.Drawing.Point(8, 131);
            this.gbImplement.Name = "gbImplement";
            this.gbImplement.Size = new System.Drawing.Size(139, 107);
            this.gbImplement.TabIndex = 8;
            this.gbImplement.TabStop = false;
            this.gbImplement.Text = "Implement";
            // 
            // HtsDtoPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbImplement);
            this.Controls.Add(this.ctxtNamespace);
            this.Controls.Add(this.chkProperties);
            this.Controls.Add(this.btnSelectTable);
            this.Controls.Add(this.lblClassName);
            this.Controls.Add(this.txtClassName);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "HtsDtoPage";
            this.gbImplement.ResumeLayout(false);
            this.gbImplement.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label lblClassName;
        private System.Windows.Forms.Button btnSelectTable;
        private System.Windows.Forms.CheckBox chkProperties;
        private System.Windows.Forms.CheckBox chkStateful;
        private UtilClasses.Winforms.CueTextBox ctxtNamespace;
        private System.Windows.Forms.CheckBox chkHasWriteId;
        private System.Windows.Forms.CheckBox chkCloneable;
        private System.Windows.Forms.GroupBox gbImplement;
    }
}
