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
    public partial class CashierForm : Form
    {
        EsemkaRestaurantEntities entities = new EsemkaRestaurantEntities();
        string id;

        public CashierForm(string id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangePass cp = new ChangePass();
            cp.Show();
        }

        private void CashierForm_Load(object sender, EventArgs e)
        {
            var name = entities.Msemployees.FirstOrDefault(r => r.EmployeeID == this.id);
            label2.Text = $"Welcome, {name.Name}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PaymentForm payment = new PaymentForm();
            payment.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login login= new Login();
            login.Show();
        }
    }
}