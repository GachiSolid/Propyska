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
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.MinDate = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Пожалуйста, заполните все поля");
                }
                else
                {
                    int personID = int.Parse(textBox1.Text);
                    string surname = textBox3.Text;
                    string name = textBox4.Text;
                    string patronymic = textBox5.Text;
                    string type = "Долгосрочный";
                    DateTime date = dateTimePicker1.Value;
                    AddPerson(personID, surname, name, patronymic, type, date);
                    this.Close();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Данный ID уже существует");
            }

            catch (FormatException)
            {
                MessageBox.Show("ID должен быть целочисленным");
            }

            catch (OverflowException)
            {
                MessageBox.Show("Данное значение ID недопустимо");
            }
        }

        static void AddPerson (int personID, string surname, string name, string patronymic, string type, DateTime date)
        {
            string connectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\AppData\Propyska.mdf;
            Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand(
                "INSERT INTO [dbo].[Persons] VALUES (@personID, @surname, @name, @patronymic)", con))
                {
                    command.Parameters.Add(new SqlParameter("personID", personID));
                    command.Parameters.Add(new SqlParameter("surname", surname));
                    command.Parameters.Add(new SqlParameter("name", name));
                    command.Parameters.Add(new SqlParameter("patronymic", patronymic));

                    command.ExecuteNonQuery();
                }

                using (SqlCommand command1 = new SqlCommand(
                "INSERT INTO [dbo].[Passes] ([PersonID], [Type], [Date]) VALUES (@personID, @type, @date)", con))
                {
                    command1.Parameters.Add(new SqlParameter("personID", personID));
                    command1.Parameters.Add(new SqlParameter("type", type));
                    command1.Parameters.Add(new SqlParameter("date", date));

                    command1.ExecuteNonQuery();
                }

                con.Close();
            }
        }
    }
}
