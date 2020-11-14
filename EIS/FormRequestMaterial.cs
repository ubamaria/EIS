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
        private int _idRequest = -1;
        public int IdRequest { set { _idRequest = value; } }

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, Program.sPath);
        public FormRequestMaterial()
        {
            InitializeComponent();
        }

        private void FormRequestMaterial_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
";New=False;Version=3";
            String selectCommand = "Select RequestMaterial.Id, RequestMaterial.IdRequest, Material.Name, RequestMaterial.Count " +
                "FROM RequestMaterial Join Material On Material.IdMaterial=RequestMaterial.IdMaterial WHERE RequestMaterial.IdRequest ='" + _idRequest + "'";
            selectTable(ConnectionString, selectCommand);
            String selectMaterial = "Select IdMaterial, Name from Material";
            selectCombo(ConnectionString, selectMaterial, toolStripComboBoxMaterial, "Name", "IdMaterial");
            String selectBuyer = "Select IdBuyer, FIO from Buyer";
            selectCombo(ConnectionString, selectBuyer, toolStripComboBoxBuyer, "FIO", "IdBuyer");
            toolStripComboBoxMaterial.SelectedIndex = -1;
            if (_idRequest != -1)
            {
                selectCommand = "Select IdBuyer From request Where IdRequest =" + _idRequest;
                toolStripComboBoxBuyer.ComboBox.SelectedValue = Convert.ToInt32(selectValue(ConnectionString, selectCommand));
            }
            else
            {
                toolStripComboBoxBuyer.SelectedIndex = -1;
            }
            
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
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
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
            if (_idRequest == -1)
            {
                selectCommand = "select MAX(IdRequest) from Request";
                object idReqValue = selectValue(ConnectionString, selectCommand);
                if (Convert.ToString(idReqValue) == "")
                {
                    _idRequest = 0;
                }
                else
                {
                    _idRequest = Convert.ToInt32(idReqValue) + 1;
                }
            }
            string txtSQLQuery = "insert into RequestMaterial (Id, IdRequest, IdMaterial, Count) values (" +
          (Convert.ToInt32(maxValue) + 1) + ", '" + _idRequest + "', '" + toolStripComboBoxMaterial.ComboBox.SelectedValue + "', '" + toolStripTextBoxCount.Text + "')";
            ExecuteQuery(txtSQLQuery);
            selectCommand = "Select RequestMaterial.Id, RequestMaterial.IdRequest, Material.Name, RequestMaterial.Count " +
                "FROM RequestMaterial Join Material On Material.IdMaterial=RequestMaterial.IdMaterial WHERE RequestMaterial.IdRequest ='" + _idRequest + "'";
            refreshForm(ConnectionString, selectCommand);
        }

        private void toolStripButtonChange_Click(object sender, EventArgs e)
        {
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();

            string txtSQLQuery = "update RequestMaterial set idMaterial = '" + toolStripComboBoxMaterial.ComboBox.SelectedValue + "' where Id = " + valueId;
            ExecuteQuery(txtSQLQuery);
            txtSQLQuery = "update RequestMaterial set Count = '" + toolStripTextBoxCount.Text + "' where Id = " + valueId;
            ExecuteQuery(txtSQLQuery);

            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            string selectCommand = "Select RequestMaterial.Id, RequestMaterial.IdRequest, Material.Name, RequestMaterial.Count " +
                "FROM RequestMaterial Join Material On Material.IdMaterial=RequestMaterial.IdMaterial WHERE RequestMaterial.IdRequest ='" + _idRequest + "'";
            refreshForm(ConnectionString, selectCommand);
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            String selectCommand = "delete from RequestMaterial where Id=" + valueId;
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            changeValue(ConnectionString, selectCommand);
            selectCommand = "Select RequestMaterial.Id, RequestMaterial.IdRequest, Material.Name, RequestMaterial.Count " +
                "FROM RequestMaterial Join Material On Material.IdMaterial=RequestMaterial.IdMaterial WHERE RequestMaterial.IdRequest ='" + _idRequest + "'";
            refreshForm(ConnectionString, selectCommand);
        }

        private void FormRequestMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_idRequest != -1)
            {
                if (Convert.ToString(toolStripComboBoxBuyer.ComboBox.SelectedValue) == "")
                {
                    MessageBox.Show("Не выбран покупатель");
                    e.Cancel = true;
                    return;
                }
                string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                String selectCommand = "select IdRequest from Request where IdRequest = " + _idRequest;
                object idReqValue = selectValue(ConnectionString, selectCommand);
                selectCommand = "select SUM(Count) from RequestMaterial where IdRequest = " + _idRequest;
                object countReq = selectValue(ConnectionString, selectCommand);
                if (Convert.ToString(countReq) == "")
                {
                    countReq = 0;
                }
                if (Convert.ToString(idReqValue) == "")
                { //add
                    string txtSQLQuery = "insert into Request (IdRequest, IdBuyer, Count, RequestDate) values (" +
                        _idRequest + ", '" + toolStripComboBoxBuyer.ComboBox.SelectedValue + "', '" +
                        countReq + "', '" + DateTime.Now.ToString() + "')";
                    ExecuteQuery(txtSQLQuery);
                }
                else
                { //change
                    string txtSQLQuery = "update Request set IdBuyer ='" + toolStripComboBoxBuyer.ComboBox.SelectedValue + "' where IdRequest =" + _idRequest;
                    ExecuteQuery(txtSQLQuery);
                    txtSQLQuery = "update Request set Count ='" + countReq + "' where IdRequest =" + _idRequest;
                    ExecuteQuery(txtSQLQuery);
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            string materialId = dataGridView1[2, CurrentRow].Value.ToString();
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            string selectCommand = "select IdMaterial from Material Where Name = '" + materialId + "'";
            object material = selectValue(ConnectionString, selectCommand);
            toolStripComboBoxMaterial.ComboBox.SelectedValue = material;
            string countId = dataGridView1[3, CurrentRow].Value.ToString();
            toolStripTextBoxCount.Text = countId;
        }
    }
}
