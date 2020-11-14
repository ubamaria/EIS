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
    public partial class FormMaterial : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, Program.sPath);

        public FormMaterial()
        {
            InitializeComponent();
        }

        private void FormMaterial_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
";New=False;Version=3";
            String selectCommand = "Select * from Material";
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
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "")
            {
                MessageBox.Show("Введите название материала");
                return;
            }
            if (toolStripTextBox2.Text.IndexOf('.') > 0)
            {
                if (toolStripTextBox2.Text.Substring(toolStripTextBox2.Text.IndexOf('.')).Length > 3)
                {
                    MessageBox.Show("Стоимость должна быть не более 10 символов и иметь не более 2-ух знаков после запятой");
                    return;
                }
            }
            if (toolStripTextBox3.Text.IndexOf('.') > 0)
            {
                if (toolStripTextBox3.Text.Substring(toolStripTextBox3.Text.IndexOf('.')).Length > 3)
                {
                    MessageBox.Show("Процент должен быть не более 3 символов и иметь не более 2-ух знаков после запятой");
                    return;
                }
            }
            string percent = toolStripTextBox3.Text.Replace(".", ",");
            int percent1 = Convert.ToInt32(Convert.ToDouble(percent));
            if (percent1 >= 100)
            {
                MessageBox.Show("Процент должен быть меньше 100");
                return;
            }
            toolStripTextBox2.Text = toolStripTextBox2.Text.Replace(",", ".");
            //Для правильной записи нового кода необходимо запросить
            //максимальное значение idMaterial из таблицы Material
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            String selectCommand = "select MAX(IdMaterial) from Material";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            //вставка в таблицу
            string txtSQLQuery = "insert into Material (IdMaterial, Name, CostMaterial, NDS) values (" +
            (Convert.ToInt32(maxValue) + 1) + ", '" + toolStripTextBox1.Text + "', '" + toolStripTextBox2.Text + "', '" + toolStripTextBox3.Text + "')";
            ExecuteQuery(txtSQLQuery);
            //обновление dataGridView1
            selectCommand = "select * from Material";
            refreshForm(ConnectionString, selectCommand);
            //toolStripTextBox1.Text = "";
            //toolStripTextBox2.Text = "";
            //toolStripTextBox3.Text = "";

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

        private void toolStripButtonChange_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "")
            {
                MessageBox.Show("Введите название материала");
                return;
            }
            if (toolStripTextBox2.Text.IndexOf('.') > 0)
            {
                if (toolStripTextBox2.Text.Substring(toolStripTextBox2.Text.IndexOf('.')).Length > 3)
                {
                    MessageBox.Show("Стоимость должна быть не более 10 символов и иметь не более 2-ух знаков после запятой");
                    return;
                }
            }
            if (toolStripTextBox3.Text.IndexOf('.') > 0)
            {
                if (toolStripTextBox3.Text.Substring(toolStripTextBox3.Text.IndexOf('.')).Length > 3)
                {
                    MessageBox.Show("Процент должен быть не более 3 символов и иметь не более 2-ух знаков после запятой");
                    return;
                }
            }
            string percent = toolStripTextBox3.Text.Replace(".", ",");
            int percent1 = Convert.ToInt32(Convert.ToDouble(percent));
            if (percent1 >= 100)
            {
                MessageBox.Show("Процент должен быть меньше 100");
                return;
            }
            toolStripTextBox2.Text = toolStripTextBox2.Text.Replace(",", ".");
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение Name выбранной строки
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            string changeName = toolStripTextBox1.Text;
            //обновление Name
            String selectCommand = "update Material set Name='" + changeName + "'where IdMaterial = " + valueId;
 string ConnectionString = @"Data Source=" + sPath +
";New=False;Version=3";
            changeValue(ConnectionString, selectCommand);
            string changeCost = toolStripTextBox2.Text;
            String selectCost = "update Material set CostMaterial='" + changeCost + "'where IdMaterial = " + valueId;
            changeValue(ConnectionString, selectCost);
            string changeNDS = toolStripTextBox3.Text;
            String selectNDS = "update Material set NDS='" + changeNDS + "'where IdMaterial = " + valueId;
            changeValue(ConnectionString, selectNDS);
            //обновление dataGridView1
            selectCommand = "select * from Material";
            refreshForm(ConnectionString, selectCommand);
            //toolStripTextBox1.Text = "";
            //toolStripTextBox2.Text = "";
            //toolStripTextBox3.Text = "";
        }
        private void dataGridView1_CellMouseClick(object sender,
DataGridViewCellMouseEventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение Name выбранной строки
            string nameId = dataGridView1[1, CurrentRow].Value.ToString();
            toolStripTextBox1.Text = nameId;
            string costId = dataGridView1[2, CurrentRow].Value.ToString();
            toolStripTextBox2.Text = costId;
            string ndsId = dataGridView1[3, CurrentRow].Value.ToString();
            toolStripTextBox3.Text = ndsId;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            {
                //выбрана строка CurrentRow
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                //получить значение idMOL выбранной строки
                string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                String selectCommand = "delete from Material where IdMaterial=" + valueId;
                string ConnectionString = @"Data Source=" + sPath +
               ";New=False;Version=3";
                changeValue(ConnectionString, selectCommand);
                //обновление dataGridView1
                selectCommand = "select * from Material";
                refreshForm(ConnectionString, selectCommand);
                toolStripTextBox1.Text = "";
            }
        }
        private void toolStripTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < '0' || l > '9') && l != '\b')
            {
                if (toolStripTextBox2.SelectionStart == 0)
                {
                    if (l == '.') e.Handled = true;
                }
                if (l != '.' || toolStripTextBox2.Text.IndexOf(".") != -1)
                {
                    e.Handled = true;
                }
            }
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
