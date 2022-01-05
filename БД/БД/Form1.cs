using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;


namespace БД
{
    public partial class Form1 : Form
    {
        Form2 form2;
        Form3 form3;
        Form4 form4;
        Form5 form5;
        Form6 form6;
        SqlConnection sqlConnection;
        SqlDataReader sqlReader;
        SqlCommand command;
        bool result = false;

        Process proc;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.FormClosing += new FormClosingEventHandler(Form4_FormClosing);
            //proc = Process.Start("Database1.mdf");
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\timur\source\repos\ProjectsToUpload\бд\БД\БД\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                sqlConnection.Close();
                form2 = new Form2(sqlConnection, command, sqlReader);
                form3 = new Form3(sqlConnection, command, sqlReader);
                form4 = new Form4(sqlConnection, command, sqlReader);
                form5 = new Form5(sqlConnection, command, sqlReader);
            }
            catch (Exception)
            {
                MessageBox.Show("Извините, база данных не загрузилась." + Environment.NewLine +
                    "Попробуйте повторить попытку!");
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)  // Бухгалтер
        {

            form6 = new Form6("1");
            form6.ShowDialog();
            if (Password.result)
                form2.ShowDialog();
            Password.result = false;
        }

        private void button2_Click(object sender, EventArgs e)  // Водитель
        {
            form6 = new Form6("2");
            form6.ShowDialog();
            if (Password.result)
                form3.ShowDialog();
            Password.result = false;
        }

        private void button3_Click(object sender, EventArgs e)  // Администратор
        {
            form6 = new Form6("4");
            form6.ShowDialog();
            if (Password.result)
                form4.ShowDialog();
            Password.result = false;
        }

        private void button4_Click(object sender, EventArgs e)   // Логист
        {
            form6 = new Form6("3");
            form6.ShowDialog();
            if (Password.result)
                form5.ShowDialog();
            Password.result = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (!proc.HasExited)
        //        proc.Close();
        //}
    }
}
