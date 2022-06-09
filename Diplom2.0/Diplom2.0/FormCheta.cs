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
    public partial class FormScheta : Form
    {
        public FormScheta()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {

            try
            {
                string constr = "Data Source=DESKTOP-01EIEFD;Initial Catalog=CezarDB;Integrated Security=True";


                string Comstr = "Select scheta.Data as 'Дата создания счета',Scheta.id_schet as 'Номер счета', Scheta.id_zakaza as 'Номер заказа'," +
                    " Contacts.Name as 'Имя заказчика', Contacts.Surname as 'Фамилия заказчика', Contacts.Otchestvo " +
                    "as 'Отчество заказчика', Scheta.schet as 'Счет' from Zakazi join Scheta on Scheta.id_zakaza = Zakazi.Id_zakaza " +
                    "join Contacts on Contacts.Id_contact = zakazi.Id_contact";



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

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();

        }

        private void buttonNewEstimate_Click(object sender, EventArgs e)
        {

            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormContacts_Load(object sender, EventArgs e)
        {
            textBox1.Text = "номер счета";//подсказка
            textBox1.ForeColor = Color.Gray;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e) //кнопка поиска
        {
            if (textBox1.Text != "")
            {
                for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    int n = int.Parse(textBox1.Text);
                    if (n != int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()))
                    {
                        dataGridView1.Rows.RemoveAt(i);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //кнопка обновления
        {
            InitializeData();
        }

        private void button3_Click(object sender, EventArgs e) //кнопка открыть
        {
            string filename = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            var application = new Microsoft.Office.Interop.Word.Application();
            var doc = new Microsoft.Office.Interop.Word.Document();
            doc = application.Documents.Add(Template: filename);
            application.Visible = true;
            try
            {
                doc.SaveAs(FileName: filename);
            }
            catch { }
        }
    }
}
