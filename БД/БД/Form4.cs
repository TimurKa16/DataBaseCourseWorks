using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace БД
{
    public partial class Form4 : Form
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




        public Form4(SqlConnection sqlConnection, SqlCommand command, SqlDataReader sqlReader)
        {
            this.sqlConnection = sqlConnection;
            this.sqlReader = sqlReader;
            this.command = command;
            InitializeComponent();
            tabPage1.AutoScroll = true;
            tabPage2.AutoScroll = true;
            tabPage3.AutoScroll = true;
            tabPage4.AutoScroll = true;
            tabPage5.AutoScroll = true;
            tabPage6.AutoScroll = true;
            tabPage7.AutoScroll = true;
            tabPage8.AutoScroll = true;
            tabPage10.AutoScroll = true;
            tabPage12.AutoScroll = true;
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
        
        private void tabPage10_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void Form4_Load(object sender, EventArgs e)
        {
            tb = new TextBox[8][][];
            Tab1();
            Tab2();
            Tab3();
            Tab4();
            Tab5();
            Tab6();
            Tab7();
            Tab8();

        }
                
        private void tabPage11_Click(object sender, EventArgs e)
        {

        }



        private void Tab1() // Заказчик
        {
            int I = 0;
            sqlConnection.Open();
            listlb = new List<string> { "ID заказчика", "Компания",
                "Фамилия", "Имя", "Отчество", "Телефон", "Почта", "Сайт", "ИНН" };
            listtb = new List<string> { "C_ID", "C_Company",
                "C_Family", "C_Name", "C_Father", "C_Phone", "C_Email", "C_WebSite", "C_INN" };
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
                    temp.Add(Convert.ToString(sqlReader["C_WebSite"]));
                    temp.Add(Convert.ToString(sqlReader["C_INN"]));
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

        private void Tab2() // Реквизиты
        {
            int I = 1;
            sqlConnection.Open();
            listlb = new List<string>() { "ID заказчика", "КПП",
                "БИК", "ОГРН", "Дата основания"};
            listtb = new List<string>() { "C_ID", "C_KPP",
                "C_BIK", "C_OGRN", "C_Data"};
            tabPage = tabPage2;
            Deltb(tb[I]);
            Create_Labels();
            l = new List<List<string>>();
            sqlReader = null;
            command = new SqlCommand("SELECT * FROM [Props]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    temp = new List<string>();
                    temp.Add(Convert.ToString(sqlReader["C_ID"]));
                    temp.Add(Convert.ToString(sqlReader["C_KPP"]));
                    temp.Add(Convert.ToString(sqlReader["C_BIK"]));
                    temp.Add(Convert.ToString(sqlReader["C_OGRN"]));
                    temp.Add(Convert.ToString(sqlReader["C_Data"]));
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

        private void Tab3() // Страны
        {
            int I = 2;
            sqlConnection.Open();
            listlb = new List<string>() { "ID заказчика", "Страна" };
            listtb = new List<string>() { "C_ID", "C_Country" };
            tabPage = tabPage3;
            Deltb(tb[I]);
            Create_Labels();
            l = new List<List<string>>();
            sqlReader = null;
            command = new SqlCommand("SELECT * FROM [Country]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    temp = new List<string>();
                    temp.Add(Convert.ToString(sqlReader["C_ID"]));
                    temp.Add(Convert.ToString(sqlReader["C_Country"]));
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

        private void Tab4() // Товар
        {
            int I = 3;
            sqlConnection.Open();
            listlb = new List<string>() { "Номер заявки", "ID заказчика","Дата загрузки", "Дата разгрузки",
                        "Цена груза", "Адрес загрузки", "Адрес разгрузки", "Длина маршрута",
                        "За границу", "Объём", "Вес", "Длина", "Ширина", "Высота" };
            listtb = new List<string>() { "P_AppNumber", "P_ID", "P_Date1", "P_Date2", "P_Cost", "P_Address1",
                        "P_Address2", "P_Distance", "P_International", "P_Volume", "P_Weight", "P_Length",
                        "P_Width", "P_Height" };
            tabPage = tabPage4;
            Deltb(tb[I]);
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
                    temp.Add(Convert.ToString(sqlReader["P_AppNumber"]));
                    temp.Add(Convert.ToString(sqlReader["P_ID"]));
                    temp.Add(Convert.ToString(sqlReader["P_Date1"]));
                    temp.Add(Convert.ToString(sqlReader["P_Date2"]));
                    temp.Add(Convert.ToString(sqlReader["P_Cost"]));
                    temp.Add(Convert.ToString(sqlReader["P_Address1"]));
                    temp.Add(Convert.ToString(sqlReader["P_Address2"]));
                    temp.Add(Convert.ToString(sqlReader["P_Distance"]));
                    temp.Add(Convert.ToString(sqlReader["P_International"]));
                    temp.Add(Convert.ToString(sqlReader["P_Volume"]));
                    temp.Add(Convert.ToString(sqlReader["P_Weight"]));
                    temp.Add(Convert.ToString(sqlReader["P_Length"]));
                    temp.Add(Convert.ToString(sqlReader["P_Width"]));
                    temp.Add(Convert.ToString(sqlReader["P_Height"]));
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

        private void Tab5() // Рейс
        {
            int I = 4;
            sqlConnection.Open();
            listlb = new List<string>() { "ID", "Время отправления", "Время прибытия", "Адрес отправления",
                        "Адрес прибытия", "Длина маршрута", "Стоимость груза", "ID водителя", "ID машины" };
            listtb = new List<string>(){ "T_RouteID", "T_Date1", "T_Date2", "T_Address1",
                        "T_Address2", "T_RouteDistance", "T_Cost", "T_DriverID", "T_VanID" };
            tabPage = tabPage5;
            Deltb(tb[I]);
            Create_Labels();
            l = new List<List<string>>();
            sqlReader = null;
            command = new SqlCommand("SELECT * FROM [Trip]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    temp = new List<string>();
                    temp.Add(Convert.ToString(sqlReader["T_RouteID"]));
                    temp.Add(Convert.ToString(sqlReader["T_Date1"]));
                    temp.Add(Convert.ToString(sqlReader["T_Date2"]));
                    temp.Add(Convert.ToString(sqlReader["T_Address1"]));
                    temp.Add(Convert.ToString(sqlReader["T_Address2"]));
                    temp.Add(Convert.ToString(sqlReader["T_RouteDistance"]));
                    temp.Add(Convert.ToString(sqlReader["T_Cost"]));
                    temp.Add(Convert.ToString(sqlReader["T_DriverID"]));
                    temp.Add(Convert.ToString(sqlReader["T_VanID"]));
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

        private void Tab6() // Остановки рейсов
        {
            int I = 5;
            sqlConnection.Open();
            listlb = new List<string>() { "ID рейса", "ID заявки" };

            listtb = new List<string>() { "T_RouteID", "T_AppNumber" };
            tabPage = tabPage6;
            Deltb(tb[I]);
            Create_Labels();
            l = new List<List<string>>();
            sqlReader = null;
            command = new SqlCommand("SELECT * FROM [AppNumber]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    temp = new List<string>();
                    temp.Add(Convert.ToString(sqlReader["T_RouteID"]));
                    temp.Add(Convert.ToString(sqlReader["T_AppNumber"]));
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

        private void Tab7() // Водители
        {
            int I = 6;
            sqlConnection.Open();
            listlb = new List<string>() { "ID водителя", "Номер удостоверения", "Фамилия", "Имя",
                        "Отчество", "Номер паспорта", "Номер ИНН", "Номер СНИЛС", "Дата рождения", "Номер телефона" };

            listtb = new List<string>(){ "D_ID", "D_LisenceNumber", "D_Family", "D_Name",
                        "D_Father", "D_Passport", "D_INN", "D_SNILS", "D_Birth", "D_Phone" };
            tabPage = tabPage7;
            Deltb(tb[I]);
            Create_Labels();
            l = new List<List<string>>();
            sqlReader = null;
            command = new SqlCommand("SELECT * FROM [Driver]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    temp = new List<string>();
                    temp.Add(Convert.ToString(sqlReader["D_ID"]));
                    temp.Add(Convert.ToString(sqlReader["D_LisenceNumber"]));
                    temp.Add(Convert.ToString(sqlReader["D_Family"]));
                    temp.Add(Convert.ToString(sqlReader["D_Name"]));
                    temp.Add(Convert.ToString(sqlReader["D_Father"]));
                    temp.Add(Convert.ToString(sqlReader["D_Passport"]));
                    temp.Add(Convert.ToString(sqlReader["D_INN"]));
                    temp.Add(Convert.ToString(sqlReader["D_SNILS"]));
                    temp.Add(Convert.ToString(sqlReader["D_Birth"]));
                    temp.Add(Convert.ToString(sqlReader["D_Phone"]));
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

        private void Tab8() // Машины
        {
            int I = 7;
            sqlConnection.Open();
            listlb = new List<string>() { "ID Машины", "Рег номер", "Длина", "Ширина", "Высота",
                        "Грузоподъёмность", "Год выпуска", "Потребление топлива", "Объём бака", "Тип топлива", "Модель" };

            listtb = new List<string>(){ "V_ID", "V_RegNumber", "V_Length", "V_Width", "V_Height",
                        "V_Carry", "V_Year", "V_Consume", "V_TankCap", "V_Fuel", "V_Model" };
            tabPage = tabPage8;
            Deltb(tb[I]);
            Create_Labels();
            l = new List<List<string>>();
            sqlReader = null;
            command = new SqlCommand("SELECT * FROM [Van]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    temp = new List<string>();
                    temp.Add(Convert.ToString(sqlReader["V_ID"]));
                    temp.Add(Convert.ToString(sqlReader["V_RegNumber"]));
                    temp.Add(Convert.ToString(sqlReader["V_Length"]));
                    temp.Add(Convert.ToString(sqlReader["V_Width"]));
                    temp.Add(Convert.ToString(sqlReader["V_Height"]));
                    temp.Add(Convert.ToString(sqlReader["V_Carry"]));
                    temp.Add(Convert.ToString(sqlReader["V_Year"]));
                    temp.Add(Convert.ToString(sqlReader["V_Consume"]));
                    temp.Add(Convert.ToString(sqlReader["V_TankCap"]));
                    temp.Add(Convert.ToString(sqlReader["V_Fuel"]));
                    temp.Add(Convert.ToString(sqlReader["V_Model"]));
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









        //        Обновляем        //
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button10_Click(object sender, EventArgs e)
        {
            Tab7();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Tab8();
        }





























        //          Добавление          //


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
            if (Qtextbox[7].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ИНН' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            command = new SqlCommand("INSERT INTO [Customer] (C_Company, C_Family, C_Name, C_Father, C_Phone, C_Email, C_WebSite, C_INN)VALUES(@C_Company, @C_Family, @C_Name, @C_Father, @C_Phone, @C_Email, @C_WebSite, @C_INN)", sqlConnection);

            command.Parameters.AddWithValue("C_Company", Qtextbox[0].Text);
            command.Parameters.AddWithValue("C_Family", Qtextbox[1].Text);
            command.Parameters.AddWithValue("C_Name", Qtextbox[2].Text);
            command.Parameters.AddWithValue("C_Father", Qtextbox[3].Text);
            command.Parameters.AddWithValue("C_Phone", Qtextbox[4].Text);
            command.Parameters.AddWithValue("C_Email", Qtextbox[5].Text);
            command.Parameters.AddWithValue("C_WebSite", Qtextbox[6].Text);
            command.Parameters.AddWithValue("C_INN", Qtextbox[7].Text);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int InsertAppNumber()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID рейса' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[1].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID заявки' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            command = new SqlCommand("INSERT INTO [AppNumber] (T_RouteID, T_AppNumber)VALUES(@T_RouteID, @T_AppNumber)", sqlConnection);

            
        
            try
            {
                command.Parameters.AddWithValue("T_RouteID", Convert.ToInt32(Qtextbox[0].Text));
                command.Parameters.AddWithValue("T_AppNumber", Convert.ToInt32(Qtextbox[1].Text));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int InsertDriver()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Номер удостоверения' должно быть заполнено!");
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
                MessageBox.Show("Поле 'Номер паспорта' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[5].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Номер ИНН' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[6].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Номер СНИЛС' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[7].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Дата рождения' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[8].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Номер телефона' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            command = new SqlCommand("INSERT INTO [Driver] (D_LisenceNumber, D_Family, D_Name, D_Father, D_Passport, D_INN, D_SNILS, D_Birth, D_Phone)VALUES(@D_LisenceNumber, @D_Family, @D_Name, @D_Father, @D_Passport, @D_INN, @D_SNILS, @D_Birth, @D_Phone)", sqlConnection);

            command.Parameters.AddWithValue("D_LisenceNumber", Qtextbox[0].Text);
            command.Parameters.AddWithValue("D_Family", Qtextbox[1].Text);
            command.Parameters.AddWithValue("D_Name", Qtextbox[2].Text);
            command.Parameters.AddWithValue("D_Father", Qtextbox[3].Text);
            command.Parameters.AddWithValue("D_Passport", Qtextbox[4].Text);
            command.Parameters.AddWithValue("D_INN", Qtextbox[5].Text);
            command.Parameters.AddWithValue("D_SNILS", Qtextbox[6].Text);
            command.Parameters.AddWithValue("D_Birth", (Qtextbox[7].Text));
            command.Parameters.AddWithValue("D_Phone", Qtextbox[8].Text);


            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int InsertCountry()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID заказчика' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[1].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Страна' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();
            try
            {
                
                command = new SqlCommand("INSERT INTO [Country] (C_ID, C_Country)VALUES(@C_ID, @C_Country)", sqlConnection);

                command.Parameters.AddWithValue("C_ID", Convert.ToInt32(Qtextbox[0].Text));
                command.Parameters.AddWithValue("C_Country", Qtextbox[1].Text);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int InsertProduct()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID заказчика' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[1].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Дата загрузки' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[2].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Дата разгрузки' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[3].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Цена груза' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[4].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Адрес загрузки' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[5].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Адрес разгрузки' должно быть заполнено!");
                return 1;
            }
            
            if (Qtextbox[6].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Длина пути' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[7].Text == String.Empty)
            {
                MessageBox.Show("Поле 'За границу' должно быть заполнено!");
                return 1;
            }
            sqlConnection.Open();

            command = new SqlCommand("INSERT INTO [Product] (P_ID, P_Date1, P_Date2, P_Cost, P_Address1, P_Address2, P_Distance, P_International, P_Volume, P_Weight, P_Length, P_Width, P_Height)VALUES(@P_ID, @P_Date1, @P_Date2, @P_Cost, @P_Address1, @P_Address2, @P_Distance, @P_International, @P_Volume, @P_Weight, @P_Length, @P_Width, @P_Height)", sqlConnection);

            command.Parameters.AddWithValue("P_ID", Convert.ToInt32(Qtextbox[0].Text));
            command.Parameters.AddWithValue("P_Date1", Qtextbox[1].Text);
            command.Parameters.AddWithValue("P_Date2", Qtextbox[2].Text);
            command.Parameters.AddWithValue("P_Cost", Qtextbox[3].Text);
            command.Parameters.AddWithValue("P_Address1", Qtextbox[4].Text);
            command.Parameters.AddWithValue("P_Address2", Qtextbox[5].Text);
            command.Parameters.AddWithValue("P_Distance", Qtextbox[6].Text);
            command.Parameters.AddWithValue("P_International", Qtextbox[7].Text);
            command.Parameters.AddWithValue("P_Volume", Qtextbox[8].Text);
            command.Parameters.AddWithValue("P_Weight", Qtextbox[9].Text);
            command.Parameters.AddWithValue("P_Length", Qtextbox[10].Text);
            command.Parameters.AddWithValue("P_Width", Qtextbox[11].Text);
            command.Parameters.AddWithValue("P_Height", Qtextbox[12].Text);  

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int InsertProps()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID заказчика' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[1].Text == String.Empty)
            {
                MessageBox.Show("Поле 'КПП' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[2].Text == String.Empty)
            {
                MessageBox.Show("Поле 'БИК' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[3].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ОГРН' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[4].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Дата основания' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            command = new SqlCommand("INSERT INTO [Props] (C_ID, C_KPP, C_BIK, C_OGRN, C_Data)VALUES(@C_ID, @C_KPP, @C_BIK, @C_OGRN, @C_Data)", sqlConnection);

            command.Parameters.AddWithValue("C_ID", Qtextbox[0].Text);
            command.Parameters.AddWithValue("C_KPP", Qtextbox[1].Text);
            command.Parameters.AddWithValue("C_BIK", Qtextbox[2].Text);
            command.Parameters.AddWithValue("C_OGRN", Qtextbox[3].Text);
            command.Parameters.AddWithValue("C_Data", Qtextbox[4].Text);
            
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int InsertTrip()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Дата загрузки' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[1].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Дата разгрузки' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[2].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Адрес загрузки' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[3].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Адрес разгрузки' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[4].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Длина маршрута' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[5].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Цена груза' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[6].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID водителя' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[7].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID машины' должно быть заполнено!");
                return 1;
            }


            sqlConnection.Open();

            command = new SqlCommand("INSERT INTO [Trip] (T_Date1, T_Date2, T_Address1, T_Address2, T_RouteDistance, T_Cost, T_DriverID, T_VanID)VALUES(@T_Date1, @T_Date2, @T_Address1, @T_Address2, @T_RouteDistance, @T_Cost, @T_DriverID, @T_VanID)", sqlConnection);
            
            command.Parameters.AddWithValue("T_Date1", Qtextbox[0].Text);
            command.Parameters.AddWithValue("T_Date2", Qtextbox[1].Text);
            command.Parameters.AddWithValue("T_Address1", Qtextbox[2].Text);
            command.Parameters.AddWithValue("T_Address2", Qtextbox[3].Text);
            command.Parameters.AddWithValue("T_RouteDistance", Qtextbox[4].Text);
            command.Parameters.AddWithValue("T_Cost", Qtextbox[5].Text);
            command.Parameters.AddWithValue("T_DriverID", Qtextbox[6].Text);
            command.Parameters.AddWithValue("T_VanID", Qtextbox[7].Text);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int InsertVan()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Рег номер' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[4].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Грузоподъёмность' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[5].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Год выпуска' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[6].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Потребление топлива' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[7].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Объём бака' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[8].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Тип топлива' должно быть заполнено!");
                return 1;
            }
            if (Qtextbox[9].Text == String.Empty)
            {
                MessageBox.Show("Поле 'Модель' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            command = new SqlCommand("INSERT INTO [Van] (V_RegNumber, V_Length, V_Width, V_Height, V_Carry, V_Year, V_Consume, V_TankCap, V_Fuel, V_Model)VALUES(@V_RegNumber, @V_Length, @V_Width, @V_Height, @V_Carry, @V_Year, @V_Consume, @V_TankCap, @V_Fuel, @V_Model)", sqlConnection);
            
            command.Parameters.AddWithValue("V_RegNumber", Qtextbox[0].Text);
            command.Parameters.AddWithValue("V_Length", Qtextbox[1].Text);
            command.Parameters.AddWithValue("V_Width", Qtextbox[2].Text);
            command.Parameters.AddWithValue("V_Height", Qtextbox[3].Text);
            command.Parameters.AddWithValue("V_Carry", Qtextbox[4].Text);
            command.Parameters.AddWithValue("V_Year", Qtextbox[5].Text);
            command.Parameters.AddWithValue("V_Consume", Qtextbox[6].Text);
            command.Parameters.AddWithValue("V_TankCap", Qtextbox[7].Text);
            command.Parameters.AddWithValue("V_Fuel", Qtextbox[8].Text);
            command.Parameters.AddWithValue("V_Model", Qtextbox[9].Text);


            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabPage = tabPage10;
            Switch = comboBox1.SelectedIndex;
            switch (Switch)
            {
                case (0):
                    Q_Delete();
                    Qlist = new List<string> { "Компания", "Фамилия", "Имя", "Отчество", "Телефон", "Почта", "Сайт", "ИНН" };                                       
                    Q_Create();
                    break;

                case (1):
                    Q_Delete();
                    Qlist = new List<string>() { "ID", "КПП",
                "БИК", "ОГРН", "Дата основания"};
                    Q_Create();
                    break;

                case (2):
                    Q_Delete();
                    Qlist = new List<string>() { "ID", "Страна" };
                    Q_Create();
                    break;

                case (3):
                    Q_Delete();
                    Qlist = new List<string>() { "ID заказчика", "Дата загрузки", "Дата разгрузки",
                        "Цена груза", "Адрес загрузки", "Адрес разгрузки", "Длина маршрута",
                        "За границу", "Объём" , "Вес", "Длина", "Ширина", "Высота"};
                    Q_Create();
                    break;

                case (4):
                    Q_Delete();
                    Qlist = new List<string>() { "Время отправления", "Время прибытия", "Адрес отправления",
                        "Адрес прибытия", "Длина маршрута", "Стоимость груза", "ID водителя", "ID машины" };
                    Q_Create();
                    break;

                case (5):
                    Q_Delete();
                    Qlist = new List<string>() { "ID рейса", "ID заявки" };
                    Q_Create();
                    break;

                case (6):
                    Q_Delete();
                    Qlist = new List<string>() { "Номер удостоверения", "Фамилия", "Имя",
                        "Отчество", "Номер паспорта", "Номер ИНН", "Номер СНИЛС", "Дата рождения", "Номер телефона" };
                    Q_Create();
                    break;

                case (7):
                    Q_Delete();
                    Qlist = new List<string>() { "Рег номер", "Длина", "Ширина", "Высота",
                        "Грузоподъёмность", "Год выпуска", "Потребление топлива", "Объём бака", "Тип топлива", "Модель" };
                    Q_Create();
                    break;
            }

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            switch (Switch)
            {
                case (0):
                    InsertCustomer();
                    break;

                case (1):
                    InsertProps();
                    break;

                case (2):
                    InsertCountry();
                    break;

                case (3):
                    InsertProduct();
                    break;

                case (4):
                    InsertTrip();
                    break;

                case (5):
                    InsertAppNumber();
                    break;

                case (6):
                    InsertDriver();
                    break;

                case (7):
                    InsertVan();
                    break;
            }
        }



























        

        //              Удаление                   //

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
                sqlReader = null;
                command = new SqlCommand("SELECT [P_AppNumber] FROM [Product] WHERE [P_ID] = @P_ID", sqlConnection);
                command.Parameters.AddWithValue("P_ID", id);
                List<int> L1 = new List<int>();

                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    L1.Add(Convert.ToInt32(sqlReader["P_AppNumber"]));
                }
                L1.Distinct();
            if (sqlReader != null)
                sqlReader.Close();
            // Есть список заявок P_AppNumber, с удаляемыми C_ID

            List<int> L2 = new List<int>();
                sqlReader = null;
                for (int i = 0; i < L1.Count; i++)
                {
                    command = new SqlCommand("SELECT [T_RouteID] FROM [AppNumber] WHERE [T_AppNumber] = @T_AppNumber", sqlConnection);
                    command.Parameters.AddWithValue("T_AppNumber", L1[i]);
                    sqlReader = command.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        L2.Add(Convert.ToInt32(sqlReader["T_RouteID"]));
                    }
                if (sqlReader != null)
                    sqlReader.Close();
            }
                L2.Distinct();
            // Есть список рейсов T_RouteID с удаляемыми заявками AppNumber

            // Теперь удаляем в AppNumber
            for (int i = 0; i < L2.Count; i++)
                {
                    command = new SqlCommand("DELETE FROM [AppNumber] WHERE [T_RouteID] = @T_RouteID", sqlConnection);
                    command.Parameters.AddWithValue("T_RouteID", L2[i]);
                    command.ExecuteNonQuery();

                    command = new SqlCommand("DELETE FROM [Trip] WHERE [T_RouteID] = @T_RouteID", sqlConnection);
                    command.Parameters.AddWithValue("T_RouteID", L2[i]);
                    command.ExecuteNonQuery();

                }

                command = new SqlCommand("DELETE FROM [Product] WHERE [P_ID] = @P_ID", sqlConnection);
                command.Parameters.AddWithValue("P_ID", id);
                command.ExecuteNonQuery();


                command = new SqlCommand("DELETE FROM [Country] WHERE [C_ID]=@C_ID", sqlConnection);
                command.Parameters.AddWithValue("C_ID", id);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM [Props] WHERE [C_ID]=@C_ID", sqlConnection);
                command.Parameters.AddWithValue("C_ID", id);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM [Customer] WHERE [C_ID]=@C_ID", sqlConnection);
                command.Parameters.AddWithValue("C_ID", id);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int DeleteAppNumber()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID рейса' должно быть заполнено!");
                return 1;
            }            

            sqlConnection.Open();

            

            try
            {
                command = new SqlCommand("DELETE FROM [AppNumber] WHERE [T_RouteID]=@T_RouteID", sqlConnection);
                command.Parameters.AddWithValue("T_RouteID", Convert.ToInt32(Qtextbox[0].Text));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int DeleteDriver()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID водителя' должно быть заполнено!");
                return 1;
            }
            

            sqlConnection.Open();

            try
            {
                int id = Convert.ToInt32(Qtextbox[0].Text);
                List<int> L = new List<int>();
                sqlReader = null;
                command = new SqlCommand("SELECT [T_RouteID] FROM [Trip] WHERE [T_DriverID] = @T_DriverID", sqlConnection);
                command.Parameters.AddWithValue("T_DriverID", id);
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    L.Add(Convert.ToInt32(sqlReader["T_RouteID"]));
                }
                if (sqlReader != null)
                    sqlReader.Close();

                L.Distinct();

                for (int i = 0; i < L.Count; i++)
                {
                    command = new SqlCommand("DELETE FROM [AppNumber] WHERE [T_RouteID] = @T_RouteID", sqlConnection);
                    command.Parameters.AddWithValue("T_RouteID", L[i]);
                    command.ExecuteNonQuery();

                    command = new SqlCommand("DELETE FROM [Trip] WHERE [T_RouteID] = @T_RouteID", sqlConnection);
                    command.Parameters.AddWithValue("T_RouteID", L[i]);
                    command.ExecuteNonQuery();
                }

                command = new SqlCommand("DELETE FROM [Driver] WHERE [D_ID]=@D_ID", sqlConnection);
                command.Parameters.AddWithValue("D_ID", id);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int DeleteCountry()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID заказчика' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            
            try
            {
                command = new SqlCommand("DELETE FROM [Country] WHERE [C_ID]=@C_ID", sqlConnection);
                command.Parameters.AddWithValue("C_ID", Convert.ToInt32(Qtextbox[0].Text));            
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int DeleteProduct()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID заявки' должно быть заполнено!");
                return 1;
            }

            
            sqlConnection.Open();

            try
            {
                int id = Convert.ToInt32(Qtextbox[0].Text);
                List<int> L = new List<int>();
                sqlReader = null;                
                    command = new SqlCommand("SELECT [T_RouteID] FROM [AppNumber] WHERE [T_AppNumber] = @T_AppNumber", sqlConnection);
                    command.Parameters.AddWithValue("T_AppNumber", id);
                    sqlReader = command.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        L.Add(Convert.ToInt32(sqlReader["T_RouteID"]));
                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                                    
                L.Distinct();

                for (int i = 0; i < L.Count; i++)
                {
                    command = new SqlCommand("DELETE FROM [AppNumber] WHERE [T_RouteID] = @T_RouteID", sqlConnection);
                    command.Parameters.AddWithValue("T_RouteID", L[i]);
                    command.ExecuteNonQuery();

                    command = new SqlCommand("DELETE FROM [Trip] WHERE [T_RouteID] = @T_RouteID", sqlConnection);
                    command.Parameters.AddWithValue("T_RouteID", L[i]);
                    command.ExecuteNonQuery();
                }

                command = new SqlCommand("DELETE FROM [Product] WHERE [P_AppNumber] = @P_AppNumber", sqlConnection);
                command.Parameters.AddWithValue("P_AppNumber", id);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int DeleteProps()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID заказчика' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            

            try
            {
                command = new SqlCommand("DELETE FROM [Props] WHERE [C_ID]=@C_ID", sqlConnection);
                command.Parameters.AddWithValue("C_ID", Convert.ToInt32(Qtextbox[0].Text));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int DeleteTrip()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID рейса' должно быть заполнено!");
                return 1;
            }

            sqlConnection.Open();

            try
            {
                command = new SqlCommand("DELETE FROM [AppNumber] WHERE [T_RouteID]=@T_RouteID", sqlConnection);
                command.Parameters.AddWithValue("T_RouteID", Qtextbox[0].Text);
                command.ExecuteNonQuery();


                command = new SqlCommand("DELETE FROM [Trip] WHERE [T_RouteID]=@T_RouteID", sqlConnection);
                command.Parameters.AddWithValue("T_RouteID", Qtextbox[0].Text);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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

        private int DeleteVan()
        {
            if (Qtextbox[0].Text == String.Empty)
            {
                MessageBox.Show("Поле 'ID водителя' должно быть заполнено!");
                return 1;
            }

            
            sqlConnection.Open();

            try
            {
                int id = Convert.ToInt32(Qtextbox[0].Text);
                List<int> L = new List<int>();
                sqlReader = null;
                command = new SqlCommand("SELECT [T_RouteID] FROM [Trip] WHERE [T_VanID] = @T_VanID", sqlConnection);
                command.Parameters.AddWithValue("T_VanID", id);
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    L.Add(Convert.ToInt32(sqlReader["T_RouteID"]));
                }
                if (sqlReader != null)
                    sqlReader.Close();

                L.Distinct();

                for (int i = 0; i < L.Count; i++)
                {
                    command = new SqlCommand("DELETE FROM [AppNumber] WHERE [T_RouteID] = @T_RouteID", sqlConnection);
                    command.Parameters.AddWithValue("T_RouteID", L[i]);
                    command.ExecuteNonQuery();

                    command = new SqlCommand("DELETE FROM [Trip] WHERE [T_RouteID] = @T_RouteID", sqlConnection);
                    command.Parameters.AddWithValue("T_RouteID", L[i]);
                    command.ExecuteNonQuery();
                }

                command = new SqlCommand("DELETE FROM [Van] WHERE [V_ID]=@V_ID", sqlConnection);
                command.Parameters.AddWithValue("V_ID", id);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Хрень");
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
                    Qlist = new List<string>() { "ID заказчика" };
                    Q_Create();
                    break;

                case (2):
                    Q_Delete();
                    Qlist = new List<string>() { "ID заказчика" };
                    Q_Create();
                    break;

                case (3):
                    Q_Delete();
                    Qlist = new List<string>() { "ID заказчика" };
                    Q_Create();
                    break;

                case (4):
                    Q_Delete();
                    Qlist = new List<string>() { "ID рейса" };
                    Q_Create();
                    break;

                case (5):
                    Q_Delete();
                    Qlist = new List<string>() { "ID рейса" };
                    Q_Create();
                    break;

                case (6):
                    Q_Delete();
                    Qlist = new List<string>() { "ID водителя" };
                    Q_Create();
                    break;

                case (7):
                    Q_Delete();
                    Qlist = new List<string>() { "ID машины" };
                    Q_Create();
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (Switch)
            {
                case (0):
                    DeleteCustomer();
                    break;

                case (1):
                    DeleteProps();
                    break;

                case (2):
                    DeleteCountry();
                    break;

                case (3):
                    DeleteProduct();
                    break;

                case (4):
                    DeleteTrip();
                    break;

                case (5):
                    DeleteAppNumber();
                    break;

                case (6):
                    DeleteDriver();
                    break;

                case (7):
                    DeleteVan();
                    break;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
