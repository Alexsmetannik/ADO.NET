using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CSLesson8
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-90SJ7B7\SQLEXPRESS;Initial Catalog=CSLessons;Integrated Security=True");
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT
                                        me.ID, me.Amount, me.EntryDate, me.Description, cat.Name
                                 FROM
                                        MoneyEntries me
                                        LEFT JOIN Category cat
                                        ON cat.ID = me.Category";

            conn.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(string.Format("{0}:{1} от {2}; {3}; {4}",
                        reader.GetInt32(0).ToString(),
                        reader.GetFloat(1).ToString(),
                        reader.GetDateTime(2).ToShortDateString(),
                        reader.IsDBNull(4) ? "<нет категории>" : reader.GetString(4),
                        reader.IsDBNull(3) ? "<нет категории>" : reader.GetString(3)));
                }
            }

            conn.Close();

            Console.ReadKey();
        }
    }
}
