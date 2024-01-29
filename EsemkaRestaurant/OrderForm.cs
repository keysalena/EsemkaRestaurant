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
    public partial class OrderForm : Form
    {
        EsemkaRestaurantEntities ent = new EsemkaRestaurantEntities();
        string id;
        public OrderForm(string id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            msmenuBindingSource.DataSource = ent.Msmenus.OrderByDescending(m => m.Name).ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (msmenuBindingSource.Current is Msmenu menu)
            {
                msmenuBindingSource1.DataSource = ent.Msmenus.FirstOrDefault(m => m.MenuID == menu.MenuID);
                pictureBox1.Image = Image.FromFile(menu.Photo);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Add();
            int rowIndex = dataGridView2.Rows.Count - 1;

            dataGridView2.Rows[rowIndex].Cells[0].Value = textBox1.Text;
            dataGridView2.Rows[rowIndex].Cells[1].Value = textBox2.Text;
            dataGridView2.Rows[rowIndex].Cells[2].Value = textBox3.Text;
            int qty = int.Parse(textBox2.Text);
            int price = int.Parse(textBox3.Text);
            dataGridView2.Rows[rowIndex].Cells[3].Value = qty * price;
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            var item = dataGridView1.Rows;
            var idOrder = ent.Headerorders.Select(p => p.OrderID).Max();
            var OrderId = $"{DateTime.Now.ToString("yyyyMM")}000{int.Parse(idOrder.Substring(6)) + 1}";

            foreach (DataGridViewRow row in item)
            {
                string menuName = row.Cells[0].Value?.ToString();
                var menuId = ent.Msmenus.FirstOrDefault(p => p.Name == menuName);
                Detailorder detail = new Detailorder {
                    OrderID = OrderId,
                    MenuID = menuId.MenuID,
                    Qty = int.Parse(row.Cells[1].Value.ToString()),
                    Price = int.Parse(row.Cells[2].Value.ToString()),
                    status = "processed",
                };
                ent.Detailorders.Add(detail);
                ent.SaveChanges();
            }

            Headerorder o = new Headerorder
            {
                MemberID = this.id,
                OrderID = OrderId,
            };
            ent.Headerorders.Add(o);
            ent.SaveChanges();
        }
    }
}