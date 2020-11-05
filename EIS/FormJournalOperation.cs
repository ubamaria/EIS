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
        private string sPath = Path.Combine(Application.StartupPath, "F:\\sql\\MyDb.db");

        public FormJournalOperation()
        {
            InitializeComponent();
        }

        private void FormJournalOperation_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "Select JournalOfOperations.IdJournalOfOperations,JournalOfOperations.NameBuy,JournalOfOperations.Date,JournalOfOperations.Ostatok,JournalOfOperations.CountBuy,JournalOfOperations.SumBuy,JournalOfOperations.SumNDS, " 
                + "Request.IdRequest,Request.Count,Request.RequestDate, Material.Name, Buyer.FIO from JournalOfOperations Join Request On Request.IdRequest=JournalOfOperations.IdRequest Join Material On Material.IdMaterial=Request.IdMaterial Join Buyer On Buyer.IdBuyer=Request.IdBuyer";
            selectTable(ConnectionString, selectCommand);

            String selectRequest = "Select * from Request";
            selectCombo(ConnectionString, selectRequest, toolStripComboBox1, "IdRequest", "IdRequest");
            toolStripComboBox1.SelectedIndex = -1;
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
            toolStripTextBox1.Text = "";
            toolStripTextBox2.Text = "";
            toolStripComboBox1.SelectedIndex = -1;
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = dateTimePicker1.Value.ToString("dd.MM.yyyy");
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
           
            String selectCount = "select Count from Request where IdRequest='" + toolStripComboBox1.ComboBox.SelectedValue + "'";
            object CountValue = selectValue(ConnectionString, selectCount);
            int count = System.Convert.ToInt32(toolStripTextBox2.Text);
           int ostatok = (System.Convert.ToInt32(CountValue) - count);
            String selectIdMaterial = "select IdMaterial from Request where IdMaterial='" + toolStripComboBox1.ComboBox.SelectedValue + "'";
            object IdMaterialValue = selectValue(ConnectionString, selectIdMaterial);
            String selectSumBuy = "select CostMaterial from Material where IdMaterial='" + System.Convert.ToInt32(IdMaterialValue) + "'";
            object SumBuyValue = selectValue(ConnectionString, selectSumBuy);
            double SumBuy = (System.Convert.ToDouble(SumBuyValue) * System.Convert.ToInt32(toolStripTextBox2.Text));
            if (ostatok < 0)
            {
                MessageBox.Show("Материалов меньше, чем есть на в заявке");
                return;
            }
            string n = "Покупка по заявке";
            string txtSQLQuery = "insert into JournalOfOperations (IdJournalOfOperations, NameBuy, Date, Ostatok, CountBuy, SumBuy,SumNDS,IdRequest) values (" + (Convert.ToInt32(maxValue) + 1) + ", '" + n + "', '" + toolStripTextBox1.Text + "', '" + ostatok + "', '" + toolStripTextBox2 + "', '" + SumBuy + "', '" + n + "', '" + toolStripComboBox1.ComboBox.SelectedValue + "')";
            ExecuteQuery(txtSQLQuery);
            //обновление dataGridView
            selectCommand = "Select JournalOfOperations.IdJournalOfOperations,JournalOfOperations.NameBuy,JournalOfOperations.Date,JournalOfOperations.Ostatok,JournalOfOperations.CountBuy,JournalOfOperations.SumBuy,JournalOfOperations.SumNDS, "
                + "Request.IdRequest,Request.Count,Request.RequestDate, Material.Name, Buyer.FIO from JournalOfOperations Join Request On Request.IdRequest=JournalOfOperations.IdRequest Join Material On Material.IdMaterial=Request.IdMaterial Join Buyer On Buyer.IdBuyer=Request.IdBuyer";
            refreshForm(ConnectionString, selectCommand);
            toolStripTextBox1.Text = "";
            toolStripTextBox2.Text = "";
            toolStripComboBox1.Text = "";
        }

        private void toolStripButtonChange_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            
        }
    }
}
