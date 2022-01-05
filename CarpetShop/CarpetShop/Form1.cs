using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarpetShop
{
    public partial class Form1 : Form
    {
        Form2 form2;
        Form3 form3;
        Form6 form6;
        SqlConnection sqlConnection;
        SqlDataReader sqlReader;
        SqlCommand command;
        bool result = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Тимур\Documents\Visual Studio 2015\Projects\3 курс\1 семестр\CarpetShop\CarpetShop\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                sqlConnection.Close();
                form2 = new Form2(sqlConnection, command, sqlReader);
                form3 = new Form3(sqlConnection, command, sqlReader);
            }
            catch (Exception)
            {
                MessageBox.Show("Извините, база данных не загрузилась." + Environment.NewLine +
                    "Попробуйте повторить попытку!");
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)      // Администратор
        {
            form6 = new Form6("admin");
            form6.ShowDialog();
            if (Password.result)
            {
                Hide();
                form3.ShowDialog();
                Show();
            }
            Password.result = false;
        }

        private void button4_Click(object sender, EventArgs e)      // Покупатель
        {
            form6 = new Form6("customer");
            form6.ShowDialog();
            if (Password.result)
            {
                Hide();
                form2.ShowDialog();
                Show();
            }
            Password.result = false;
        }
    }
}
