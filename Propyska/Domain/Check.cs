using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Propyska.Domain
{
    public static class Check
    {
        static string connectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\AppData\Propyska.mdf;
            Integrated Security=True";

        public static Passes GetUser(int passID)
        {
            Passes passes = new Passes();

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand command2 = new SqlCommand(
                String.Format("SELECT * FROM [dbo].[Passes] pass WHERE pass.PassID = '{0}'", passID),
                conn);

            SqlDataReader reader = command2.ExecuteReader();
            while (reader.Read())
            {
                passes.PassID = reader.GetInt32(0);
            }

            conn.Close();
            return passes;
        }

        public static Persons GetID(int personID)
        {
            Persons persons = new Persons();

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand command2 = new SqlCommand(
                String.Format("SELECT * FROM [dbo].[Persons] pass WHERE pass.PersonID = '{0}'", personID),
                conn);

            SqlDataReader reader = command2.ExecuteReader();
            while (reader.Read())
            {
                persons.PersonID = reader.GetInt32(0);
            }

            conn.Close();
            return persons;
        }
    }
}
