using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EIS
{
    public partial class FormBuyer : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, Program.sPath);
        private int minLenght = 2;
        private int maxLenght = 50;

        public FormBuyer()
        {
            InitializeComponent();
        }

        private void FormBuyer_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
";New=False;Version=3";
            String selectCommand = "Select * from Buyer";
            selectTable(ConnectionString, selectCommand);
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
            toolStripTextBox3.Text = "";

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
        private void dataGridView1_CellMouseClick(object sender,
DataGridViewCellMouseEventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение Name выбранной строки
            string nameId = dataGridView1[1, CurrentRow].Value.ToString();
            toolStripTextBox1.Text = nameId;
            string mailId = dataGridView1[2, CurrentRow].Value.ToString();
            toolStripTextBox2.Text = mailId;
            string ndsId = dataGridView1[3, CurrentRow].Value.ToString();
            toolStripTextBox3.Text = ndsId;
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            String selectCommand = "select MAX(IdBuyer) from Buyer";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            if (!(toolStripTextBox1.Text.Length > minLenght && toolStripTextBox1.Text.Length < maxLenght))
            {
                MessageBox.Show("ФИО должно содержать не менее 3 и не более 50 символов");
                return;
            }

            //вставка в таблицу
            string patternmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            string patternphone = @"^^[(8|\+7]\d+$";
            if (!(Regex.IsMatch(toolStripTextBox2.Text, patternmail, RegexOptions.IgnoreCase)))
            {
                MessageBox.Show("Почта введена не корректно");
                return;
            }
            if (!(Regex.IsMatch(toolStripTextBox3.Text, patternphone, RegexOptions.IgnoreCase)))
            {
                MessageBox.Show("Номер введен не корректно");
                return;
            }
            string txtSQLQuery = "insert into Buyer (IdBuyer, FIO, Mail, Phone) values (" +
        (Convert.ToInt32(maxValue) + 1) + ", '" + toolStripTextBox1.Text + "', '" + toolStripTextBox2.Text + "', '" + toolStripTextBox3.Text + "')";
            ExecuteQuery(txtSQLQuery);
            //обновление dataGridView1
            selectCommand = "select * from Buyer";
            refreshForm(ConnectionString, selectCommand);
            toolStripTextBox1.Text = "";
            toolStripTextBox2.Text = "";
            toolStripTextBox3.Text = "";
           
        }

            private void toolStripButtonChange_Click(object sender, EventArgs e)
        {
            if (!(toolStripTextBox1.Text.Length > minLenght && toolStripTextBox1.Text.Length < maxLenght))
            {
                MessageBox.Show("ФИО должно содержать не менее 3 и не более 50 символов");
                return;
            }
            string patternmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            string patternphone = @"^^[(8|\+7]\d+$";
            if (!(Regex.IsMatch(toolStripTextBox2.Text, patternmail, RegexOptions.IgnoreCase)))
            {
                MessageBox.Show("Почта введена не корректно");
                return;
            }
            if (!(Regex.IsMatch(toolStripTextBox3.Text, patternphone, RegexOptions.IgnoreCase)))
            {
                MessageBox.Show("Номер введен не корректно");
                return;
            }
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение Name выбранной строки
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            string changeName = toolStripTextBox1.Text;
            //обновление Name
            String selectCommand = "update Buyer set FIO='" + changeName + "'where IdBuyer = " + valueId;
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            changeValue(ConnectionString, selectCommand);
            string changeMail = toolStripTextBox2.Text;
            String selectMail = "update Buyer set Mail='" + changeMail + "'where IdBuyer = " + valueId;
            changeValue(ConnectionString, selectMail);
            string changePhone = toolStripTextBox3.Text;
            String selectPhone = "update Buyer set Phone='" + changePhone + "'where IdBuyer = " + valueId;
            changeValue(ConnectionString, selectPhone);
            //обновление dataGridView1
            selectCommand = "select * from Buyer";
            refreshForm(ConnectionString, selectCommand);
            toolStripTextBox1.Text = "";
            toolStripTextBox2.Text = "";
            toolStripTextBox3.Text = "";
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение idMOL выбранной строки
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            String selectCommand = "delete from Buyer where IdBuyer=" + valueId;
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            changeValue(ConnectionString, selectCommand);
            //обновление dataGridView1
            selectCommand = "select * from Buyer";
            refreshForm(ConnectionString, selectCommand);
            toolStripTextBox1.Text = "";
        }
        private void toolStripTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < '0' || l > '9') && l != '\b')
            {
                if (toolStripTextBox3.SelectionStart == 0)
                {
                    if (l == '.') e.Handled = true;
                }
                if (l != '.' || toolStripTextBox3.Text.IndexOf(".") != -1)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
