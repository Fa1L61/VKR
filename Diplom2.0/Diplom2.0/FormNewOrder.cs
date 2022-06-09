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
using System.IO;

namespace Diplom2._0
{
    public partial class FormNewOrder : Form
    {
        string[] docks = { "", "", "", "" };
        public FormNewOrder(int num)
        {
            InitializeComponent();

            textBoxName.Text = num.ToString();
            //textBoxName.Visible = false;
        }

        private string[] addFile(string fileName, int flag)
        {
            if (flag <= 4)
                docks[flag] = fileName;
            else
            {
                string[] docks = {"", "", "", "" };
            }

            return docks;
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


        private void FormNC_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonAddManager_Click(object sender, EventArgs e)
        {



        }

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormContacts fc = new FormContacts();
            fc.Show();

        }

        private void addClient_Click(object sender, EventArgs e)
        { //загрузка акта
            try
            {

                SqlConnection con = new SqlConnection("Data Source=DESKTOP-01EIEFD;Initial Catalog=CezarDB;Integrated Security=True");

                con.Open();

                //полуение id_zakaza для присвоения к акту
                SqlCommand command = new SqlCommand("Select id_zakaza from Zakazi where Zakazi.id_contact = @num", con);
                command.Parameters.AddWithValue("@num", int.Parse(textBoxName.Text));
                int idZakaza = int.Parse(command.ExecuteScalar().ToString());

                SqlCommand command1 = new SqlCommand("insert into Akts(nomer_zakaza, akt) values(@idZakaza, @dock)", con);
                command1.Parameters.AddWithValue("@idZakaza", idZakaza);
                string akt = docks[3];

                command1.Parameters.AddWithValue("@dock", akt);

                command.ExecuteNonQuery();
                command1.ExecuteNonQuery();

                //полуение id_zakaza для присвоения к счету
                //SqlCommand command2 = new SqlCommand("Select id_zakaza from Zakazi where Zakazi.id_contact = @num", con);

                SqlCommand command3 = new SqlCommand("insert into Scheta(id_zakaza, schet) values(@idZakaza, @dock)", con);
                command3.Parameters.AddWithValue("@idZakaza", idZakaza);
                string schet = docks[2];
                 
                command3.Parameters.AddWithValue("@dock", schet);

                //command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();

                con.Close();

                string kompred = docks[1];

                con.Open();
                //полуение id_zakaza для присвоения к компред
                SqlCommand command4 = new SqlCommand("update Zakazi set Predlojenie = @dock, Product_name = @prod," +
                    " cost_for_1 = @cost, Kollichestvo = @all, itog = @res where id_zakaza= @idZakaza", con);

                command4.Parameters.AddWithValue("@dock", kompred);
                command4.Parameters.AddWithValue("@idZakaza", idZakaza);
                command4.Parameters.AddWithValue("@prod", textBox1.Text);
                command4.Parameters.AddWithValue("@cost", int.Parse(textBox2.Text));
                command4.Parameters.AddWithValue("@all", int.Parse(textBox3.Text));

                int res = int.Parse(textBox2.Text) * int.Parse(textBox3.Text);

                command4.Parameters.AddWithValue("@res", res);

                command4.ExecuteNonQuery();

                //полуение id_zakaza для присвоения к смете
                //SqlCommand command5 = new SqlCommand("Select id_zakaza from Zakazi where Zakazi.id_contact = @num", con);

                SqlCommand command6 = new SqlCommand("insert into Smeta(nomer_zakaza, smeta) values(@idZakaza, @dock)", con);
                command6.Parameters.AddWithValue("@idZakaza", idZakaza);
                string smeta = docks[0];

                command6.Parameters.AddWithValue("@dock", smeta);

                //command5.ExecuteNonQuery();
                command6.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Запись добавлена");

                this.Hide();
                FormContacts fc = new FormContacts();
                fc.Show();

            }

            catch
            {
                MessageBox.Show("Ошбика подключения к базе");
            }
        }

        private void button1_Click(object sender, EventArgs e) //файл1
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "D:\\Cezar\\смета";

                if (ofd.ShowDialog() != DialogResult.OK) return;
                // всё. имя файла теперь хранится в openFileDialog1.FileName
                {
                    // имя акта ofd.FileNames ;
                    string aktName = ofd.FileName;
                    int flag = 0;
                    addFile(ofd.FileName, flag);
                    
                }
            }


        }

        private void button2_Click(object sender, EventArgs e) //файл2
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "D:\\Cezar\\компред";

                if (ofd.ShowDialog() != DialogResult.OK) return;
                // всё. имя файла теперь хранится в openFileDialog1.FileName
                {
                    // имя акта ofd.FileNames ;
                    string aktName = ofd.FileName;
                    int flag = 1;
                    addFile(ofd.FileName, flag);

                }
            }
        }

        private void button3_Click(object sender, EventArgs e) //файл3
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "D:\\Cezar\\счет";

                if (ofd.ShowDialog() != DialogResult.OK) return;
                // всё. имя файла теперь хранится в openFileDialog1.FileName
                {
                    // имя акта ofd.FileNames ;
                    string aktName = ofd.FileName;
                    int flag = 2;
                    addFile(ofd.FileName, flag);

                }
            }
        }

        private void button4_Click(object sender, EventArgs e) //файл4
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "D:\\Cezar\\акты"; // в диалоговом окне 
                //выведется директория по заданному пути

                if (ofd.ShowDialog() != DialogResult.OK) return;
                // всё. имя файла теперь хранится в openFileDialog1.FileName
                {
                    // имя акта ofd.FileNames ;
                    string aktName = ofd.FileName;
                    int flag = 3;
                    addFile(ofd.FileName, flag);
                }
            }
        }

        private void buttonMenu_Click_1(object sender, EventArgs e) //назад
        {
            this.Hide();
            FormContacts fc = new FormContacts();
            fc.Show();
        }
    }
}

