using MyCoffee.MyCoffeeDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANCAFFE.Components.KhoHang
{
    public partial class ThemMon : Form
    {
        public ThemMon()
        {
            InitializeComponent();
            btnAddMon.Enabled = false;
        }

        private void txtProPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtProName_TextChange(object sender, EventArgs e)
        {
            if (txtProName.Text.Length > 0 && txtProPrice.Text.Length > 0)
                btnAddMon.Enabled = true;
        }

        private void txtProPrice_TextChange(object sender, EventArgs e)
        {
            if (txtProName.Text.Length > 0 && txtProPrice.Text.Length > 0)
                btnAddMon.Enabled = true;
        }

        private void btnAddMon_Click(object sender, EventArgs e)
        {
            string pName = txtProName.Text;
            double pPrice = double.Parse(txtProPrice.Text);
            string pStatus = cbProStatus.SelectedItem.ToString();
            if (ProductDAO.Instance.newPro(pName, pPrice, pStatus))
            {
                MessageBox.Show("Thêm món thành công");
                KhoHang f = new KhoHang();             
                this.Close();
                f.loadProducts();

            }
            else
            {
                MessageBox.Show("Có lỗi!");
            }
        }
    }
}
