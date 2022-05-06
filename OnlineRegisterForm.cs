using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Alpha_WindanginnAssist
{
    public partial class OnlineRegisterForm : Form
    {
        public OnlineRegisterForm()
        {
            InitializeComponent();
        }

        private void OnlineRegisterForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text)|| string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("all the textboxes are needed to be filled");
                return;
            }
            int numbertest;
            if (!int.TryParse(textBox3.Text, out numbertest)|| textBox3.Text.Length!=9)
            {
                MessageBox.Show("Phone Number Failed");

                return;
            }


            if (!int.TryParse(textBox5.Text, out numbertest) || textBox5.Text.Length<14)
            {
                MessageBox.Show("Credit Card failed");

                return;
            }


            if (!int.TryParse(textBox6.Text, out numbertest) || textBox6.Text.Length == 2)
            {
                MessageBox.Show("Expire date failed");

                return;
            }

            if (!int.TryParse(textBox7.Text, out numbertest) || textBox7.Text.Length == 2)
            {
                MessageBox.Show("Expire date failed");

                return;
            }

            if (!int.TryParse(textBox8.Text, out numbertest) || textBox8.Text.Length == 3)
            {
                MessageBox.Show("CVV failed");

                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = "confirmed";
        }

    }
}
