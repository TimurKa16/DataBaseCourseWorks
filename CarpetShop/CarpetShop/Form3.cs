using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarpetShop
{
    public partial class Form3 : Form
    {
        bool f = true;
        Label[] lb;
        TextBox[][] tb;
        List<string> listlb;
        List<List<string>> l;
        SqlCommand command;
        SqlConnection sqlConnection;
        SqlDataReader sqlReader;
        TabPage tabPage;
        List<string> temp;

        public Form3(SqlConnection sqlConnection, SqlCommand command, SqlDataReader sqlReader)
        {
            InitializeComponent();
            this.sqlConnection = sqlConnection;
            this.sqlReader = sqlReader;
            this.command = command;
            
            tabPage1.AutoScroll = true;
            tabPage2.AutoScroll = true;
            tabPage3.AutoScroll = true;
            tabPage5.AutoScroll = true;
            tabPage6.AutoScroll = true;

        }

        private void Create_Labels()
        {
            lb = new Label[listlb.Count];
            int a = 15, b = 60;
            for (int i = 0; i < listlb.Count; i++)
            {
                lb[i] = new Label();
                lb[i].Name = "lb" + i.ToString();
                lb[i].Parent = tabPage1;
                lb[i].BackColor = Color.Violet;
                lb[i].Left = a;
                lb[i].Top = b;
                lb[i].Size = new Size(100, 50);
                lb[i].Text = listlb[i];
                lb[i].ForeColor = Color.Black;
                lb[i].Font = new Font(lb[i].Font, FontStyle.Bold);
                lb[i].Font = new Font(lb[i].Font.Name, 9, lb[i].Font.Style);
                lb[i].TextAlign = ContentAlignment.MiddleCenter;
                lb[i].BorderStyle = BorderStyle.FixedSingle;
                lb[i].BringToFront();
                a += 100;
            }
        }
        private void Create_tb()
        {

            tb = new TextBox[l.Count][];
            for (int i = 0; i < l.Count; i++)
                tb[i] = new TextBox[l[i].Count];
            int a = 15, b = 110, k = 0;
            for (int i = 0; i < l.Count; i++)
            {
                for (int j = 0; j < l[i].Count; j++)
                {
                    tb[i][j] = new TextBox();
                    tb[i][j].Parent = tabPage1;
                    tb[i][j].Left = a;
                    tb[i][j].Top = b;
                    tb[i][j].Multiline = true;
                    tb[i][j].Size = new Size(100, 50);
                    tb[i][j].ForeColor = Color.Black;
                    tb[i][j].TextAlign = HorizontalAlignment.Center;
                    tb[i][j].BorderStyle = BorderStyle.FixedSingle;
                    tb[i][j].BringToFront();
                    a += 100;
                    k++;
                }
                a = 15;
                b += 50;
            }
        }

        private void Deltb()
        {
            if (tb != null)
                for (int i = 0; i < tb.Length; i++)
                    for (int j = 0; j < tb[i].Length; j++)
                        tb[i][j].Dispose();
        }
        private void Dellb()
        {
            if (lb != null)
                for (int i = 0; i < lb.Length; i++)
                    lb[i].Dispose();
        }

        private void Tab1() // Вывод данных
        {
            sqlConnection.Open();
            listlb = new List<string> { "ID изделия", "Название", "Материал", "Длина", "Ширина",
                "Ворс", "Страна", "Цена закупки", "Цена продажи", "В наличии" };

            tabPage = tabPage1;
            Deltb();
            Create_Labels();
            l = new List<List<string>>();
            sqlReader = null;
            command = new SqlCommand("SELECT * FROM [Product]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    temp = new List<string>();
                    temp.Add(Convert.ToString(sqlReader["P_ID"]));
                    temp.Add(Convert.ToString(sqlReader["P_Name"]));
                    temp.Add(Convert.ToString(sqlReader["P_Material"]));
                    temp.Add(Convert.ToString(sqlReader["P_Length"]));
                    temp.Add(Convert.ToString(sqlReader["P_Width"]));
                    temp.Add(Convert.ToString(sqlReader["P_Pile"]));
                    temp.Add(Convert.ToString(sqlReader["P_Country"]));
                    temp.Add(Convert.ToString(sqlReader["P_Buy"]));
                    temp.Add(Convert.ToString(sqlReader["P_Sell"]));
                    temp.Add(Convert.ToString(sqlReader["P_Count"]));
                    l.Add(temp);
                }
                Create_tb();
                for (int i = 0; i < tb.Length; i++)
                    for (int j = 0; j < tb[i].Length; j++)
                        tb[i][j].Text = l[i][j];
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            sqlConnection.Close();
        }


        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void button4_Click(object sender, EventArgs e)      // Обновляем данные
        {
            Tab1();
        }


        

        

        

        private void Form3_Load(object sender, EventArgs e)
        {
            Tab1();
            but5();
        }





        //          Добавляем данные         //

        private int but2()
        {
            if (textBox1.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Название' должно быть заполнено!");
                return 1;
            }

            if (comboBox1.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Материал' должно быть заполнено!");
                return 1;
            }

            if (textBox2.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Длина' должно быть заполнено!");
                return 1;
            }

            if (textBox3.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Ширина' должно быть заполнено!");
                return 1;
            }

            if (comboBox2.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Ворс' должно быть заполнено!");
                return 1;
            }

            if (textBox12.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Страна' должно быть заполнено!");
                return 1;
            }

            if (textBox4.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Цена закупки' должно быть заполнено!");
                return 1;
            }

            if (textBox5.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Цена продажи' должно быть заполнено!");
                return 1;
            }


            try
            {
            sqlConnection.Open();

            command = new SqlCommand("INSERT INTO [Product] (P_Name, P_Material, P_Length, P_Width, P_Pile, P_Country, P_Buy, P_Sell, P_Count)VALUES(@P_Name, @P_Material, @P_Length, @P_Width, @P_Pile, @P_Country, @P_Buy, @P_Sell, @P_Count)", sqlConnection);

            command.Parameters.AddWithValue("P_Name", textBox1.Text);
            command.Parameters.AddWithValue("P_Material", comboBox1.Text);
            command.Parameters.AddWithValue("P_Length", textBox2.Text);
            command.Parameters.AddWithValue("P_Width", textBox3.Text);
            command.Parameters.AddWithValue("P_Pile", comboBox2.Text);
            command.Parameters.AddWithValue("P_Country", textBox12.Text);
            command.Parameters.AddWithValue("P_Buy", textBox4.Text);
            command.Parameters.AddWithValue("P_Sell", textBox5.Text);
            command.Parameters.AddWithValue("P_Count", 0);

            
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Данные введены неправльно!");
                f = false;
            }
            finally
            {
                if (f)
                {
                    textBox1.Text = String.Empty;
                    comboBox1.SelectedIndex = -1;
                    textBox2.Text = String.Empty;
                    textBox3.Text = String.Empty;
                    comboBox2.SelectedIndex = -1;
                    textBox12.Text = String.Empty;
                    textBox4.Text = String.Empty;
                    textBox5.Text = String.Empty;

                    MessageBox.Show("Данные успешно введены!");
                }
            }
            f = true;

            sqlConnection.Close();
            return 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            but2();
        }



        //          Удаляем данные


        private int but1()
        {
            if (textBox6.Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID изделия' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();


            try
            {
                command = new SqlCommand("DELETE FROM [Product] WHERE [P_ID]=@P_ID", sqlConnection);
                command.Parameters.AddWithValue("P_ID", Convert.ToInt32(textBox6.Text));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Данные введены неверно");
                f = false;
            }
            finally
            {
                if (f)
                {
                    textBox6.Text = String.Empty;
                    MessageBox.Show("Данные успешно удалены!");
                }
            }
            f = true;

            sqlConnection.Close();
            return 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            but1();
        }











        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }


        //      Изменяем     цены   //

        private int but6()
        {
            if (textBox13.Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID изделия' должно быть заполнено!");
                return 1;
            }

            if (textBox14.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Новая цена' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();


            try
            {
                command = new SqlCommand("UPDATE [Product] SET [P_Sell] = @P_Sell WHERE [P_ID]=@P_ID", sqlConnection);
                command.Parameters.AddWithValue("P_ID", Convert.ToInt32(textBox13.Text));
                command.Parameters.AddWithValue("P_Sell", Convert.ToInt32(textBox14.Text));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Данные введены неверно");
                f = false;
            }
            finally
            {
                if (f)
                {
                    textBox13.Text = String.Empty;
                    textBox14.Text = String.Empty;
                    MessageBox.Show("Данные успешно изменены!");
                }
            }
            f = true;

            sqlConnection.Close();
            return 0;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            but6();
        }


        private int but7()
        {
            if (textBox15.Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID изделия' должно быть заполнено!");
                return 1;
            }

            if (textBox16.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Новая цена' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();


            try
            {
                command = new SqlCommand("UPDATE [Product] SET [P_Buy] = @P_Buy WHERE [P_ID]=@P_ID", sqlConnection);
                command.Parameters.AddWithValue("P_ID", Convert.ToInt32(textBox15.Text));
                command.Parameters.AddWithValue("P_Buy", Convert.ToInt32(textBox16.Text));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Данные введены неверно");
                f = false;
            }
            finally
            {
                if (f)
                {
                    textBox15.Text = String.Empty;
                    textBox16.Text = String.Empty;
                    MessageBox.Show("Данные успешно изменены!");
                }
            }
            f = true;

            sqlConnection.Close();
            return 0;

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            but7();

        }












        //      закупаем товар   //



        private int but3()
        {
            double Count = 0, Outgo = 0, Income = 0, Profit = 0, Cost = 0;
            int ID = 0, Number = 0;

            if (textBox7.Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID изделия' должно быть заполнено!");
                return 1;
            }

            if (textBox8.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Количество' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();



            try
            {
                ID = Convert.ToInt32(textBox7.Text);
                Number = Convert.ToInt32(textBox8.Text);

                sqlReader = null;
                command = new SqlCommand("SELECT [P_Count] FROM [Product] WHERE [P_ID] = @P_ID", sqlConnection);
                command.Parameters.AddWithValue("P_ID", ID);
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    Count = Convert.ToDouble(sqlReader["P_Count"]);
                }
                if (sqlReader != null)
                    sqlReader.Close();



                sqlReader = null;
                command = new SqlCommand("SELECT * FROM [Gain]", sqlConnection);
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    Income = Convert.ToDouble(sqlReader["G_Income"]);
                    Outgo = Convert.ToDouble(sqlReader["G_Outgo"]);
                    Profit = Convert.ToDouble(sqlReader["G_Profit"]);
                }
                if (sqlReader != null)
                    sqlReader.Close();



                sqlReader = null;
                command = new SqlCommand("SELECT [P_Buy] FROM [Product] WHERE [P_ID] = @P_ID", sqlConnection);
                command.Parameters.AddWithValue("P_ID", ID);
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    Cost = Convert.ToDouble(sqlReader["P_Buy"]);
                }
                if (sqlReader != null)
                    sqlReader.Close();
                Cost *= Number;








                command = new SqlCommand("UPDATE [Product] SET [P_Count] = @P_Count WHERE [P_ID]=@P_ID", sqlConnection);
                command.Parameters.AddWithValue("P_ID", ID);
                command.Parameters.AddWithValue("P_Count", (Number + Count));
                command.ExecuteNonQuery();

                command = new SqlCommand("UPDATE [Gain] SET [G_Income] = @G_Income, [G_Outgo] = @G_Outgo, [G_Profit] = @G_Profit WHERE [G_ID]= 1", sqlConnection);
                command.Parameters.AddWithValue("G_Income", Income);
                command.Parameters.AddWithValue("G_Outgo", Outgo + Cost);
                command.Parameters.AddWithValue("G_Profit", Profit - Cost);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Данные введены неверно");
                f = false;
            }
            finally
            {
                if (f)
                {
                    textBox7.Text = String.Empty;
                    textBox8.Text = String.Empty;
                    MessageBox.Show("Товар закуплен!");
                }
            }
            f = true;

            sqlConnection.Close();
            return 0;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            but3();
        }



        //      Смотрим прибыль   //

        private int but5()
        {
            sqlConnection.Open();
            sqlReader = null;
            command = new SqlCommand("SELECT [G_Income], [G_Outgo], [G_Profit] FROM [Gain] WHERE [G_ID] = 1", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    textBox9.Text = Convert.ToString(sqlReader["G_Income"]);
                    textBox10.Text = Convert.ToString(sqlReader["G_Outgo"]);
                    textBox11.Text = Convert.ToString(sqlReader["G_Profit"]);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
                return 1;
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            sqlConnection.Close();
            return 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            but5();
        }


    }
}
