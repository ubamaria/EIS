using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EIS
{
    public partial class FormWholeSale : Form
    {
        public FormWholeSale()
        {
            InitializeComponent();
        }

        private void планСчетовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChartOfAccounts newForm = new FormChartOfAccounts();
            newForm.Show();
        }

        private void материалыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMaterial newForm = new FormMaterial();
            newForm.Show();
        }

        private void покупательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBuyer newForm = new FormBuyer();
            newForm.Show();
        }

        private void поставщикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProvider newForm = new FormProvider();
            newForm.Show();
        }

        private void заявкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRequest newForm = new FormRequest();
            newForm.Show();
        }

        private void журналОперацийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormJournalOperation newForm = new FormJournalOperation();
            newForm.Show();
        }
    }
}
