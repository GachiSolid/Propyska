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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string connectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\AppData\Propyska.mdf;
            Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Select [dbo].[Passes].[PassID], [dbo].[Persons].[Surname], [dbo].[Persons].[Name], [dbo].[Persons].[Patronymic], [dbo].[Entering].[TypeofEntering], [dbo].[Entering].[Time], [dbo].[Persons].[PersonID], [dbo].[Entering].[EnterID] from [dbo].[Passes], [dbo].[Persons], [dbo].[Entering] where ([dbo].[Passes].[PersonID]=[dbo].[Persons].[PersonID]) AND ([dbo].[Entering].[PassID] = [dbo].[Passes].[PassID]) ORDER BY [dbo].[Entering].[Time]";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[8]);
                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                    data[data.Count - 1][4] = reader[4].ToString();
                    data[data.Count - 1][5] = reader[5].ToString();
                    data[data.Count - 1][6] = reader[6].ToString();
                    data[data.Count - 1][7] = reader[7].ToString();
                }
                reader.Close();
                con.Close();
                foreach (string[] s in data)
                    dataGridView1.Rows.Add(s);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
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

                        int enterid = int.Parse(row1.Cells[7].Value.ToString());

                        string query = "DELETE from [dbo].[Entering] WHERE EnterID=" + enterid;
                        SqlCommand command = new SqlCommand(query, con);

                        int reader = command.ExecuteNonQuery();

                    }
                }
                con.Close();
            }
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
                DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите очистить весь журнал?", "Удаление", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row1 in dataGridView1.Rows)
                    {
                        dataGridView1.Rows.Clear();

                        string query = "DELETE from [dbo].[Entering]";
                        SqlCommand command = new SqlCommand(query, con);

                        int reader = command.ExecuteNonQuery();

                        con.Close();
                    }

                }
            }
        }
    }
}