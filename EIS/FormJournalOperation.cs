using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EIS
{
    public partial class FormJournalOperation : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, Program.sPath);

        public FormJournalOperation()
        {
            InitializeComponent();
        }

        private void FormJournalOperation_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "Select JO.IdJournalOfOperations, JO.NameBuy, JO.Date," +
                " JO.CountBuy, JO.SumBuy, JO.SumNDS, R.RequestDate, JO.IdRequest, JO.IdMaterial, B.FIO, M.Name, M.Remains" +
                " From JournalOfOperations JO" +
                " Join Request R On R.IdRequest = JO.IdRequest" +
                " Join Buyer B On R.IdBuyer = B.IdBuyer" +
                " Join Material M On JO.IdMaterial = M.IdMaterial";
            selectTable(ConnectionString, selectCommand);
            toolStripTextBoxName.Text = "Покупка по заявке";
            String selectRequest = "Select IdRequest from Request";
            selectCombo(ConnectionString, selectRequest, toolStripComboBoxRequest, "IdRequest", "IdRequest");
            toolStripComboBoxRequest.SelectedIndex = -1;
        }

        public void selectCombo(string ConnectionString, String selectCommand,
ToolStripComboBox comboBox, string displayMember, string valueMember)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            comboBox.ComboBox.DataSource = ds.Tables[0];
            comboBox.ComboBox.DisplayMember = displayMember;
            comboBox.ComboBox.ValueMember = valueMember;
            connect.Close();
        }

        public object selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand(selectCommand,
connect);
            SQLiteDataReader reader = command.ExecuteReader();
            object value = "";
            while (reader.Read())
            {
                value = reader[0];
            }
            connect.Close();
            return value;
        }
        private void ExecuteQuery(string txtQuery)
        {
            sql_con = new SQLiteConnection("Data Source=" + sPath +
           ";Version=3;New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }
        public void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridView1.Update();
            dataGridView1.Refresh();
            toolStripTextBoxName.Text = "Покупка по заявке";
            toolStripTextBoxCount.Text = "";
            toolStripComboBoxRequest.SelectedIndex = -1;
            toolStripComboBoxMaterial.SelectedIndex = -1;
        }
        public void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            dataGridView1.Columns[8].Visible = false;
            connect.Close();
        }
        public void changeValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteTransaction trans;
            SQLiteCommand cmd = new SQLiteCommand();
            trans = connect.BeginTransaction();
            cmd.Connection = connect;
            cmd.CommandText = selectCommand;
            cmd.ExecuteNonQuery();
            trans.Commit();
            connect.Close();
        }

        private void toolStripButtonBuy_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "select MAX(IdJournalOfOperations) from JournalOfOperations";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
            {
                maxValue = 0;
            }
           
            int RequestedCount = Convert.ToInt32(textBoxRequested.Text);
            int countBuy = Convert.ToInt32(toolStripTextBoxCount.Text);
            int RemainsCount = Convert.ToInt32(toolStripTextBoxRemains.Text);
            double MPrice = Convert.ToDouble(toolStripTextBoxMPrice.Text);

            if (RequestedCount > RemainsCount)
            {
                MessageBox.Show("Остатки материалов достаточны для удовлетворения заявки");
                return;
            }

            double SumBuy = MPrice * countBuy;
            string selectNDS = "select NDS from Material where IdMaterial='" + toolStripComboBoxMaterial.ComboBox.SelectedValue + "'";
            double SumNDS = SumBuy * Convert.ToDouble(selectValue(ConnectionString, selectNDS)) / 100;

            string txtSQLQuery = "insert into JournalOfOperations (IdJournalOfOperations, NameBuy, Date, CountBuy, SumBuy, " +
                "SumNDS, IdRequest, IdMaterial) values (" + (Convert.ToInt32(maxValue) + 1) + ", '" + toolStripTextBoxName.Text + "', '" + 
                dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', '" + countBuy + "', '" + SumBuy + "', '" + 
                SumNDS + "', '" + toolStripComboBoxRequest.ComboBox.SelectedValue + "', '" + toolStripComboBoxMaterial.ComboBox.SelectedValue + "')";
            ExecuteQuery(txtSQLQuery);

            selectCommand = "Select JO.IdJournalOfOperations, JO.NameBuy, JO.Date," +
                " JO.CountBuy, JO.SumBuy, JO.SumNDS, R.RequestDate, JO.IdRequest, JO.IdMaterial, B.FIO, M.Name, M.Remains" +
                " From JournalOfOperations JO" +
                " Join Request R On R.IdRequest = JO.IdRequest" +
                " Join Buyer B On R.IdBuyer = B.IdBuyer" +
                " Join Material M On JO.IdMaterial = M.IdMaterial";
            refreshForm(ConnectionString, selectCommand);
        }

        private void toolStripButtonChange_Click(object sender, EventArgs e)
        {
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            string changeName = toolStripTextBoxName.Text;
            String selectName = "update JournalOfOperations set NameBuy='" + changeName + "'where IdJournalOfOperations = " + valueId;
            changeValue(ConnectionString, selectName);
            string changeDate = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            String selectDate = "update JournalOfOperations set Date='" + changeDate + "'where IdJournalOfOperations = " + valueId;
            changeValue(ConnectionString, selectDate);
            string changeCountBuy = toolStripTextBoxCount.Text;
            String selectCountBuy = "update JournalOfOperations set CountBuy='" + changeCountBuy + "'where IdJournalOfOperations = " + valueId;
            changeValue(ConnectionString, selectCountBuy);
            string changeSumBuy = toolStripTextBoxSumBuy.Text;
            String selectSumBuy = "update JournalOfOperations set SumBuy='" + changeSumBuy + "'where IdJournalOfOperations = " + valueId;
            changeValue(ConnectionString, selectSumBuy);
            string selectNDS = "select NDS from Material where IdMaterial='" + toolStripComboBoxMaterial.ComboBox.SelectedValue + "'";
            string changeSumNDS = Convert.ToString(Convert.ToDouble(changeSumBuy) * Convert.ToDouble(selectValue(ConnectionString, selectNDS))/100);
            String selectSumNDS= "update JournalOfOperations set SumNDS='" + changeSumNDS + "'where IdJournalOfOperations = " + valueId;
            changeValue(ConnectionString, selectSumNDS);
            string changeIdRequest = toolStripComboBoxRequest.ComboBox.SelectedValue.ToString();
            String selectIdRequest = "update JournalOfOperations set IdRequest='" + changeIdRequest + "'where IdJournalOfOperations = " + valueId;
            changeValue(ConnectionString, selectIdRequest);
            string changeIdMaterial = toolStripComboBoxMaterial.ComboBox.SelectedValue.ToString();
            String selectIdMaterial = "update JournalOfOperations set IdMaterial='" + changeIdMaterial + "'where IdJournalOfOperations = " + valueId;
            changeValue(ConnectionString, selectIdMaterial);

            string selectCommand = "Select JO.IdJournalOfOperations, JO.NameBuy, JO.Date," +
                " JO.CountBuy, JO.SumBuy, JO.SumNDS, R.RequestDate, JO.IdRequest, JO.IdMaterial, B.FIO, M.Name, M.Remains" +
                " From JournalOfOperations JO" +
                " Join Request R On R.IdRequest = JO.IdRequest" +
                " Join Buyer B On R.IdBuyer = B.IdBuyer" +
                " Join Material M On JO.IdMaterial = M.IdMaterial";
            refreshForm(ConnectionString, selectCommand);
        }

        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение idMOL выбранной строки
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            String selectCommand = "delete from JournalOfOperations where IdJournalOfOperations=" + valueId;
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            changeValue(ConnectionString, selectCommand);
            //обновление dataGridView1
            selectCommand = "Select JO.IdJournalOfOperations, JO.NameBuy, JO.Date," +
                " JO.CountBuy, JO.SumBuy, JO.SumNDS, R.RequestDate, JO.IdRequest, JO.IdMaterial, B.FIO, M.Name, M.Remains" +
                " From JournalOfOperations JO" +
                " Join Request R On R.IdRequest = JO.IdRequest" +
                " Join Buyer B On R.IdBuyer = B.IdBuyer" +
                " Join Material M On JO.IdMaterial = M.IdMaterial";
            refreshForm(ConnectionString, selectCommand);
        }

        private void toolStripComboBoxRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectMaterial = "Select M.Name, M.IdMaterial from RequestMaterial RM " +
                "Join Material M On RM.IdMaterial = M.IdMaterial " +
                "Where RM.IdRequest = '" + toolStripComboBoxRequest.ComboBox.SelectedValue + "'";
            selectCombo(ConnectionString, selectMaterial, toolStripComboBoxMaterial, "Name", "IdMaterial");
            toolStripComboBoxMaterial.SelectedIndex = -1;
        }

        private void toolStripComboBoxMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

            String selectCount = "select Count from RequestMaterial where IdRequest='" + toolStripComboBoxRequest.ComboBox.SelectedValue + "' " +
                "AND IdMaterial='" + toolStripComboBoxMaterial.ComboBox.SelectedValue + "'";
            textBoxRequested.Text = Convert.ToString(selectValue(ConnectionString, selectCount));

            String selectRemains = "select Remains from Material where IdMaterial='" + toolStripComboBoxMaterial.ComboBox.SelectedValue + "'";
            toolStripTextBoxRemains.Text = Convert.ToString(selectValue(ConnectionString, selectRemains));

            String selectMPrice = "select CostMaterial from Material where IdMaterial='" + toolStripComboBoxMaterial.ComboBox.SelectedValue + "'";
            toolStripTextBoxMPrice.Text = Convert.ToString(selectValue(ConnectionString, selectMPrice));
        }

        private void toolStripTextBoxCount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int countBuy = Convert.ToInt32(toolStripTextBoxCount.Text);
                double MPrice = Convert.ToDouble(toolStripTextBoxMPrice.Text);
                double SumBuy = MPrice * countBuy;
                toolStripTextBoxSumBuy.Text = SumBuy.ToString();
            }catch(Exception){ }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение Name выбранной строки
            string nameId = dataGridView1[1, CurrentRow].Value.ToString();
            toolStripTextBoxName.Text = nameId;
            string dateId = dataGridView1[2, CurrentRow].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dateId);
            string countBuyId = dataGridView1[3, CurrentRow].Value.ToString();
            toolStripTextBoxCount.Text = countBuyId;
            int RequestId = Convert.ToInt32(dataGridView1[7, CurrentRow].Value);
            toolStripComboBoxRequest.ComboBox.SelectedValue = RequestId;
            int MaterialId = Convert.ToInt32(dataGridView1[8, CurrentRow].Value);
            toolStripComboBoxMaterial.ComboBox.SelectedValue = MaterialId;

            double MPrice = Convert.ToDouble(toolStripTextBoxMPrice.Text);
            int countBuy = Convert.ToInt32(toolStripTextBoxCount.Text);
            double SumBuy = MPrice * countBuy;
            toolStripTextBoxSumBuy.Text = SumBuy.ToString();
        }
    }
}
