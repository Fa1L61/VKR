using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Diplom2._0
{
    public partial class FormSuppliers : Form
    {

        public FormSuppliers()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {

            try
            {
                string constr = "Data Source=DESKTOP-01EIEFD;Initial Catalog=CezarDB;Integrated Security=True";


                string Comstr = "Select Company as 'Компания', fio as 'ФИО', phone as 'Телефон', mail as 'Почта'" +
                    ", [address] as 'Адрес', tovari as 'Товары' from Postavshiki";

                var c = new SqlConnection(constr);
                var dataAdapter = new SqlDataAdapter(Comstr, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];
            }

            catch
            {
                MessageBox.Show("Ошибка подключения к базе");
            }

        }

        private void buttonNewSupplier_Click(object sender, EventArgs e) //создать поставщика
        {
            this.Hide();
            FormNewSupplier fnc = new FormNewSupplier();
            fnc.Show();
        }

        private void buttonMenu_Click(object sender, EventArgs e) //меню
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e) //поиск
        {
            if (textBoxSuppliers.Text != "")
            {
                for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    string n = textBoxSuppliers.Text.ToLower();
                    string m = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    if (!m.Contains(n))
                    {
                        dataGridView1.Rows.RemoveAt(i);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //обновить
        {
            InitializeData();
        }
    }
}
