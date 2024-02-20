using SupplyChain.Dto;

namespace ScriptOMatic.Pages
{
    partial class AggregateForm
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
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.ForeignKeyColumnLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.PrimaryKeyColumnLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.JsonFieldTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TableLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForTable = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForForeignKeyColumn = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForPrimaryKeyColumn = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForJsonField = new DevExpress.XtraLayout.LayoutControlItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.aggregateBS = new System.Windows.Forms.BindingSource(this.components);
            this.foreignKeyBS = new System.Windows.Forms.BindingSource(this.components);
            this.primaryKeyBS = new System.Windows.Forms.BindingSource(this.components);
            this.tableBS = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ForeignKeyColumnLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrimaryKeyColumnLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JsonFieldTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForForeignKeyColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForPrimaryKeyColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForJsonField)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aggregateBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.foreignKeyBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.primaryKeyBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableBS)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.ForeignKeyColumnLookUpEdit);
            this.dataLayoutControl1.Controls.Add(this.PrimaryKeyColumnLookUpEdit);
            this.dataLayoutControl1.Controls.Add(this.JsonFieldTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TableLookUpEdit);
            this.dataLayoutControl1.DataSource = this.aggregateBS;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(251, 324);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // ForeignKeyColumnLookUpEdit
            // 
            this.ForeignKeyColumnLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.aggregateBS, "ForeignKeyColumn", true));
            this.ForeignKeyColumnLookUpEdit.Location = new System.Drawing.Point(16, 80);
            this.ForeignKeyColumnLookUpEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ForeignKeyColumnLookUpEdit.Name = "ForeignKeyColumnLookUpEdit";
            this.ForeignKeyColumnLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ForeignKeyColumnLookUpEdit.Properties.DataSource = this.foreignKeyBS;
            this.ForeignKeyColumnLookUpEdit.Properties.DisplayMember = "Name";
            this.ForeignKeyColumnLookUpEdit.Properties.NullText = "";
            this.ForeignKeyColumnLookUpEdit.Properties.ValueMember = "Name";
            this.ForeignKeyColumnLookUpEdit.Size = new System.Drawing.Size(219, 22);
            this.ForeignKeyColumnLookUpEdit.StyleController = this.dataLayoutControl1;
            this.ForeignKeyColumnLookUpEdit.TabIndex = 5;
            // 
            // PrimaryKeyColumnLookUpEdit
            // 
            this.PrimaryKeyColumnLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.aggregateBS, "PrimaryKeyColumn", true));
            this.PrimaryKeyColumnLookUpEdit.Location = new System.Drawing.Point(16, 126);
            this.PrimaryKeyColumnLookUpEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PrimaryKeyColumnLookUpEdit.Name = "PrimaryKeyColumnLookUpEdit";
            this.PrimaryKeyColumnLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PrimaryKeyColumnLookUpEdit.Properties.DataSource = this.primaryKeyBS;
            this.PrimaryKeyColumnLookUpEdit.Properties.DisplayMember = "Name";
            this.PrimaryKeyColumnLookUpEdit.Properties.NullText = "";
            this.PrimaryKeyColumnLookUpEdit.Properties.ValueMember = "Name";
            this.PrimaryKeyColumnLookUpEdit.Size = new System.Drawing.Size(219, 22);
            this.PrimaryKeyColumnLookUpEdit.StyleController = this.dataLayoutControl1;
            this.PrimaryKeyColumnLookUpEdit.TabIndex = 6;
            // 
            // JsonFieldTextEdit
            // 
            this.JsonFieldTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.aggregateBS, "JsonField", true));
            this.JsonFieldTextEdit.Location = new System.Drawing.Point(16, 172);
            this.JsonFieldTextEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.JsonFieldTextEdit.Name = "JsonFieldTextEdit";
            this.JsonFieldTextEdit.Size = new System.Drawing.Size(219, 22);
            this.JsonFieldTextEdit.StyleController = this.dataLayoutControl1;
            this.JsonFieldTextEdit.TabIndex = 7;
            // 
            // TableLookUpEdit
            // 
            this.TableLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.aggregateBS, "Table", true));
            this.TableLookUpEdit.Location = new System.Drawing.Point(16, 34);
            this.TableLookUpEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TableLookUpEdit.Name = "TableLookUpEdit";
            this.TableLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TableLookUpEdit.Properties.DataSource = this.foreignKeyBS;
            this.TableLookUpEdit.Properties.DisplayMember = "Name";
            this.TableLookUpEdit.Properties.NullText = "";
            this.TableLookUpEdit.Properties.ValueMember = "Name";
            this.TableLookUpEdit.Size = new System.Drawing.Size(219, 22);
            this.TableLookUpEdit.StyleController = this.dataLayoutControl1;
            this.TableLookUpEdit.TabIndex = 8;
            this.TableLookUpEdit.EditValueChanged += new System.EventHandler(this.TableLookUpEdit_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(251, 324);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForTable,
            this.ItemForForeignKeyColumn,
            this.ItemForPrimaryKeyColumn,
            this.ItemForJsonField});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(225, 300);
            // 
            // ItemForTable
            // 
            this.ItemForTable.Control = this.TableLookUpEdit;
            this.ItemForTable.Location = new System.Drawing.Point(0, 0);
            this.ItemForTable.Name = "ItemForTable";
            this.ItemForTable.Size = new System.Drawing.Size(225, 46);
            this.ItemForTable.Text = "Table";
            this.ItemForTable.TextLocation = DevExpress.Utils.Locations.Top;
            this.ItemForTable.TextSize = new System.Drawing.Size(115, 16);
            // 
            // ItemForForeignKeyColumn
            // 
            this.ItemForForeignKeyColumn.Control = this.ForeignKeyColumnLookUpEdit;
            this.ItemForForeignKeyColumn.Location = new System.Drawing.Point(0, 46);
            this.ItemForForeignKeyColumn.Name = "ItemForForeignKeyColumn";
            this.ItemForForeignKeyColumn.Size = new System.Drawing.Size(225, 46);
            this.ItemForForeignKeyColumn.Text = "Foreign Key Column";
            this.ItemForForeignKeyColumn.TextLocation = DevExpress.Utils.Locations.Top;
            this.ItemForForeignKeyColumn.TextSize = new System.Drawing.Size(115, 16);
            // 
            // ItemForPrimaryKeyColumn
            // 
            this.ItemForPrimaryKeyColumn.Control = this.PrimaryKeyColumnLookUpEdit;
            this.ItemForPrimaryKeyColumn.Location = new System.Drawing.Point(0, 92);
            this.ItemForPrimaryKeyColumn.Name = "ItemForPrimaryKeyColumn";
            this.ItemForPrimaryKeyColumn.Size = new System.Drawing.Size(225, 46);
            this.ItemForPrimaryKeyColumn.Text = "Primary Key Column";
            this.ItemForPrimaryKeyColumn.TextLocation = DevExpress.Utils.Locations.Top;
            this.ItemForPrimaryKeyColumn.TextSize = new System.Drawing.Size(115, 16);
            // 
            // ItemForJsonField
            // 
            this.ItemForJsonField.Control = this.JsonFieldTextEdit;
            this.ItemForJsonField.Location = new System.Drawing.Point(0, 138);
            this.ItemForJsonField.Name = "ItemForJsonField";
            this.ItemForJsonField.Size = new System.Drawing.Size(225, 162);
            this.ItemForJsonField.Text = "Json Field";
            this.ItemForJsonField.TextLocation = DevExpress.Utils.Locations.Top;
            this.ItemForJsonField.TextSize = new System.Drawing.Size(115, 16);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btnOk, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 283);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(251, 41);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOk.Location = new System.Drawing.Point(25, 8);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(74, 25);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.Location = new System.Drawing.Point(150, 8);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // foreignKeyBS
            // 
            this.foreignKeyBS.DataSource = typeof(ColumnProperties);
            // 
            // primaryKeyBS
            // 
            this.primaryKeyBS.DataSource = typeof(ColumnProperties);
            // 
            // tableBS
            // 
            this.tableBS.DataSource = typeof(TableNode);
            // 
            // AggregateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 324);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dataLayoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AggregateForm";
            this.Text = "AggregateForm";
            this.Load += new System.EventHandler(this.AggregateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ForeignKeyColumnLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrimaryKeyColumnLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JsonFieldTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForForeignKeyColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForPrimaryKeyColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForJsonField)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aggregateBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.foreignKeyBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.primaryKeyBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableBS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.BindingSource aggregateBS;
        private DevExpress.XtraEditors.LookUpEdit ForeignKeyColumnLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit PrimaryKeyColumnLookUpEdit;
        private DevExpress.XtraEditors.TextEdit JsonFieldTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTable;
        private DevExpress.XtraLayout.LayoutControlItem ItemForForeignKeyColumn;
        private DevExpress.XtraLayout.LayoutControlItem ItemForPrimaryKeyColumn;
        private DevExpress.XtraLayout.LayoutControlItem ItemForJsonField;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private DevExpress.XtraEditors.LookUpEdit TableLookUpEdit;
        private System.Windows.Forms.BindingSource foreignKeyBS;
        private System.Windows.Forms.BindingSource tableBS;
        private System.Windows.Forms.BindingSource primaryKeyBS;
    }
}