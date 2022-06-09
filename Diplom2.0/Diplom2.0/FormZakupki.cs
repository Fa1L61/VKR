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
    public partial class FormZakupki : Form
    {
        public FormZakupki()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {

            try
            {
                string constr = "Data Source=DESKTOP-01EIEFD;Initial Catalog=CezarDB;Integrated Security=True";


                string Comstr = "Select Zakupki.id_zakaza as '№ заказа', zakupki.Data_zakupki As 'Дата закупки', Contacts.Name As 'Имя'," +
                    " Contacts.Surname As 'Фамилия', Zakupki.Kategory_zakupri As 'Категория закупки', Zakupki.Opisanie_producta As 'Описание продукта', " +
                    "Zakupki.edinica_izmereniya As 'Е-ца измерения', Zakupki.Kollichestvo_producta As 'Кол-во', Zakupki.stoimost_1_ed As 'Стоимость 1', " +
                    "Zakupki.Stoimost_zakupki As 'Стоимость закупки',Scheta.ID_schet As 'Номер счета' from Zakazi join Zakupki on " +
                    "Zakupki.Id_zakaza = Zakazi.Id_zakaza join Contacts on Contacts.Id_contact = Zakazi.Id_contact " +
                    "join Scheta on Scheta.Id_zakaza = Zakazi.Id_zakaza";



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



        private void FormContacts_Load(object sender, EventArgs e)
        {
            textBox1.Text = "номер заказа";//подсказка
            textBox1.ForeColor = Color.Gray;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.ForeColor = Color.Black;
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

        private void button1_Click(object sender, EventArgs e) //поиск
        {
            if (textBox1.Text != "")
            {
                for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    int n = int.Parse(textBox1.Text);
                    if (n != int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()))
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

        private void button3_Click(object sender, EventArgs e) //создать закупку
        {
            this.Hide();
            FormNewZakupki fnz = new FormNewZakupki();
            fnz.Show();

        }
    }
}
