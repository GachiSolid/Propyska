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

namespace Propyska
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void Refresh()
        {
            string connectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\AppData\Propyska.mdf;
            Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "Select [dbo].[Passes].[PassID], [dbo].[Passes].[Type], [dbo].[Passes].[Date], [dbo].[Persons].[PersonID], [dbo].[Persons].[Surname], [dbo].[Persons].[Name], [dbo].[Persons].[Patronymic] from [dbo].[Passes], [dbo].[Persons] where ([dbo].[Passes].[PersonID]=[dbo].[Persons].[PersonID]) ORDER BY [dbo].[Passes].[Date]";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[7]);
                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                    data[data.Count - 1][4] = reader[4].ToString();
                    data[data.Count - 1][5] = reader[5].ToString();
                    data[data.Count - 1][6] = reader[6].ToString();
                }
                reader.Close();
                con.Close();
                foreach (string[] s in data)
                    dataGridView1.Rows.Add(s);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\AppData\Propyska.mdf;
            Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row1 in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows.Remove(row1);
                        int passid = int.Parse(row1.Cells[0].Value.ToString());
                        int personid = int.Parse(row1.Cells[3].Value.ToString());

                        string query = "DELETE from [dbo].[Entering] WHERE PassID=" + passid;
                        string query1 = "DELETE from [dbo].[Passes] WHERE PassID=" + passid;
                        string query2 = "DELETE from [dbo].[Persons] WHERE PersonID=" + personid;

                        SqlCommand command = new SqlCommand(query, con);
                        SqlCommand command1 = new SqlCommand(query1, con);
                        SqlCommand command2 = new SqlCommand(query2, con);
                        int reader = command.ExecuteNonQuery();
                        int reader1 = command1.ExecuteNonQuery();
                        int reader2 = command2.ExecuteNonQuery();

                    }
                }
                con.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Close();
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            label9.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }
    }
}
