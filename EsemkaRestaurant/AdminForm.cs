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
    public partial class AdminForm : Form
    {
        EsemkaRestaurantEntities entities = new EsemkaRestaurantEntities();
        string id;
        public AdminForm(string id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmployeeForm employeeForm = new EmployeeForm();
            employeeForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm();
            menuForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MemberForm memberForm = new MemberForm();
            memberForm.Show();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            var name = entities.Msemployees.FirstOrDefault(r => r.EmployeeID == this.id);
            label2.Text = $"Welcome, {name.Name}";
        }
    }
}
