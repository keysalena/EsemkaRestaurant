using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsemkaRestaurant
{
    public partial class EmployeeForm : Form
    {
        EsemkaRestaurantEntities ent = new EsemkaRestaurantEntities();
        bool isCreate = true;
        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ComboBoxLoad()
        {
            positionComboBox.Items.Add("Admin");
            positionComboBox.Items.Add("Chef");
            positionComboBox.Items.Add("Cashier");
        }

        private void DisableLoad()
        {
            EmployeeLabel.Text = "EmployeeID";
            EmployeetextBox.UseSystemPasswordChar = false;
            nameTextBox.Enabled = false;
            positionComboBox.Enabled = false;
            emailTextBox.Enabled = false;
            EmployeetextBox.Enabled = false;    
            handphoneTextBox.Enabled = false;
        }   

        private void LoadData()
        {
            msemployeeBindingSource1.AddNew();
            positionComboBox.Text = null;
            positionComboBox.Items.Clear();
            ComboBoxLoad();
            EmployeetextBox.Clear();
            nameTextBox.Clear();
            emailTextBox.Clear();
            handphoneTextBox.Clear();
            DisableLoad();
            msemployeeBindingSource.DataSource = ent.Msemployees.ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (msemployeeBindingSource.Current is Msemployee emp)
            {
                EmployeetextBox.Text = emp.EmployeeID;
                msemployeeBindingSource1.DataSource = ent.Msemployees.FirstOrDefault(a => a.EmployeeID == emp.EmployeeID);
            }
        }

        private void InsertBtn_Click(object sender, EventArgs e)
        {
            EditBtn.Enabled = false;
            DeleteBtn.Enabled = false;
            msemployeeBindingSource1.AddNew();
            EmployeetextBox.Enabled = true;
            EnableLoad();
            EmployeetextBox.UseSystemPasswordChar = true;
            EmployeeLabel.Text = "Password";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (msemployeeBindingSource1.Current is Msemployee emp)
            {
                if (isCreate)
                {
                    string number = "0123456789";
                    Random random = new Random();
                    string randomPart = new string(Enumerable.Repeat(number, 4).Select(s => s[random.Next(s.Length)]).ToArray());
                    emp.EmployeeID = "E-" + randomPart;
                    emp.password = EmployeetextBox.Text;
                }
                ent.Msemployees.AddOrUpdate(emp);
                ent.SaveChanges();

                LoadData();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if(msemployeeBindingSource.Current is Msemployee emp)
            {
                DialogResult dialogResult = MessageBox.Show($"Yakin untuk menghapus {emp.Name}??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    ent.Msemployees.Remove(emp);
                    ent.SaveChanges();

                    LoadData();
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EnableLoad();
            isCreate = false;
            if(msemployeeBindingSource.Current is Msemployee emp)
            {
                EmployeetextBox.Text = emp.EmployeeID;
                msemployeeBindingSource1.DataSource = ent.Msemployees.AsNoTracking().FirstOrDefault(s => s.EmployeeID == emp.EmployeeID);
            }
        }

        private void EnableLoad()
        {
            nameTextBox.Enabled = true;
            emailTextBox.Enabled = true;
            positionComboBox.Enabled = true;
            handphoneTextBox.Enabled = true;
        }
    }
}