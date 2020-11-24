namespace EIS
{
    partial class FormJournalOperation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormJournalOperation));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxRequest = new System.Windows.Forms.ToolStripComboBox();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonBuy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonChange = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxCount = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBoxName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxMaterial = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBoxRemains = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxMPrice = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxSumBuy = new System.Windows.Forms.ToolStripTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRequested = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 48);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1323, 322);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(46, 27);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(112, 20);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.bindingNavigator1.CountItem = null;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBoxName,
            this.toolStripLabel2,
            this.toolStripComboBoxRequest,
            this.toolStripLabel5,
            this.toolStripComboBoxMaterial,
            this.toolStripLabel6,
            this.toolStripTextBoxRemains,
            this.toolStripLabel7,
            this.toolStripTextBoxMPrice,
            this.bindingNavigatorSeparator2,
            this.toolStripButtonBuy,
            this.toolStripButtonChange,
            this.toolStripButtonDel,
            this.toolStripLabel3,
            this.toolStripTextBoxCount,
            this.toolStripLabel8,
            this.toolStripTextBoxSumBuy});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = null;
            this.bindingNavigator1.MoveLastItem = null;
            this.bindingNavigator1.MoveNextItem = null;
            this.bindingNavigator1.MovePreviousItem = null;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = null;
            this.bindingNavigator1.Size = new System.Drawing.Size(1457, 27);
            this.bindingNavigator1.TabIndex = 8;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(70, 24);
            this.toolStripLabel1.Text = "Имя заявки";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(84, 24);
            this.toolStripLabel2.Text = "Номер заявки";
            // 
            // toolStripComboBoxRequest
            // 
            this.toolStripComboBoxRequest.Name = "toolStripComboBoxRequest";
            this.toolStripComboBoxRequest.Size = new System.Drawing.Size(92, 27);
            this.toolStripComboBoxRequest.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxRequest_SelectedIndexChanged);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonBuy
            // 
            this.toolStripButtonBuy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBuy.Image")));
            this.toolStripButtonBuy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBuy.Name = "toolStripButtonBuy";
            this.toolStripButtonBuy.Size = new System.Drawing.Size(78, 24);
            this.toolStripButtonBuy.Text = "Покупка";
            this.toolStripButtonBuy.Click += new System.EventHandler(this.toolStripButtonBuy_Click);
            // 
            // toolStripButtonChange
            // 
            this.toolStripButtonChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonChange.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonChange.Image")));
            this.toolStripButtonChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonChange.Name = "toolStripButtonChange";
            this.toolStripButtonChange.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonChange.Text = "Редактировать";
            this.toolStripButtonChange.Click += new System.EventHandler(this.toolStripButtonChange_Click);
            // 
            // toolStripButtonDel
            // 
            this.toolStripButtonDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDel.Image")));
            this.toolStripButtonDel.Name = "toolStripButtonDel";
            this.toolStripButtonDel.RightToLeftAutoMirrorImage = true;
            this.toolStripButtonDel.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonDel.Text = "Удалить";
            this.toolStripButtonDel.Click += new System.EventHandler(this.toolStripButtonDel_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(95, 24);
            this.toolStripLabel3.Text = "Кол-во покупки";
            // 
            // toolStripTextBoxCount
            // 
            this.toolStripTextBoxCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxCount.Name = "toolStripTextBoxCount";
            this.toolStripTextBoxCount.Size = new System.Drawing.Size(76, 27);
            this.toolStripTextBoxCount.TextChanged += new System.EventHandler(this.toolStripTextBoxCount_TextChanged);
            // 
            // toolStripTextBoxName
            // 
            this.toolStripTextBoxName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxName.Name = "toolStripTextBoxName";
            this.toolStripTextBoxName.Size = new System.Drawing.Size(76, 27);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(62, 24);
            this.toolStripLabel5.Text = "Материал";
            // 
            // toolStripComboBoxMaterial
            // 
            this.toolStripComboBoxMaterial.Name = "toolStripComboBoxMaterial";
            this.toolStripComboBoxMaterial.Size = new System.Drawing.Size(92, 27);
            this.toolStripComboBoxMaterial.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxMaterial_SelectedIndexChanged);
            // 
            // toolStripTextBoxRemains
            // 
            this.toolStripTextBoxRemains.Enabled = false;
            this.toolStripTextBoxRemains.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxRemains.Name = "toolStripTextBoxRemains";
            this.toolStripTextBoxRemains.Size = new System.Drawing.Size(76, 27);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(51, 24);
            this.toolStripLabel6.Text = "Остаток";
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(35, 24);
            this.toolStripLabel7.Text = "Цена";
            // 
            // toolStripTextBoxMPrice
            // 
            this.toolStripTextBoxMPrice.Enabled = false;
            this.toolStripTextBoxMPrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxMPrice.Name = "toolStripTextBoxMPrice";
            this.toolStripTextBoxMPrice.Size = new System.Drawing.Size(76, 27);
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(94, 24);
            this.toolStripLabel8.Text = "Сумма покупки";
            // 
            // toolStripTextBoxSumBuy
            // 
            this.toolStripTextBoxSumBuy.Enabled = false;
            this.toolStripTextBoxSumBuy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxSumBuy.Name = "toolStripTextBoxSumBuy";
            this.toolStripTextBoxSumBuy.Size = new System.Drawing.Size(76, 27);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Дата";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(433, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Заказано в заявке";
            // 
            // textBoxRequested
            // 
            this.textBoxRequested.Enabled = false;
            this.textBoxRequested.Location = new System.Drawing.Point(543, 27);
            this.textBoxRequested.Name = "textBoxRequested";
            this.textBoxRequested.Size = new System.Drawing.Size(100, 20);
            this.textBoxRequested.TabIndex = 11;
            // 
            // FormJournalOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(1457, 368);
            this.Controls.Add(this.textBoxRequested);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormJournalOperation";
            this.Text = "Журнал операций";
            this.Load += new System.EventHandler(this.FormJournalOperation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxRequest;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonBuy;
        private System.Windows.Forms.ToolStripButton toolStripButtonChange;
        private System.Windows.Forms.ToolStripButton toolStripButtonDel;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxCount;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxName;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxMaterial;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxRemains;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxMPrice;
        private System.Windows.Forms.ToolStripLabel toolStripLabel8;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSumBuy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRequested;
    }
}