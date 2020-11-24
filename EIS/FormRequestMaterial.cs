using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EIS
{
    
    public partial class FormRequestMaterial : Form
    {
        private int? id;
        public int Id { set { id = value; } }

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, "F:\\sql\\MyDb.db");
        public FormRequestMaterial()
        {
            InitializeComponent();
        }

        private void FormRequestMaterial_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
";New=False;Version=3";
            String selectCommand = "Select Material.Name, RequestMaterial.Count FROM RequestMaterial Join Material On Material.IdMaterial=RequestMaterial.IdMaterial";
            selectTable(ConnectionString, selectCommand);
            String selectMaterial = "Select IdMaterial, Name from Material";
            selectCombo(ConnectionString, selectMaterial, toolStripComboBoxMaterial, "Name", "IdMaterial");
            String selectBuyer = "Select IdBuyer, FIO from Buyer";
            selectCombo(ConnectionString, selectBuyer, toolStripComboBoxBuyer, "FIO", "IdBuyer");
            toolStripComboBoxMaterial.SelectedIndex = -1;
            toolStripComboBoxBuyer.SelectedIndex = -1;
        }
        public void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            connect.Close();
        }
        public void selectCombo(string ConnectionString, String selectCommand, ToolStripComboBox comboBox, string displayMember, string valueMember)
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
        public object selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand(selectCommand, connect);
            SQLiteDataReader reader = command.ExecuteReader();
            object value = "";
            while (reader.Read())
            {
                value = reader[0];
            }
            connect.Close();
            return value;
        }
        public void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridView1.Update();
            dataGridView1.Refresh();
            toolStripComboBoxMaterial.SelectedIndex = -1;
            toolStripComboBoxBuyer.SelectedIndex = -1;
            toolStripTextBoxCount.Text = "";
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

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "select MAX(Id) from RequestMaterial";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            string txtSQLQuery = "insert into RequestMaterial (Id,IdRequest, IdMaterial, Count) values (" +
          (Convert.ToInt32(maxValue) + 1) + ", '" + toolStripComboBoxMaterial.ComboBox.SelectedValue + "', '" + toolStripTextBoxCount.Text + "')";
            ExecuteQuery(txtSQLQuery);
            selectCommand = "Select Material.Name, RequestMaterial.Count FROM RequestMaterial Join Material On Material.IdMaterial=RequestMaterial.IdMaterial";
            refreshForm(ConnectionString, selectCommand);
            toolStripComboBoxMaterial.SelectedIndex = -1;
            toolStripComboBoxBuyer.SelectedIndex = -1;
            toolStripTextBoxCount.Text = "";
        }
        private void FormRequestMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (id != null)
            {
                //change
            }
            else
            {
                //add
            }
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "select MAX(IdRequest) from Request";
            object maxValue = selectValue(ConnectionString, selectCommand);
            selectCommand = "select MAX(Id) from RequestMaterial";
            object idRM = selectValue(ConnectionString, selectCommand);

            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            string txtSQLQuery = "insert into Request (IdRequest, IdRequestMaterial, IdBuyer, Count, RequestDate) values (" +
          (Convert.ToInt32(maxValue) + 1) + ", '" + idRM + "', '" + toolStripComboBoxBuyer.ComboBox.SelectedValue + "', '" + toolStripTextBoxCount.Text + "', '" + DateTime.Now.ToString() + "')";
            ExecuteQuery(txtSQLQuery);
            selectCommand = "Select Request.IdRequest,Material.Name, Buyer.FIO, Request.Count, Request.RequestDate FROM Request Join Material On Material.IdMaterial=Request.IdMaterial Join Buyer On Buyer.IdBuyer=Request.IdBuyer";
            refreshForm(ConnectionString, selectCommand);
        }
    }
}
