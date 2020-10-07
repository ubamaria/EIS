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
    public partial class FormRequest : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private List<int> nameMaterialIds = new List<int>();
        private string sPath = Path.Combine(Application.StartupPath, "F:\\sql\\MyDb.db");
        public FormRequest()
        {
            InitializeComponent();
            MaterialName();
        }
        public void MaterialName()
        {
            string connectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "Select IdMaterial,Name from Material";
            SQLiteConnection connect = new SQLiteConnection(connectionString);
            connect.Open();

            var list = new List<string>();

            var cmd = new SQLiteCommand(selectCommand, connect);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                list.Add($"{rdr.GetValue(0)} ({rdr.GetValue(1)})");
                nameMaterialIds.Add(rdr.GetInt32(0));
            }
            ComboBox1.ComboBox.DataSource = list;
            connect.Close();
        }
        private void FormRequest_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
";New=False;Version=3";
            String selectCommand = "Select IdRequest, IdBuyer, RequestDate, Name from Request as req join Material as mat where req.IdMaterial = mat.IdMaterial ";
            selectTable(ConnectionString, selectCommand);
            String selectMaterial = "Select IdMaterial, Name from Material";
            selectCombo(ConnectionString, selectMaterial, ComboBox1, "IdMaterial");
            String selectBuyer = "Select IdBuyer from Buyer";
            selectCombo(ConnectionString, selectBuyer, ComboBox2, "IdBuyer");
            ComboBox2.SelectedIndex = -1;
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
        public void selectCombo(string ConnectionString, String selectCommand, ToolStripComboBox comboBox, string displayMember)
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
            ComboBox1.SelectedIndex = -1;
            ComboBox1.SelectedIndex = -1;
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
            String selectCommand = "select MAX(IdRequest) from Request";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            string txtSQLQuery = "insert into Request (IdRequest, IdMaterial, IdBuyer, RequestDate) values (" +
          (Convert.ToInt32(maxValue) + 1) + ", '" + ComboBox1.Text + "', '" + ComboBox2.Text + "', '" + DateTime.Now.ToString() + "')";
            ExecuteQuery(txtSQLQuery);
            selectCommand = "Select IdRequest, IdBuyer, RequestDate, Name from Request as req join Material as mat where req.IdMaterial = mat.IdMaterial";
            refreshForm(ConnectionString, selectCommand);
            ComboBox2.Text = "";
            ComboBox1.Text = "";
        }
        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                string idmaterial = dataGridView1[1, CurrentRow].Value.ToString();
                ComboBox1.Text = idmaterial;
                string idfio = dataGridView1[2, CurrentRow].Value.ToString();
                ComboBox2.Text = idfio;
            }
        }

        private void toolStripButtonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                string changeMaterial = ComboBox1.Text;
                String selectCommand = "update Request set IdMaterial='" + changeMaterial + "' where IdRequest = " + valueId;
                changeValue(ConnectionString, selectCommand);
                string changeFIO = ComboBox2.Text;
                selectCommand = "update Request set IdBuyer='" + changeFIO + "' where IdRequest = " + valueId;
                changeValue(ConnectionString, selectCommand);
                selectCommand = "Select IdRequest, IdBuyer, RequestDate, Name from Request as req join Material as mat where req.IdMaterial = mat.IdMaterial";
                refreshForm(ConnectionString, selectCommand);
                ComboBox1.Text = "";
                ComboBox2.Text = "";
            }
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                String selectCommand = "delete from Request where IdRequest =" + valueId;
                string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                changeValue(ConnectionString, selectCommand);
                selectCommand = "Select IdRequest, IdBuyer, RequestDate, Name from Request as req join Material as mat where req.IdMaterial = mat.IdMaterial";
                refreshForm(ConnectionString, selectCommand);
                ComboBox1.Text = "";
                ComboBox2.Text = "";
            }
        }
    }
}
