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
using rent_a_car_manager.Models;

namespace rent_a_car_manager
{
    public partial class MainForm : Form
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
        MySqlConnection connection;
        public MainForm()
        {
            InitializeComponent();
            

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Form1 _form1 = new Form1;
            label10.Text = "your id: "+Form1.id.ToString();
            label2.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            try
            {
                connection = new MySqlConnection("server=remotemysql.com;port=3306;username=xN07x0Dcz2;password=2zqHoBUMbW;database=xN07x0Dcz2");
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    label3.ForeColor = Color.Green;
                    label3.Text = "Connected";
                }
                else
                {
                    label3.ForeColor = Color.Red;
                    label3.Text = "Not Connected";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            try
            {
                MySqlCommand sqlcmd = new MySqlCommand("select * from cars", connection);
                MySqlDataReader mysqlrd = sqlcmd.ExecuteReader();

                IList<CarModel> list = new List<CarModel>();

                while (mysqlrd.Read())
                {
                    string _id = mysqlrd["carid"].ToString();
                    string _name = mysqlrd["carname"].ToString();
                    string _price = mysqlrd["price"].ToString();

                    CarModel car = new CarModel(Int32.Parse(_id), _name, Int32.Parse(_price));
                    list.Add(car);
                    Console.WriteLine(car);
                    listBox1.Items.Add(car);
                    //selectedItem = _name + "\t" + _price;
                }
                connection.Close();
                /*
                allinfo[arrynum] = carname + "   " + price.ToString() + "\r\n";
                if (allinfo[arrynum] != "")
                {
                    listBox1.Items.Add(allinfo[arrynum]);
                }
                arrynum++;
                */
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            //--------------------incase some infos are missing------------------------start
            bool notFisrt = false;
            try 
            {
                connection.Open();
                MySqlCommand sqlcmd = new MySqlCommand("select * from info where userid=?userid" ,connection);
                sqlcmd.Parameters.AddWithValue("?userid", Form1.id);
                MySqlDataReader mysqlrd = sqlcmd.ExecuteReader();
                while (mysqlrd.Read()) 
                {
                    notFisrt = true;
                    age = Convert.ToInt32(mysqlrd["age"]);
                    salaryPerMonth = Convert.ToInt32(mysqlrd["salary"]);
                    savingsPerMonth = Convert.ToInt32(mysqlrd["taxes"]);
                    break;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (!notFisrt) 
            {
                panel2.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                button1.Enabled = false;
            }
            //--------------------incase some infos are missing------------------------end
        }
        //-----------------------------------------------start
        //--------TEST--------TEST--------TEST--------TEST--------TEST--------TEST
        int age;
        int salaryPerMonth ;
        int savingsPerMonth ;
        double p30;
        double p20;
        double p10;
        double firstpayment;
        bool plan30year = false;
        bool plan20year = false;
        bool plan10year = false;
        double percentage = 0.10;
        int carprice;
        string car;

        //--------TEST--------TEST--------TEST--------TEST--------TEST--------TEST
        //-----------------------------------------------end

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        CarModel selectedItem;
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                selectedItem = (CarModel)listBox1.SelectedItem;
                //Console.WriteLine(selectedItem.price);
                label2.Text = "you selected: " + selectedItem;
                //--------TEST--------TEST--------TEST--------TEST-------start
                label5.Text = "your first payment will be %10";
                listBox2.Items.Clear();
                listBox2.Visible = true;
                button2.Visible = true;
                label4.Visible = true;
                carprice = selectedItem.price;
                //getting info from database-----start

                //getting info from database-----start
                if (age < 65)
                {
                    //int all50Savings = ((age * 12) * savingsPerMonth)/2;
                    int savings50permonth = savingsPerMonth / 2;
                    int plan30years = 360 * savings50permonth;
                    int plan20years = 240 * savings50permonth;
                    int plan10years = 120 * savings50permonth;
                    if (carprice < plan10years)
                    {
                        plan10year = true;
                    }
                    if (carprice < plan20years)
                    {
                        plan20year = true;
                    }
                    if (carprice < plan30years)
                    {
                        plan30year = true;
                    }
                    //------percentage------
                    if (plan10year || plan20year)
                    {
                        firstpayment = percentage * carprice;
                        p10 = (carprice - firstpayment) / 119;
                        p20 = (carprice - firstpayment) / 239;
                        p30 = (carprice - firstpayment) / 359;
                    }
                }
                else { MessageBox.Show("you are too old to or buy a car"); }

                if (plan10year) { listBox2.Items.Add("10 years plan"); }
                if (plan20year) { listBox2.Items.Add("20 years plan"); }
                if (plan30year) { listBox2.Items.Add("30 years plan"); }
                label8.Text = "full price " + Convert.ToString(carprice) + " Euro";
                car = selectedItem.name;
                //--------TEST--------TEST--------TEST--------TEST-------start

            }
            else MessageBox.Show("please select a car first");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            label6.Text = "your first payment will be " + (0.1*carprice);
            if (listBox2.SelectedItem == "10 years plan") { label7.Text = "you will pay " + Convert.ToInt32(p10) + " monthly"; }
            if (listBox2.SelectedItem == "20 years plan") { label7.Text = "you will pay " + Convert.ToInt32(p20) + " monthly"; }
            if (listBox2.SelectedItem == "30 years plan") { label7.Text = "you will pay " + Convert.ToInt32(p30) + " monthly"; }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            connection.Open();
            MySqlCommand sqlcmd = new MySqlCommand("INSERT INTO orders (id , car , date) VALUES ( ?id , ?car , ?date)", connection);
            sqlcmd.Parameters.AddWithValue("?id", Form1.id);
            sqlcmd.Parameters.AddWithValue("?car", car);
            sqlcmd.Parameters.AddWithValue("?date", DateTime.Now);
            sqlcmd.ExecuteNonQuery();
            connection.Close();
            label9.Visible = true;
            button3.Visible = false;
            button2.Visible = false;
            button1.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label2.Visible = false;
            listBox1.Visible = false;
            listBox2.Visible = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //when some infos are missings
            //textBox1=age   textBox2=salary textBox3=savings
            if (textBox1.Text != null || textBox2.Text != null || textBox3.Text != null) 
            {
                connection.Open();
                MySqlCommand sqlcmd = new MySqlCommand("INSERT INTO info (userid , age , salary , taxes) VALUES ( ?userid , ?age , ?salary , ?taxes)", connection);
                sqlcmd.Parameters.AddWithValue("?userid", Form1.id);
                sqlcmd.Parameters.AddWithValue("?age", textBox1.Text);
                sqlcmd.Parameters.AddWithValue("?salary", textBox2.Text);
                sqlcmd.Parameters.AddWithValue("?taxes", textBox3.Text);
                sqlcmd.ExecuteNonQuery();
                connection.Close();
                button1.Enabled = true;
                panel2.Visible = false;
            }
            else { MessageBox.Show("please fill all the fields"); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            // source:https://stackoverflow.com/questions/463299/how-do-i-make-a-textbox-that-only-accepts-numbers
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
