using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    public class PgSQL
    {
        public static NpgsqlConnection Connect(string port, string username, string password, string database)
        {
            string conString = $"Server=localhost;Database={database};Port={port};Username={username};Password={password}";
            NpgsqlConnection con = new NpgsqlConnection(conString);
            con.Open();
            con.Close();
            return con;
        }
        public static List<string[]> Reader(NpgsqlConnection con, string command)
        {
            List<string[]> res = new List<string[]>();
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand(command, con);
            NpgsqlDataReader reader = com.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //var a = reader.GetColumnSchema()[0].ColumnName;

                    string[] row = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader[i].ToString();
                    }
                    res.Add(row);
                }
            }
            con.Close();
            return res;
        }
    }
}
