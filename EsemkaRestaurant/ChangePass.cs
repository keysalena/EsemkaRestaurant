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
    public partial class ChangePass : Form
    {
        EsemkaRestaurantEntities entities = new EsemkaRestaurantEntities();
        public ChangePass()
        {
            InitializeComponent();
        }

        private void ChangePass_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var old = entities.Msemployees.FirstOrDefault(a => a.password == textBox1.Text);

            if (old != null && textBox2.Text == textBox3.Text)
            {
                Msemployee emp = entities.Msemployees.FirstOrDefault();
                emp.password = textBox2.Text;
                entities.SaveChanges();
                MessageBox.Show("Password berhasil diubah");
                this.Close();
            }
            else
            {
                MessageBox.Show("Data yang anda masukkan salah");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
               textBox1.UseSystemPasswordChar = false;
               textBox2.UseSystemPasswordChar = false;
               textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
                textBox2.UseSystemPasswordChar = true;
                textBox3.UseSystemPasswordChar = true;
            }
        }
    }
}