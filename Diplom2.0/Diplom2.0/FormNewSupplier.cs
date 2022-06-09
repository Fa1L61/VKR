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
    public partial class FormNewSupplier : Form
    {
        public bool flag;
        public FormNewSupplier()
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

        private void addClient_Click(object sender, EventArgs e) //добавить запись
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-01EIEFD;Initial Catalog=CezarDB;Integrated Security=True");

                con.Open();

                SqlCommand command = new SqlCommand("Insert into Postavshiki(company, Phone, Mail, [Address], Tovari, FIO) " +
                    "values(@comp, @phone, @mail, @address, @tovari, @fio)",con);

                command.Parameters.AddWithValue("@comp", textBoxCompany.Text);
                command.Parameters.AddWithValue("@phone", textBoxPhone.Text);
                command.Parameters.AddWithValue("@mail", textBoxMail.Text);
                command.Parameters.AddWithValue("@address", textBoxAdress.Text);
                command.Parameters.AddWithValue("@tovari", textBoxKatalog.Text.ToLower());
                command.Parameters.AddWithValue("@fio", textBoxName.Text);

                command.ExecuteNonQuery();

                con.Close();


                MessageBox.Show("Поставщик добавлен");
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

        private void buttonAddManager_Click(object sender, EventArgs e) //новый менеджер
        {
            labelCompany.Visible = false;
            label1.Visible = false;
            label2.Visible = false;

            textBoxCompany.Visible = false;
            textBoxKatalog.Visible = false;

            textBoxName.Text = "";
            textBoxPhone.Text = "";
            textBoxMail.Text = "";
            textBoxAdress.Text = "";

            addClient_Click(sender, e);
        }

        private void buttonMenu_Click(object sender, EventArgs e) //форма поставщики
        {
            this.Hide();
            FormSuppliers fs = new FormSuppliers();
            fs.Show();

        }

        private void button1_Click(object sender, EventArgs e) //назад
        {
            this.Hide();
            FormSuppliers fs = new FormSuppliers();
            fs.Show();
        }
    }
}
