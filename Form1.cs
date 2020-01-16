using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace rent_a_car_manager
{
    public partial class Form1 : Form
    {
        //-----------------------------------------------------
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }
        //https://stackoverflow.com/questions/23966253/moving-form-without-title-bar
        //-----------------------------------------------------
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection connection;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connection = new MySqlConnection("server=remotemysql.com;port=3306;username=xN07x0Dcz2;password=2zqHoBUMbW;database=xN07x0Dcz2");
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    label10.ForeColor = Color.Green;
                    label10.Text = "Connected";
                }
                else
                {
                    label10.ForeColor = Color.Red;
                    label10.Text = "Not Connected";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //username
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //password
        }
        public static string username;
        public static string id;
        MainForm mainform = new MainForm();
        private void button1_Click(object sender, EventArgs e)
        {
            //login
            username = textBox1.Text;
            string password = textBox2.Text;
            if (username.Length != 0 || password.Length != 0)
            {
                MySqlCommand sqlcmd = new MySqlCommand("select * from users where username=@username AND PASSWORD=@password", connection);
                sqlcmd.Parameters.AddWithValue("@username", username);
                sqlcmd.Parameters.AddWithValue("@password", password);
                MySqlDataReader mysqlrd = sqlcmd.ExecuteReader();
                //id = mysqlrd[id].ToString();
                
                bool access = false;
                while (mysqlrd.Read())
                {
                    access = true;
                    id = mysqlrd["id"].ToString();
                    break;
                }
                if (access)
                {
                    //show MainForm
                    this.Hide();
                    mainform.ShowDialog();
                    


                }
                else { MessageBox.Show("wrong username/password"); }
                mysqlrd.Close();
            }
            else
                MessageBox.Show("ERROR");

            /*
            MySqlCommand sqlid = new MySqlCommand("SELECT `id` FROM `users` WHERE `username` = @username ", connection);
            sqlid.Parameters.AddWithValue("@username", username);
            MySqlDataReader sqlidreader = sqlid.ExecuteReader();
            while (sqlidreader.Read())
            {
                id = (long)(int)sqlidreader["id"];
            }
            sqlidreader.Close();
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //register
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //new username
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //new password
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //confirm new password
        }

        private void label10_Click(object sender, EventArgs e)
        {
            //connecting...
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }
        public static string newusername;
        private void button3_Click(object sender, EventArgs e)
        {
            newusername = textBox6.Text;
            string newpassword = textBox5.Text;
            string confirmpassword = textBox4.Text;
            if (newusername != null && newpassword != null) 
            {
                if (newpassword == confirmpassword)
                {
                    MySqlCommand sqlcmd = new MySqlCommand("INSERT INTO users (username , PASSWORD) VALUES ( ?newusername , ?newpassword)", connection);
                    sqlcmd.Parameters.AddWithValue("?newusername", newusername);
                    sqlcmd.Parameters.AddWithValue("?newpassword", newpassword);
                    sqlcmd.ExecuteNonQuery();
                    id = sqlcmd.LastInsertedId.ToString();
                    connection.Close();
                    this.Hide();
                    mainform.ShowDialog();
                }
                else { MessageBox.Show("both passwords doesn't match"); }
            }
            else { MessageBox.Show("type new username and password"); }
            
        }
    }
}
