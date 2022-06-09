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
    public partial class FormOplata : Form
    {
        public FormOplata()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {

            try
            {
                string constr = "Data Source=DESKTOP-01EIEFD;Initial Catalog=CezarDB;Integrated Security=True";


                string Comstr = "Select Zakazi.Id_zakaza as '№ заказа', Scheta.ID_schet as '№ счета', Zakazi.Product_name as 'Имя продукта'," +
                " Zakazi.Cost_for_1 as 'Цена за шт',Zakazi.Kollichestvo as 'Количество',zakazi.Itog as 'Итог', Zakazi.Data_oplati as 'Дата оплаты'," +
                "Zakazi.Summa_oplati as 'Сумма оплаты' ,Zakazi.Data_doplati as 'Дата доплаты', Zakazi.Summa_doplati as 'Сумма доплаты', zakazi.Saldo as 'Сальдо'" +
                "from Zakazi join Scheta on Scheta.Id_zakaza = zakazi.Id_zakaza";



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
        
        private void buttonMenu_Click(object sender, EventArgs e) //назад
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();

        }

        private void buttonNewEstimate_Click(object sender, EventArgs e) //внести оплату
        {
            int num = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());

            int firstSum = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[7].Value.ToString()); //сумма оплаты
            int secondSum = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value.ToString()); //сумма доплаты

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-01EIEFD;Initial Catalog=CezarDB;Integrated Security=True");

            try
            {
                if (firstSum == 0)
                {
                    con.Open();

                    SqlCommand command = new SqlCommand("update zakazi set Summa_oplati = @first, data_oplati = getdate() " +
                        "where id_zakaza = @num", con);

                    command.Parameters.AddWithValue("@num", num);
                    command.Parameters.AddWithValue("@first", int.Parse(textBox1.Text));

                    command.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("Оплата внесена");
                }

                else if (secondSum == 0)
                {
                    con.Open();

                    SqlCommand command = new SqlCommand("update zakazi set Summa_doplati = @second, data_doplati = getdate()" +
                        " where id_zakaza = @num", con);

                    command.Parameters.AddWithValue("@num", num);
                    command.Parameters.AddWithValue("@second", int.Parse(textBox1.Text));

                    command.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("Оплата внесена");
                }
            }
            catch
            {
                MessageBox.Show("Ошбика подключения к базе");
            }
            
            string constr = "Data Source=DESKTOP-01EIEFD;Initial Catalog=CezarDB;Integrated Security=True";


            string Comstr = "Select Zakazi.Id_zakaza as '№ заказа', Scheta.ID_schet as '№ счета', Zakazi.Product_name as 'Имя продукта'," +
                " Zakazi.Cost_for_1 as 'Цена за шт',Zakazi.Kollichestvo as 'Количество',zakazi.Itog as 'Итог', Zakazi.Data_oplati as 'Дата оплаты'," +
                "Zakazi.Summa_oplati as 'Сумма оплаты' ,Zakazi.Data_doplati as 'Дата доплаты', Zakazi.Summa_doplati as 'Сумма доплаты', zakazi.Saldo as 'Сальдо'" +
                "from Zakazi join Scheta on Scheta.Id_zakaza = zakazi.Id_zakaza";

            var c = new SqlConnection(constr);
            var dataAdapter = new SqlDataAdapter(Comstr, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

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

        private void button3_Click(object sender, EventArgs e) //назад
        {
            InitializeData();
        }
    }
}
