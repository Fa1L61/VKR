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
    public partial class FormNewContact : Form
    {

        public FormNewContact()
        {
            InitializeComponent();
        }



        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {

            CloseButton.BackColor = Color.Silver;
            CloseButton.ForeColor = Color.Red;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.BackColor = Color.WhiteSmoke;
            CloseButton.ForeColor = Color.Black;
        }


        Point lastPoint;
        private void splitContainer1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moving(sender, e);
            }
        }

        private void splitContainer1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPosition(sender, e);
        }

        private void splitContainer2_Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moving(sender, e);
            }
        }

        private void splitContainer2_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPosition(sender, e);
        }

        private void splitContainer1_Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moving(sender, e);
            }
        }

        private void moving(object sender, MouseEventArgs e)
        {
            this.Left += e.X - lastPoint.X;
            this.Top += e.Y - lastPoint.Y;
        }

        private void lastPosition(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void splitContainer1_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPosition(sender, e);
        }

        private void addClient_Click(object sender, EventArgs e) //добавление записи
        {
            try
            {

                SqlConnection con = new SqlConnection("Data Source=DESKTOP-01EIEFD;Initial Catalog=CezarDB;Integrated Security=True" );

                con.Open();

                SqlCommand command = new SqlCommand("Insert into Contacts(Surname, name, Otchestvo, Phone, Mail, Address) " +
                    "Values(@Surname, @name, @Otchestvo, @Phone, @Mail, @Address)", con);

                command.Parameters.AddWithValue("@Surname", textBoxName.Text);
                command.Parameters.AddWithValue("@name", textBoxCompany.Text);
                command.Parameters.AddWithValue("@Otchestvo", textBoxCustom.Text);
                command.Parameters.AddWithValue("@Phone", int.Parse(textBoxMail.Text));
                command.Parameters.AddWithValue("@Mail", textBoxAddress.Text);
                command.Parameters.AddWithValue("@Address", textBox1.Text);

                command.ExecuteNonQuery();
                con.Close();

                con.Open();

                SqlCommand sql = new SqlCommand("SELECT MAX(id_contact) from Contacts", con);
                int max = int.Parse(sql.ExecuteScalar().ToString());

                SqlCommand com = new SqlCommand("insert into Companys(id_contact, Kimpaniya) values(@max, @company)", con);
                com.Parameters.AddWithValue("@max", max);
                com.Parameters.AddWithValue("@company", textBoxPhone.Text); ;
                com.ExecuteNonQuery();

                SqlCommand command2 = new SqlCommand("Insert into Zakazi(Zapros, id_contact,summa_oplati, summa_doplati) " +
                    "values(@zapros,@max, 0, 0)", con);
                command2.Parameters.AddWithValue("@Zapros", textBox2.Text);
                command2.Parameters.AddWithValue("@max", max);
                
                command2.ExecuteNonQuery();

                con.Close();

                con.Open();

                SqlCommand sqlcom = new SqlCommand("SELECT MAX(id_zakaza) from Zakazi", con);
                int max_zakaz = int.Parse(sqlcom.ExecuteScalar().ToString());

                SqlCommand color = new SqlCommand("insert into Stats(id_zakaza) values(@max)", con);
                color.Parameters.AddWithValue("@max", max_zakaz);

                color.ExecuteNonQuery();
                con.Close();
            }

            catch
            {
                MessageBox.Show("Ошбика подключения к базе");
            }
        }

        private void FormNC_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonMenu_Click(object sender, EventArgs e) //назад
        {
            this.Hide();
            FormContacts fc = new FormContacts();
            fc.Show();
        }
    }
}
