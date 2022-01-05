using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarpetShop
{
    public partial class Form6 : Form
    {
        string pass;

        public Form6(string pass)
        {
            InitializeComponent();
            this.pass = pass;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pass == textBox1.Text)
            {
                Password.result = true;
                Close();
            }
            else
            {
                textBox1.Text = null;
                MessageBox.Show("Пароль введен неверно!");
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
