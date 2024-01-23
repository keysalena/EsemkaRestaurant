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
    public partial class MemberForm : Form
    {
        EsemkaRestaurantEntities entities = new EsemkaRestaurantEntities();
        bool isCreate = true;
        public MemberForm()
        {
            InitializeComponent();
        }

        private void MemberForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            msmemberBindingSource.AddNew();
            msmemberBindingSource1.DataSource = entities.Msmembers.ToList();
            DisableLoad();
        }

        private void DisableLoad()
        {
            nameTextBox.Enabled = false;
            button1.Enabled = false;
            emailTextBox.Enabled = false;
            handphoneTextBox.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(msmemberBindingSource1.Current is Msmember m)
            {
                msmemberBindingSource.DataSource = entities.Msmembers.FirstOrDefault(w => w.MemberID == m.MemberID);
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            nameTextBox.Enabled = true;
            button1.Enabled = true;
            emailTextBox.Enabled = true;
            handphoneTextBox.Enabled = true;
            isCreate = false;
            if (msmemberBindingSource1.Current is Msmember m)
            {
                msmemberBindingSource.DataSource = entities.Msmembers.AsNoTracking().FirstOrDefault(s => s.MemberID == m.MemberID);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (msmemberBindingSource1.Current is Msmember m)
            {
                DialogResult dialogResult = MessageBox.Show($"Yakin untuk menghapus {m.Name}??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    entities.Msmembers.Remove(m);
                    entities.SaveChanges();

                    LoadData();
                }
            }
        }

        private void InsertBtn_Click(object sender, EventArgs e)
        {
            nameTextBox.Enabled = true;
            button1.Enabled = true;
            emailTextBox.Enabled = true;
            handphoneTextBox.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(msmemberBindingSource.Current is Msmember m)
            {
                if(isCreate)
                {
                    string number = "0123456789";
                    Random random = new Random();
                    string randomPart = new string(Enumerable.Repeat(number, 6).Select(s => s[random.Next(s.Length)]).ToArray());
                    m.MemberID = "M-" + randomPart;
                    m.JoinDate = DateTime.Now;
                }
                entities.Msmembers.AddOrUpdate(m);
                entities.SaveChanges();
                LoadData();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Msmember m)
            {
                if (e.ColumnIndex == JoinDate.Index)
                {
                    e.Value = m.JoinDate.ToString("yyyy-MM-dd");
                }
            }
        }
    }
}