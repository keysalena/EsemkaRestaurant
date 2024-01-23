using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace EsemkaRestaurant
{
    public partial class MenuForm : Form
    {
        private string fileName = null;
        bool isCreate = true;
        EsemkaRestaurantEntities entities = new EsemkaRestaurantEntities();
        public MenuForm()
        {
            InitializeComponent();
        }

        private void InsertBtn_Click(object sender, EventArgs e)
        {
            msmenuBindingSource1.AddNew();
            EnableLoad();
        }

        private void EnableLoad()
        {
            button3.Enabled = true; button4.Enabled = true;
            nameTextBox.Enabled = true;
            priceNumericUpDown.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog= new OpenFileDialog();
            fileDialog.Filter = "Filter Gambar|*.png;*.jpeg;*.jpg";
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = fileDialog.FileName;
                pictureBox1.Image = Image.FromFile(fileDialog.FileName);
                photoTextBox.Text = fileDialog.FileName;
            }
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            msmenuBindingSource.DataSource = entities.Msmenus.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (msmenuBindingSource1.Current is Msmenu menu)
            {
                if (isCreate)
                {
                    if (fileName != null)
                    {
                        DirectoryInfo projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
                        string imageDir = Path.Combine(projectDir.FullName, "Images");

                        string destination = Path.Combine(imageDir, Path.GetFileName(fileName));
                        File.Copy(fileName, destination, true);
                        menu.Photo = destination;
                    }
                }
                entities.Msmenus.AddOrUpdate(menu);
                entities.SaveChanges();

                LoadData();
                DisableLoad();
            }
        }

        private void DisableLoad()
        {
            button3.Enabled = false; button4.Enabled = false;
            nameTextBox.Enabled = false;
            priceNumericUpDown.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (msmenuBindingSource.Current is Msmenu menu)
            {
                pictureBox1.Image = Image.FromFile(menu.Photo);
                msmenuBindingSource1.DataSource = entities.Msmenus.FirstOrDefault(p => p.MenuID == menu.MenuID);
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EnableLoad();
            isCreate = false;

            if(msmenuBindingSource.Current is Msmenu menu)
            {
                pictureBox1.Image = Image.FromFile(menu.Photo);
                msmenuBindingSource1.DataSource = entities.Msmenus.FirstOrDefault(p => p.MenuID == menu.MenuID);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (msmenuBindingSource.Current is Msmenu menu)
            {
                DialogResult dialogResult = MessageBox.Show($"Yakin untuk menghapus {menu.Name}??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    entities.Msmenus.Remove(menu);
                    entities.SaveChanges();

                    LoadData();
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Msmenu menu)
            {
                if(e.ColumnIndex == Price.Index)
                {
                    int price = (int)menu.Price;
                    e.Value = price.ToString("N0");
                }
            }
        }
    }
}