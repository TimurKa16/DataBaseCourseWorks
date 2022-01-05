using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace БД
{
    public partial class Form3 : Form
    {
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

        public Form3(SqlConnection sqlConnection, SqlCommand command, SqlDataReader sqlReader)
        {
            this.sqlConnection = sqlConnection;
            this.sqlReader = sqlReader;
            this.command = command;
            InitializeComponent();
            tabPage4.AutoScroll = true;
            tabPage5.AutoScroll = true;
            tabPage6.AutoScroll = true;
            tabPage8.AutoScroll = true;
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            tb = new TextBox[8][][];
            Tab4();
            Tab5();
            Tab6();
            Tab8();
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

        private void button11_Click(object sender, EventArgs e)
        {
            Tab8();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}
