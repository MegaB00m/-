using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
	public partial class authorization : Form
	{
		string username = "postgres", password, port = "5433", database = "postres";
		public static Npgsql.NpgsqlConnection con;

		public authorization()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (!(textBox1.Text == "")) username = textBox1.Text;
			if (!(textBox2.Text == "")) password = textBox2.Text;
			if (!(textBox3.Text == "")) port = textBox3.Text;
			if (!(textBox4.Text == "")) database = textBox4.Text;

			try
			{
				con = PgSQL.Connect(port, username, password, database);
				MessageBox.Show("Connection was successful");
				this.Hide();
				Form1 form = new Form1();
				form.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
