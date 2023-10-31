using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Курсовая
{
    public partial class Form1 : Form
    {
		static NpgsqlConnection con = authorization.con;
        List<string[]> tables = PgSQL.Reader(con, "select table_name from information_schema.tables where table_schema = \'public\' and table_type = \'BASE TABLE\';");
        List<string[]> columns = new List<string[]>();
        List<string[]> data = new List<string[]>();
        int i = 0;

        public Form1()
        {
            InitializeComponent();
            table_initialize();
        }

        private void table_initialize()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 1;

            for (int i = 0; i < tables.Count; i++)
            {
                dataGridView1.Rows.Add(tables[i]);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            textBox1.Text = "";
            if (i == 0)
            {
                this.Height += 143;
                textBox1.Show();
                i++;
            }
            else if (i == 1)
            {
                this.Height -= 143;
                textBox1.Hide();
                i--;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' && i == 1)
            {
                string console_command = textBox1.Text.ToString();
                textBox1.Text = "";

                dataGridView2.Columns.Clear();
                dataGridView2.Rows.Clear();

                try
                {
                    data = PgSQL.Reader(con, console_command);
                    dataGridView2.ColumnCount = data[0].Length;
                    for (int i = 0; i < data.Count; i++)
                    {
                        dataGridView2.Rows.Add(data[i]);
                    }
                }
                catch (Exception ex)
                {
                    textBox1.Text += $"\n {ex.Message}";
                    con.Close();
                }
            }
        }

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			dataGridView2.Columns.Clear();
			dataGridView2.Rows.Clear();

			columns = PgSQL.Reader(con, $"select column_name from information_schema.columns where table_name = \'{tables[e.RowIndex][0]}\' order by ordinal_position;");
			data = PgSQL.Reader(con, $"select * from {tables[e.RowIndex][0]} order by {columns[0][0]};");

			try
			{
				for (int i = 0; i < data[0].Length; i++)
				{
					dataGridView2.Columns.Add(i.ToString(), columns[i][0]);
				}

				for (int i = 0; i < data.Count; i++)
				{
					dataGridView2.Rows.Add(data[i]);
				}
			}
			catch
			{
				MessageBox.Show($"В таблице {tables[e.RowIndex][0]} нет значений");
			}
		}
	}
}
