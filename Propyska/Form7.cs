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
using Propyska.Domain;

namespace Propyska
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            this.passesTableAdapter.Fill(this.propyskaDataSet.Passes);

        }

        private void button1_Click(object sender, EventArgs e)
        {
                int passID = int.Parse(comboBox1.Text);
                DateTime time = DateTime.Now;
                string typeofentering = "Вход";

                AddEnter(passID, time, typeofentering);

                Passes passes = Check.GetUser(passID);

                if (passes.PassID != 0)
                {
                    this.Close();
                }
            }

            static void AddEnter(int passID, DateTime time, string typeofentering)
            {
                string connectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\AppData\Propyska.mdf;
            Integrated Security=True";

            Passes passes = Check.GetUser(passID);

            if (passes.PassID != 0)
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "INSERT INTO [dbo].[Entering] ([PassID], [TypeofEntering], [Time]) VALUES (@PassID, @typeofentering, @time)", con))
                    {
                        command.Parameters.Add(new SqlParameter("passId", passID));
                        command.Parameters.Add(new SqlParameter("typeofentering", typeofentering));
                        command.Parameters.Add(new SqlParameter("time", time));

                        command.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }

            else
            {
                MessageBox.Show("Введен неверный ID");
            }
            }
    }
    }
