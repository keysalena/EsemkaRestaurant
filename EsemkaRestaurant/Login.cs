using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsemkaRestaurant
{
    public partial class Login : Form
    {
        EsemkaRestaurantEntities entities = new EsemkaRestaurantEntities();
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            textBox1.Text = "keysa@gmail.com";
            textBox2.Text = "123";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var check = entities.Msemployees.FirstOrDefault(r => r.Email == textBox1.Text && r.password == textBox2.Text);
            string id = check.EmployeeID;
            if (check != null)
            {
                if(check.Position == "Admin")
                {
                    AdminForm form = new AdminForm(id);
                    form.Show();
                    this.Hide();
                }
                else if(check.Position == "Chef")
                {
                    ChefForm form = new ChefForm();
                    form.Show();
                    this.Hide();
                }
                else if (check.Position == "Cashier")
                {
                    CashierForm form = new CashierForm();
                    form.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Password or Email is not valid");
            }
        }
    }
}
