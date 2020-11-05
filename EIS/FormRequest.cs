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
        private string sPath = Path.Combine(Application.StartupPath, "F:\\sql\\MyDb.db");
        public FormRequest()
        {
            InitializeComponent();
        }

        private void FormRequest_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
";New=False;Version=3";
            String selectCommand = "Select Request.IdRequest,Material.Name, Buyer.FIO, Request.Count, Request.RequestDate FROM Request Join Material On Material.IdMaterial=Request.IdMaterial Join Buyer On Buyer.IdBuyer=Request.IdBuyer";
            selectTable(ConnectionString, selectCommand);
            String selectMaterial = "Select IdMaterial, Name from Material";
            selectCombo(ConnectionString, selectMaterial, toolStripComboBox1, "Name","IdMaterial");
            String selectBuyer = "Select IdBuyer, FIO from Buyer";
            selectCombo(ConnectionString, selectBuyer, toolStripComboBox2, "FIO", "IdBuyer");
            toolStripComboBox1.SelectedIndex = -1;
            toolStripComboBox2.SelectedIndex = -1;
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
            toolStripComboBox1.SelectedIndex = -1;
            toolStripComboBox2.SelectedIndex = -1;
            toolStripTextBox1.Text = "";
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
            var form = new FormRequestMaterial();
            form.Show();////////////////////////////////


          //  if (toolStripComboBox1.Text == "")
          //  {
          //      MessageBox.Show("Выберите материал");
          //      return;
          //  }
          //  if (toolStripTextBox1.Text == "")
          //  {
          //      MessageBox.Show("Введите количество материалов");
          //      return;
          //  }
          //  if (toolStripComboBox2.Text == "")
          //  {
          //      MessageBox.Show("Выберите покупателя");
          //      return;
          //  }
          //  string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
          //  String selectCommand = "select MAX(IdRequest) from Request";
          //  object maxValue = selectValue(ConnectionString, selectCommand);
          //  if (Convert.ToString(maxValue) == "")
          //      maxValue = 0;
          //  string txtSQLQuery = "insert into Request (IdRequest,IdMaterial, IdBuyer, Count, RequestDate) values (" +
          //(Convert.ToInt32(maxValue) + 1) + ", '" + toolStripComboBox1.ComboBox.SelectedValue + "', '" + toolStripComboBox2.ComboBox.SelectedValue + "', '" + toolStripTextBox1.Text + "', '" + DateTime.Now.ToString() + "')";
          //  ExecuteQuery(txtSQLQuery);
          //  selectCommand = "Select Request.IdRequest,Material.Name, Buyer.FIO, Request.Count, Request.RequestDate FROM Request Join Material On Material.IdMaterial=Request.IdMaterial Join Buyer On Buyer.IdBuyer=Request.IdBuyer";
          //  refreshForm(ConnectionString, selectCommand);
          //  toolStripComboBox1.SelectedIndex = -1;
          //  toolStripComboBox2.SelectedIndex = -1;
          //  toolStripTextBox1.Text = "";
        }
        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                string idmaterial = dataGridView1[1, CurrentRow].Value.ToString();
                toolStripComboBox1.Text = idmaterial;
                string idfio = dataGridView1[2, CurrentRow].Value.ToString();
                toolStripComboBox2.Text = idfio;
                string count = dataGridView1[3, CurrentRow].Value.ToString();
                toolStripTextBox1.Text = count;
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
                selectCommand = "Select Request.IdRequest,Material.Name, Buyer.FIO, Request.Count, Request.RequestDate FROM Request Join Material On Material.IdMaterial=Request.IdMaterial Join Buyer On Buyer.IdBuyer=Request.IdBuyer";
                refreshForm(ConnectionString, selectCommand);
                toolStripComboBox1.Text = "";
                toolStripComboBox2.Text = "";
                toolStripTextBox1.Text = "";
            }
        }

        private void toolStripButtonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var form = new FormRequestMaterial();
                form.Id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            }////////////////////////////////////

             if (toolStripComboBox1.Text == "")
            {
                MessageBox.Show("Выберите материал");
                return;
            }
            if (toolStripTextBox1.Text == "")
            {
                MessageBox.Show("Введите количество материалов");
                return;
            }
            if (toolStripComboBox2.Text == "")
            {
                MessageBox.Show("Выберите покупателя");
                return;
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                string changeMaterial = toolStripComboBox1.ComboBox.SelectedValue.ToString();
                String selectCommand = "update Request set IdMaterial='" + changeMaterial + "' where IdRequest = " + valueId;
                changeValue(ConnectionString, selectCommand);
                string changeFIO = toolStripComboBox2.ComboBox.SelectedValue.ToString();
                selectCommand = "update Request set IdBuyer='" + changeFIO + "' where IdRequest = " + valueId;
                changeValue(ConnectionString, selectCommand);
                string changeCount = toolStripTextBox1.Text;
                selectCommand = "update Request set Count='" + changeCount + "' where IdRequest = " + valueId;
                changeValue(ConnectionString, selectCommand);
                selectCommand = "Select Request.IdRequest,Material.Name, Buyer.FIO, Request.Count, Request.RequestDate FROM Request Join Material On Material.IdMaterial=Request.IdMaterial Join Buyer On Buyer.IdBuyer=Request.IdBuyer";
                refreshForm(ConnectionString, selectCommand);
                toolStripComboBox1.SelectedIndex = -1;
                toolStripComboBox2.SelectedIndex = -1;
                toolStripTextBox1.Text = "";
            }
        }
    }
}
