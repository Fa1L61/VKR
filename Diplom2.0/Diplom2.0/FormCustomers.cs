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
    public partial class FormCustomers : Form
    {
        int _flag = 1;

        public FormCustomers()
        {
            InitializeComponent();
            initializeData();
        }

        private void initializeData()
        {
            try
            {
                string constr = "Data Source=DESKTOP-01EIEFD;Initial Catalog=CezarDB;Integrated Security=True";

                
                string Comstr = "select Zakazi.Id_zakaza as '№ заказа', Contacts.Surname as 'Фамилия', Contacts.[Name] as 'Имя',Contacts.Otchestvo as 'Отчество'," +
                    "Contacts.Phone as 'Телефон', Contacts.Mail as 'Почта', Contacts.[Address] as 'Адрес',Zakazi.Data_zakaza as " +
                    "'Дата создания заказа', Zakazi.Zapros as 'Запрос',akts.akt as 'Акт', Smeta.Smeta as 'Смета',Zakazi.Predlojenie " +
                    "as 'Предложение', Scheta.schet as 'Счет'  from Zakazi join Contacts on" +
                    " Zakazi.Id_contact = Contacts.Id_contact join Akts on Zakazi.Id_zakaza = Akts.Nomer_zakaza join Smeta on " +
                    "Zakazi.Id_zakaza = Smeta.Nomer_zakaza join Scheta on Zakazi.Id_zakaza = Scheta.Id_zakaza";

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

        private void FormCustomers_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "cezarDBDataSet.Contacts". При необходимости она может быть перемещена или удалена.
            this.contactsTableAdapter.Fill(this.cezarDBDataSet.Contacts);

            textBoxCusstomers.Text = "номер заказа";//подсказка
            textBoxCusstomers.ForeColor = Color.Gray;
        }

        private void textBoxCusstomers_Enter(object sender, EventArgs e)
        {
            textBoxCusstomers.Text = null;
            textBoxCusstomers.ForeColor = Color.Black;
        }
        private void button1_Click(object sender, EventArgs e) //статус заказа
        { 
            button2.Visible = true;

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-01EIEFD;Initial Catalog=CezarDB;Integrated Security=True");

            con.Open();

            if (_flag == 0)
            {
                _flag += 1;
                initializeData();
            }

            else if (_flag == 1)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    SqlCommand sql = new SqlCommand("SELECT status from Stats where id_zakaza = @id", con);
                    int id = Convert.ToInt32(row.Cells[0].Value);
                    sql.Parameters.AddWithValue("@id", id);

                    int status = Convert.ToInt32(sql.ExecuteScalar());

                    if (status == 1)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                    if (status == 2)
                    {
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    if (status == 3)

                        row.DefaultCellStyle.BackColor = Color.Green;
                }
                _flag -= 1;
            }
        }
            


            

        private void button2_Click(object sender, EventArgs e) //изменение статуса
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-01EIEFD;Initial Catalog=CezarDB;Integrated Security=True");// строка
            //подключения

            con.Open(); // открытие подключения

            SqlCommand sql = new SqlCommand("SELECT status from Stats where id_zakaza = @id", con); //SQL запрос

            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()); // получение ID заказа выбранной строки

            sql.Parameters.AddWithValue("@id", id); // указатель на id для Sql команды 

            int status = Convert.ToInt32(sql.ExecuteScalar()); //конвертация результата Sql запроса

            if (status == 1) // если статус красный
            {
                try
                {
                    SqlCommand sql2 = new SqlCommand("update Stats set status = 2 where id_zakaza = @id", con); // устанавливаем 
                    // статус желтый
                    sql2.Parameters.AddWithValue("@id", id);
                    sql2.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Error"); // если SQL запрос не выполнился
                }
            }

            if (status == 2)
            {
                try
                {
                    SqlCommand sql2 = new SqlCommand("update Stats set status =3 where id_zakaza = @id", con);
                    sql2.Parameters.AddWithValue("@id", id);

                    sql2.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Error");
                }                
            }

            if (status == 3)
            {
                try
                {
                    SqlCommand sql2 = new SqlCommand("update Stats set status =1 where id_zakaza = @id", con);
                    sql2.Parameters.AddWithValue("@id", id);

                    sql2.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }


            button1_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e) //поиск
        {
            if (textBoxCusstomers.Text != "")
            {
                for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    int n = int.Parse(textBoxCusstomers.Text);
                    if (n != int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()))
                    {
                        dataGridView1.Rows.RemoveAt(i);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e) //обновление
        {
            initializeData();
        }
    }
}
