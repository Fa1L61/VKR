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
    public partial class FormNewZakupki : Form
    {

        public FormNewZakupki()
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

                SqlCommand command = new SqlCommand("insert into Zakupki(id_zakaza, id_postavshika,Kategory_zakupri, " +
                    "Opisanie_producta, Kollichestvo_producta, edinica_izmereniya, stoimost_1_ed, Stoimost_zakupki, id_scheta) " +
                    "values(@iz,@ip,@kz,@op,@kp,@ei,@s1,@sz,@is)", con);

                command.Parameters.AddWithValue("@iz", int.Parse(textBox1.Text));
                command.Parameters.AddWithValue("@ip", int.Parse(textBox2.Text));
                command.Parameters.AddWithValue("@kz", textBox3.Text);
                command.Parameters.AddWithValue("@op", textBox4.Text);
                command.Parameters.AddWithValue("@kp", int.Parse(textBox5.Text));
                command.Parameters.AddWithValue("@ei", textBox6.Text);
                command.Parameters.AddWithValue("@s1", int.Parse(textBox7.Text));

                int cost = int.Parse(textBox7.Text) * int.Parse(textBox5.Text);

                command.Parameters.AddWithValue("@sz", cost);
                command.Parameters.AddWithValue("@is", int.Parse(textBox8.Text));
                command.ExecuteNonQuery();

                con.Close();


                MessageBox.Show("Закупка добавлена");

                
            }
            catch
            { 
                MessageBox.Show("Ошбика подключения к базе");
            }

            this.Hide();
            FormZakupki fz = new FormZakupki();
            fz.Show();
        }

        private void FormNC_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

     


        

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormSuppliers fs = new FormSuppliers();
            fs.Show();

        }

        private void buttonMenu_Click_1(object sender, EventArgs e) //назад
        {
            this.Hide();
            FormZakupki fz = new FormZakupki();
            fz.Show();
        }
    }
}
