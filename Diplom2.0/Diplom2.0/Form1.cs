using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom2._0
{
    public partial class Form1 : Form
    {
        public bool flag;

        public Form1()
        {
            InitializeComponent();
        }

        private void SuppliersButton_Click(object sender, EventArgs e)
        {   
            
            this.Hide();
            FormSuppliers fs = new FormSuppliers();
            fs.Show();

        }

        private void CustomerButton_Click(object sender, EventArgs e)
        {

            this.Hide();
            FormCustomers fc = new FormCustomers();
            fc.Show();
        
        }

        private void buttonContact_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormContacts fc = new FormContacts();
            fc.Show();
        }

        private void buttonEstimate_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormEstimate fe = new FormEstimate();
            fe.Show();
        }

        private void buttonAct_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormAkts fa = new FormAkts();
            fa.Show();
        }

        private void buttonPayment_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormOplata fo = new FormOplata();
            fo.Show();
        }

        private void buttonAccounts_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormScheta fc = new FormScheta();
            fc.Show();
        }

        private void buttonPurchases_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormZakupki fz = new FormZakupki();
            fz.Show();
        }

        private void buttonApplications_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormKompred fk = new FormKompred();
            fk.Show();
        }
    }
}
