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
    public partial class FormContacts : Form
    {

        public FormContacts()
        {
            InitializeComponent();
        }

        private void FormContacts_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "cezarDBDataSet.Contacts". При необходимости она может быть перемещена или удалена.
            this.contactsTableAdapter.Fill(this.cezarDBDataSet.Contacts);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "cezarDBDataSet.Contacts". При необходимости она может быть перемещена или удалена.

            textBox1.Text = "номер контакта";//подсказка
            textBox1.ForeColor = Color.Gray;
        }

        
        private void buttonMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();

        }

        private void buttonNewOrder_Click(object sender, EventArgs e) //создание заказа
        {
            int num = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
         
            this.Hide();
            FormNewOrder fno = new FormNewOrder(num);
            
            fno.Show();
        }

        private void button1_Click(object sender, EventArgs e) //создание контакта
        {
            this.Hide();
            FormNewContact fnc = new FormNewContact();
            fnc.Show();
        }

        private void textBox1_Enter_1(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.ForeColor = Color.Black;
        }

        private void button2_Click(object sender, EventArgs e) //кнопка поиска
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

        private void button3_Click(object sender, EventArgs e) //кнопка обновления
        {
            FormContacts_Load(sender, e);
        }
    }
}
