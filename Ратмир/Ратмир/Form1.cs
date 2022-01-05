using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ратмир
{
    public partial class Form1 : Form
    {

        bool f = true;
        Label[] lb;
        TextBox[][][] tb;
        List<string> listlb;
        List<string> listtb;
        List<List<string>> l;
        SqlCommand command;
        SqlConnection sqlConnection;
        SqlDataReader sqlReader;
        TabPage tabPage;
        List<string> temp;
        
        public Form1()
        {
            InitializeComponent();
            tabPage1.AutoScroll = true;
            tabPage2.AutoScroll = true;
            tabPage3.AutoScroll = true;
            tabPage4.AutoScroll = true;
            tabPage5.AutoScroll = true;
            tabPage6.AutoScroll = true;
            tabPage10.AutoScroll = true;
            tabPage12.AutoScroll = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Тимур\Documents\Visual Studio 2015\Projects\3 курс\1 семестр\Ратмир\Ратмир\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                sqlConnection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Извините, база данных не загрузилась." + Environment.NewLine +
                    "Попробуйте повторить попытку!");
                Application.Exit();
            }

            tb = new TextBox[6][][];
            Tab1();
            Tab2();
            Tab3();
            Tab4();
            Tab5();
            Tab6();




        }

        private void Create_Labels()
        {
            lb = new Label[listlb.Count];
            int a = 15, b = 60;
            for (int i = 0; i < listlb.Count; i++)
            {
                lb[i] = new Label();
                lb[i].Name = "lb" + i.ToString();
                lb[i].Parent = tabPage;
                lb[i].BackColor = Color.Orange;
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

        private void Create_tb(int I)
        {

            tb[I] = new TextBox[l.Count][];
            for (int i = 0; i < l.Count; i++)
                tb[I][i] = new TextBox[l[i].Count];
            int a = 15, b = 110, k = 0;
            for (int i = 0; i < l.Count; i++)
            {
                for (int j = 0; j < l[i].Count; j++)
                {
                    tb[I][i][j] = new TextBox();
                    tb[I][i][j].Parent = tabPage;
                    tb[I][i][j].Left = a;
                    tb[I][i][j].Top = b;
                    tb[I][i][j].Multiline = true;
                    tb[I][i][j].Size = new Size(100, 50);
                    tb[I][i][j].ForeColor = Color.Black;
                    tb[I][i][j].TextAlign = HorizontalAlignment.Center;
                    tb[I][i][j].BorderStyle = BorderStyle.FixedSingle;
                    tb[I][i][j].BringToFront();
                    a += 100;
                    k++;
                }
                a = 15;
                b += 50;
            }
        }

        private void Deltb(TextBox[][] tb)
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

        private void Tab1() // Заказчики
        {
            int I = 0;
            sqlConnection.Open();
            listlb = new List<string> { "ID заказчика", "Компания",
                "Фамилия", "Имя", "Отчество", "Телефон", "Почта", "ИНН", "СНИЛС", "КПП", "ОГРН", "БИК" };
            listtb = new List<string> { "C_ID", "C_Company",
                "C_Family", "C_Name", "C_Father", "C_Phone", "C_Email", "C_INN", "C_SNILS", "C_KPP", "C_OGRN", "C_BIK" };
            tabPage = tabPage1;
            Deltb(tb[I]);
            Create_Labels();
            l = new List<List<string>>();
            sqlReader = null;
            command = new SqlCommand("SELECT * FROM [Customer]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    temp = new List<string>();
                    temp.Add(Convert.ToString(sqlReader["C_ID"]));
                    temp.Add(Convert.ToString(sqlReader["C_Company"]));
                    temp.Add(Convert.ToString(sqlReader["C_Family"]));
                    temp.Add(Convert.ToString(sqlReader["C_Name"]));
                    temp.Add(Convert.ToString(sqlReader["C_Father"]));
                    temp.Add(Convert.ToString(sqlReader["C_Phone"]));
                    temp.Add(Convert.ToString(sqlReader["C_Email"]));
                    temp.Add(Convert.ToString(sqlReader["C_INN"]));
                    temp.Add(Convert.ToString(sqlReader["C_SNILS"]));
                    temp.Add(Convert.ToString(sqlReader["C_KPP"]));
                    temp.Add(Convert.ToString(sqlReader["C_OGRN"]));
                    temp.Add(Convert.ToString(sqlReader["C_BIK"]));
                    l.Add(temp);
                }
                Create_tb(I);
                for (int i = 0; i < tb[I].Length; i++)
                    for (int j = 0; j < tb[I][i].Length; j++)
                        tb[I][i][j].Text = l[i][j];
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

        private void Tab2() // Объекты
        {
            int I = 1;
            sqlConnection.Open();
            listlb = new List<string>() { "ID объекта", "Адрес",
                "Дата начала", "Дата окончания", "ID начальника", "ID заказчика", "В срок"};
            listtb = new List<string>() { "O_ID", "O_Address",
                "O_Start", "O_End", "O_HeadID", "O_CustomerID", "O_OnTime"};
            tabPage = tabPage2;
            Deltb(tb[I]);
            Create_Labels();
            l = new List<List<string>>();
            sqlReader = null;
            command = new SqlCommand("SELECT * FROM [Object]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    temp = new List<string>();
                    temp.Add(Convert.ToString(sqlReader["O_ID"]));
                    temp.Add(Convert.ToString(sqlReader["O_Address"]));
                    temp.Add(Convert.ToString(sqlReader["O_Start"]));
                    temp.Add(Convert.ToString(sqlReader["O_End"]));
                    temp.Add(Convert.ToString(sqlReader["O_HeadID"]));
                    temp.Add(Convert.ToString(sqlReader["O_CustomerID"]));
                    temp.Add(Convert.ToString(sqlReader["O_OnTime"]));
                    l.Add(temp);
                }
                Create_tb(I);
                for (int i = 0; i < tb[I].Length; i++)
                    for (int j = 0; j < tb[I][i].Length; j++)
                        tb[I][i][j].Text = l[i][j];
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            sqlConnection.Close();
        }

        private void Tab3() // Бригады
        {
            int I = 2;
            sqlConnection.Open();
            listlb = new List<string>() { "ID Бригады", "ID бригадира", "Тип бригады" };
            listtb = new List<string>() { "B_ID", "B_HeadID", "B_Type" };
            tabPage = tabPage3;
            Deltb(tb[I]);
            Create_Labels();
            l = new List<List<string>>();
            sqlReader = null;
            command = new SqlCommand("SELECT * FROM [Brigade]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    temp = new List<string>();
                    temp.Add(Convert.ToString(sqlReader["B_ID"]));
                    temp.Add(Convert.ToString(sqlReader["B_HeadID"]));
                    temp.Add(Convert.ToString(sqlReader["B_Type"]));
                    l.Add(temp);
                }
                Create_tb(I);
                for (int i = 0; i < tb[I].Length; i++)
                    for (int j = 0; j < tb[I][i].Length; j++)
                        tb[I][i][j].Text = l[i][j];
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

        private void Tab4() // Сотрудники
        {
            int I = 3;
            sqlConnection.Open();
            listlb = new List<string>() { "ID сотрудника", "Фамилия", "Имя", "Отчество",
                        "Телефон","Паспорт", "СНИЛС", "ИНН", "Должность" };
            listtb = new List<string>() { "W_ID", "W_Family", "W_Name", "W_Father",
                        "W_Phone", "W_Passport", "W_SNILS", "W_INN", "W_Post" };
        tabPage = tabPage4;
            Deltb(tb[I]);
            Create_Labels();
            l = new List<List<string>>();
            sqlReader = null;
            command = new SqlCommand("SELECT * FROM [Worker]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    temp = new List<string>();
                    temp.Add(Convert.ToString(sqlReader["W_ID"]));
                    temp.Add(Convert.ToString(sqlReader["W_Family"]));
                    temp.Add(Convert.ToString(sqlReader["W_Name"]));
                    temp.Add(Convert.ToString(sqlReader["W_Father"]));
                    temp.Add(Convert.ToString(sqlReader["W_Phone"]));
                    temp.Add(Convert.ToString(sqlReader["W_Passport"]));
                    temp.Add(Convert.ToString(sqlReader["W_SNILS"]));
                    temp.Add(Convert.ToString(sqlReader["W_INN"]));
                    temp.Add(Convert.ToString(sqlReader["W_Post"]));
                    l.Add(temp);
                }
                Create_tb(I);
                for (int i = 0; i < tb[I].Length; i++)
                    for (int j = 0; j < tb[I][i].Length; j++)
                        tb[I][i][j].Text = l[i][j];
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

        private void Tab5() // Состав бригад
        {
            int I = 4;
            sqlConnection.Open();
            listlb = new List<string>() { "ID бригады", "ID сотрудника" };
            listtb = new List<string>(){ "B_ID", "W_ID" };
            tabPage = tabPage5;
            Deltb(tb[I]);
            Create_Labels();
            l = new List<List<string>>();
            sqlReader = null;
            command = new SqlCommand("SELECT * FROM [BrigadeWorker]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    temp = new List<string>();
                    temp.Add(Convert.ToString(sqlReader["B_ID"]));
                    temp.Add(Convert.ToString(sqlReader["W_ID"]));
                    l.Add(temp);
                }
                Create_tb(I);
                for (int i = 0; i < tb[I].Length; i++)
                    for (int j = 0; j < tb[I][i].Length; j++)
                        tb[I][i][j].Text = l[i][j];
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

        private void Tab6() // Бригады на объектах
        {
            int I = 5;
            sqlConnection.Open();
            listlb = new List<string>() { "ID объекта", "ID бригады" };

            listtb = new List<string>() { "O_ID", "O_BrigadeID" };
            tabPage = tabPage6;
            Deltb(tb[I]);
            Create_Labels();
            l = new List<List<string>>();
            sqlReader = null;
            command = new SqlCommand("SELECT * FROM [ObjectBrigade]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    temp = new List<string>();
                    temp.Add(Convert.ToString(sqlReader["O_ID"]));
                    temp.Add(Convert.ToString(sqlReader["O_BrigadeID"]));
                    l.Add(temp);
                }
                Create_tb(I);
                for (int i = 0; i < tb[I].Length; i++)
                    for (int j = 0; j < tb[I][i].Length; j++)
                        tb[I][i][j].Text = l[i][j];
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


        private void button4_Click_1(object sender, EventArgs e)
        {
            Tab1();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Tab2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Tab3();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Tab4();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Tab5();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Tab6();
        }






















        //       Добавляем       //
        Label[] Qlabel;
        TextBox[] Qtextbox;
        List<string> Qlist;
        int Switch;

        private void Q_Create()
        {
            Qlabel = new Label[Qlist.Count];
            int a = 15, b = 60;
            for (int i = 0; i < Qlist.Count; i++)
            {
                Qlabel[i] = new Label();
                Qlabel[i].Parent = tabPage;
                Qlabel[i].BackColor = Color.Orange;
                Qlabel[i].Left = a;
                Qlabel[i].Top = b;
                Qlabel[i].Size = new Size(100, 50);
                Qlabel[i].Text = Qlist[i];
                Qlabel[i].ForeColor = Color.Black;
                Qlabel[i].Font = new Font(Qlabel[i].Font, FontStyle.Bold);
                Qlabel[i].Font = new Font(Qlabel[i].Font.Name, 9, Qlabel[i].Font.Style);
                Qlabel[i].TextAlign = ContentAlignment.MiddleCenter;
                Qlabel[i].BorderStyle = BorderStyle.FixedSingle;
                Qlabel[i].BringToFront();
                a += 100;
            }

            Qtextbox = new TextBox[Qlist.Count];
            a = 15;
            b = 110;
            for (int i = 0; i < Qlist.Count; i++)
            {
                Qtextbox[i] = new TextBox();
                Qtextbox[i].Parent = tabPage;
                Qtextbox[i].Left = a;
                Qtextbox[i].Top = b;
                Qtextbox[i].Multiline = true;
                Qtextbox[i].Size = new Size(100, 50);
                Qtextbox[i].ForeColor = Color.Black;
                Qtextbox[i].TextAlign = HorizontalAlignment.Center;
                Qtextbox[i].BorderStyle = BorderStyle.FixedSingle;
                Qtextbox[i].BringToFront();
                a += 100;
            }
        }

        private void Q_Delete()
        {
            if (Qtextbox != null)
                for (int i = 0; i < Qtextbox.Length; i++)
                    Qtextbox[i].Dispose();

            if (Qlabel != null)
                for (int i = 0; i < Qlabel.Length; i++)
                    Qlabel[i].Dispose();
        }



        private int InsertCustomer()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Компания' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[1].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Фамилия' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[2].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Имя' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[4].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Телефон' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[5].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Почта' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[6].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ИНН' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[7].Text == String.Empty)
            {
                MessageBox.Show("Поле 'СНИЛС' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            command = new SqlCommand("INSERT INTO [Customer] (C_Company, C_Family, C_Name, C_Father, C_Phone, C_Email, C_INN, C_SNILS, C_KPP, C_OGRN, C_BIK)VALUES(@C_Company, @C_Family, @C_Name, @C_Father, @C_Phone, @C_Email, @C_INN, @C_SNILS, @C_KPP, @C_OGRN, @C_BIK)", sqlConnection);

            command.Parameters.AddWithValue("C_Company", Qtextbox[0].Text);
            command.Parameters.AddWithValue("C_Family", Qtextbox[1].Text);
            command.Parameters.AddWithValue("C_Name", Qtextbox[2].Text);
            command.Parameters.AddWithValue("C_Father", Qtextbox[3].Text);
            command.Parameters.AddWithValue("C_Phone", Qtextbox[4].Text);
            command.Parameters.AddWithValue("C_Email", Qtextbox[5].Text);
            command.Parameters.AddWithValue("C_INN", Qtextbox[6].Text);
            command.Parameters.AddWithValue("C_SNILS", Qtextbox[7].Text);
            command.Parameters.AddWithValue("C_KPP", Qtextbox[8].Text);
            command.Parameters.AddWithValue("C_OGRN", Qtextbox[9].Text);
            command.Parameters.AddWithValue("C_BIK", Qtextbox[10].Text);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
                f = false;
            }
            finally
            {
                if (f)
                {
                    Q_Delete();
                    MessageBox.Show("Данные успешно введены!");
                }
            }
            f = true;

            sqlConnection.Close();
            return 0;

        }

        private int InsertObject()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Адрес' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[1].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Дата начала' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[2].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Дата окончания' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[3].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID начальника' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[4].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID заказчика' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[5].Text == String.Empty)
            {
                MessageBox.Show("Поле 'В срок должно быть заполнено!");
                return 1;
            }
            sqlConnection.Open();

            command = new SqlCommand("INSERT INTO [Object] (O_Address, O_Start, O_End, O_HeadID, O_CustomerID, O_OnTime)VALUES(@O_Address, @O_Start, @O_End, @O_HeadID, @O_CustomerID, @O_OnTime)", sqlConnection);



            try
            {
                command.Parameters.AddWithValue("O_Address", Qtextbox[0].Text);
                command.Parameters.AddWithValue("O_Start", Qtextbox[1].Text);
                command.Parameters.AddWithValue("O_End", Qtextbox[2].Text);
                command.Parameters.AddWithValue("O_HeadID", Convert.ToInt32(Qtextbox[3].Text));
                command.Parameters.AddWithValue("O_CustomerID", Convert.ToInt32(Qtextbox[4].Text));
                command.Parameters.AddWithValue("O_OnTime", Qtextbox[5].Text);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
                f = false;
            }
            finally
            {
                if (f)
                {
                    Q_Delete();
                    MessageBox.Show("Данные успешно введены!");
                }
            }
            f = true;

            sqlConnection.Close();
            return 0;
        }

        private int InsertBrigade()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID бригадира' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[1].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Тип бригады' должно быть заполнено!");
                return 1;
            }
            

            sqlConnection.Open();

            command = new SqlCommand("INSERT INTO [Brigade] (B_HeadID, B_Type)VALUES(@B_HeadID, @B_Type)", sqlConnection);

            command.Parameters.AddWithValue("B_HeadID", Convert.ToInt32(Qtextbox[0].Text));
            command.Parameters.AddWithValue("B_Type", Qtextbox[1].Text);


            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
                f = false;
            }
            finally
            {
                if (f)
                {
                    Q_Delete();
                    MessageBox.Show("Данные успешно введены!");
                }
            }
            f = true;

            sqlConnection.Close();
            return 0;
        }

        private int InsertWorker()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Фамилия' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[1].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Имя' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[2].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Отчество' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[3].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Номер телефона' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[4].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Паспорт' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[5].Text == String.Empty)
            {
                MessageBox.Show("Поле 'СНИЛС' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[6].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ИНН' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[7].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Должность' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();
            try
            {

                command = new SqlCommand("INSERT INTO [Worker] (W_Family, W_Name, W_Father, W_Phone, W_Passport, W_SNILS, W_INN, W_Post)VALUES(@W_Family, @W_Name, @W_Father, @W_Phone, @W_Passport, @W_SNILS, @W_INN, @W_Post)", sqlConnection);

                command.Parameters.AddWithValue("W_Family", Qtextbox[0].Text);
                command.Parameters.AddWithValue("W_Name", Qtextbox[1].Text);
                command.Parameters.AddWithValue("W_Father", Qtextbox[2].Text);
                command.Parameters.AddWithValue("W_Phone", Qtextbox[3].Text);
                command.Parameters.AddWithValue("W_Passport", Qtextbox[4].Text);
                command.Parameters.AddWithValue("W_SNILS", Qtextbox[5].Text);
                command.Parameters.AddWithValue("W_INN", Qtextbox[6].Text);
                command.Parameters.AddWithValue("W_Post", Qtextbox[7].Text);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
                f = false;
            }
            finally
            {
                if (f)
                {
                    Q_Delete();
                    MessageBox.Show("Данные успешно введены!");
                }
            }
            f = true;

            sqlConnection.Close();
            return 0;
        }

        private int InsertBrigadeWorker()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID бригады' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[1].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID сотрудника' должно быть заполнено!");
                return 1;
            }
            
            sqlConnection.Open();

            command = new SqlCommand("INSERT INTO [BrigadeWorker] (B_ID, W_ID)VALUES(@B_ID, @W_ID)", sqlConnection);

            command.Parameters.AddWithValue("B_ID", Convert.ToInt32(Qtextbox[0].Text));
            command.Parameters.AddWithValue("W_ID", Convert.ToInt32(Qtextbox[1].Text));
            

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
                f = false;
            }
            finally
            {
                if (f)
                {
                    Q_Delete();
                    MessageBox.Show("Данные успешно введены!");
                }
            }
            f = true;

            sqlConnection.Close();
            return 0;
        }

        private int InsertObjectBrigade()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID объекта' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[1].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID бригады' должно быть заполнено!");
                return 1;
            }
            
            sqlConnection.Open();

            command = new SqlCommand("INSERT INTO [ObjectBrigade] (O_ID, O_BrigadeID)VALUES(@O_ID, @O_BrigadeID)", sqlConnection);

            command.Parameters.AddWithValue("O_ID", Convert.ToInt32(Qtextbox[0].Text));
            command.Parameters.AddWithValue("O_BrigadeID", Convert.ToInt32(Qtextbox[1].Text));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
                f = false;
            }
            finally
            {
                if (f)
                {
                    Q_Delete();
                    MessageBox.Show("Данные успешно введены!");
                }
            }
            f = true;

            sqlConnection.Close();
            return 0;
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            switch (Switch)
            {
                case (0):
                    InsertCustomer();
                    break;

                case (1):
                    InsertObject();
                    break;

                case (2):
                    InsertBrigade();
                    break;

                case (3):
                    InsertWorker();
                    break;

                case (4):
                    InsertBrigadeWorker();
                    break;

                case (5):
                    InsertObjectBrigade();
                    break;
            }
        }
       

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabPage = tabPage10;
            Switch = comboBox1.SelectedIndex;
            switch (Switch)
            {
                case (0):
                    Q_Delete();
                    Qlist = new List<string> { "Компания", "Фамилия", "Имя", "Отчество",
                        "Телефон", "Почта", "ИНН", "СНИЛС", "КПП", "ОГРН", "БИК" };
                    Q_Create();
                    break;

                case (1):
                    Q_Delete();
                    Qlist = new List<string>() { "Адрес", "Дата начала", "Дата окончания",
                        "ID начальника", "ID заказчика", "В срок"};
                    Q_Create();
                    break;

                case (2):
                    Q_Delete();
                    Qlist = new List<string>() { "ID бригадира", "Тип бригады" };
                    Q_Create();
                    break;

                case (3):
                    Q_Delete();
                    Qlist = new List<string>() { "Фамилия", "Имя", "Отчество",
                        "Телефон","Паспорт", "СНИЛС", "ИНН", "Должность" };
                    Q_Create();
                    break;

                case (4):
                    Q_Delete();
                    Qlist = new List<string>() { "ID бригады", "ID сотрудника" };
                    Q_Create();
                    break;

                case (5):
                    Q_Delete();
                    Qlist = new List<string>() { "ID объекта", "ID бригады" };
                    Q_Create();
                    break;
            }
        }











        //          Удаляем            //


        private int DeleteCustomer()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID заказчика' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            try
            {
                int id = Convert.ToInt32(Qtextbox[0].Text);
                

                

                command = new SqlCommand("DELETE FROM [Object] WHERE [O_CustomerID]=@O_CustomerID", sqlConnection);
                command.Parameters.AddWithValue("O_CustomerID", id);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM [Customer] WHERE [C_ID]=@C_ID", sqlConnection);
                command.Parameters.AddWithValue("C_ID", id);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка");
                f = false;
            }
            finally
            {
                if (f)
                {
                    Q_Delete();
                    MessageBox.Show("Данные успешно удалены!");
                }
            }
            f = true;



            sqlConnection.Close();
            return 0;

        }

        private int DeleteObject()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID объекта' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            try
            {
                int id = Convert.ToInt32(Qtextbox[0].Text);




                command = new SqlCommand("DELETE FROM [ObjectBrigade] WHERE [O_ID]=@O_ID", sqlConnection);
                command.Parameters.AddWithValue("O_ID", id);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM [Object] WHERE [O_ID]=@O_ID", sqlConnection);
                command.Parameters.AddWithValue("O_ID", id);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
                f = false;
            }
            finally
            {
                if (f)
                {
                    Q_Delete();
                    MessageBox.Show("Данные успешно удалены!");
                }
            }
            f = true;



            sqlConnection.Close();
            return 0;

        }

        private int DeleteBrigade()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID бригады' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            try
            {
                int id = Convert.ToInt32(Qtextbox[0].Text);

                command = new SqlCommand("DELETE FROM [ObjectBrigade] WHERE [O_BrigadeID]=@O_BrigadeID", sqlConnection);
                command.Parameters.AddWithValue("O_BrigadeID", id);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM [BrigadeWorker] WHERE [B_ID]=@B_ID", sqlConnection);
                command.Parameters.AddWithValue("B_ID", id);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM [Brigade] WHERE [B_ID]=@B_ID", sqlConnection);
                command.Parameters.AddWithValue("B_ID", id);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
                f = false;
            }
            finally
            {
                if (f)
                {
                    Q_Delete();
                    MessageBox.Show("Данные успешно удалены!");
                }
            }
            f = true;



            sqlConnection.Close();
            return 0;

        }

        private int DeleteWorker()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID сотрудника' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            try
            {
                int id = Convert.ToInt32(Qtextbox[0].Text);
                int a = -1;

                sqlReader = null;
                command = new SqlCommand("SELECT [B_ID] FROM [Brigade] WHERE [B_HeadID] = @B_HeadID", sqlConnection);
                command.Parameters.AddWithValue("B_HeadID", id);

                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    a = Convert.ToInt32(sqlReader["P_AppNumber"]);
                }
                if (sqlReader != null)
                    sqlReader.Close();


                if (a != -1)
                {
                    command = new SqlCommand("DELETE FROM [ObjectBrigade] WHERE [O_BrigadeID] = @O_BrigadeID", sqlConnection);
                    command.Parameters.AddWithValue("O_BrigadeID", a);
                    command.ExecuteNonQuery();

                    command = new SqlCommand("DELETE FROM [Brigade] WHERE [B_HeadID] = @B_HeadID", sqlConnection);
                    command.Parameters.AddWithValue("B_HeadID", id);
                    command.ExecuteNonQuery();
                }

                command = new SqlCommand("DELETE FROM [BrigadeWorker] WHERE [W_ID] = @W_ID", sqlConnection);
                command.Parameters.AddWithValue("W_ID", id);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
                f = false;
            }
            finally
            {
                if (f)
                {
                    Q_Delete();
                    MessageBox.Show("Данные успешно удалены!");
                }
            }
            f = true;



            sqlConnection.Close();
            return 0;
        }

        private int DeleteBrigadeWorker()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID сотрудника' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            try
            {
                int id = Convert.ToInt32(Qtextbox[0].Text);
                
                command = new SqlCommand("DELETE FROM [BrigadeWorker] WHERE [W_ID]=@W_ID", sqlConnection);
                command.Parameters.AddWithValue("W_ID", id);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
                f = false;
            }
            finally
            {
                if (f)
                {
                    Q_Delete();
                    MessageBox.Show("Данные успешно удалены!");
                }
            }
            f = true;



            sqlConnection.Close();
            return 0;

        }

        private int DeleteObjectBrigade()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID бригады' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            try
            {
                int id = Convert.ToInt32(Qtextbox[0].Text);

                command = new SqlCommand("DELETE FROM [ObjectBrigade] WHERE [O_BrigadeID]=@O_BrigadeID", sqlConnection);
                command.Parameters.AddWithValue("O_BrigadeID", id);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
                f = false;
            }
            finally
            {
                if (f)
                {
                    Q_Delete();
                    MessageBox.Show("Данные успешно удалены!");
                }
            }
            f = true;



            sqlConnection.Close();
            return 0;

        }

        

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabPage = tabPage12;
            Switch = comboBox3.SelectedIndex;
            switch (Switch)
            {
                case (0):
                    Q_Delete();
                    Qlist = new List<string> { "ID заказчика" };
                    Q_Create();
                    break;

                case (1):
                    Q_Delete();
                    Qlist = new List<string>() { "ID объекта" };
                    Q_Create();
                    break;

                case (2):
                    Q_Delete();
                    Qlist = new List<string>() { "ID бригады" };
                    Q_Create();
                    break;

                case (3):
                    Q_Delete();
                    Qlist = new List<string>() { "ID сотрудника" };
                    Q_Create();
                    break;

                case (4):
                    Q_Delete();
                    Qlist = new List<string>() { "ID сотрудника" };
                    Q_Create();
                    break;

                case (5):
                    Q_Delete();
                    Qlist = new List<string>() { "ID объекта" };
                    Q_Create();
                    break;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            switch (Switch)
            {
                case (0):
                    DeleteCustomer();
                    break;

                case (1):
                    DeleteObject();
                    break;

                case (2):
                    DeleteBrigade();
                    break;

                case (3):
                    DeleteWorker();
                    break;

                case (4):
                    DeleteBrigadeWorker();
                    break;

                case (5):
                    DeleteObjectBrigade();
                    break;
            }
        }







    }
}
