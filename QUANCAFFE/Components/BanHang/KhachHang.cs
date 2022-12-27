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

namespace QUANCAFFE.Components.BanHang
{
    public partial class KhachHang : Form
    {
        public KhachHang()
        {
            InitializeComponent();
            btnThemKhachHang.Enabled = false;
            txtTenKhachHang.TextChanged += TxtTenKhachHang_TextChanged;
            txtSDT.TextChanged += TxtSDT_TextChanged;
            txtSDT.KeyPress += TxtSDT_KeyPress;
        }

        private void TxtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void TxtSDT_TextChanged(object sender, EventArgs e)
        {
            if (txtTenKhachHang.Text.Length > 0 && txtSDT.Text.Length > 0)
                btnThemKhachHang.Enabled = true;
        }

        private void TxtTenKhachHang_TextChanged(object sender, EventArgs e)
        {
            if (txtTenKhachHang.Text.Length > 0 && txtSDT.Text.Length > 0)
                btnThemKhachHang.Enabled = true;
        }

        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            string pName = txtTenKhachHang.Text;
            string pPhone = txtSDT.Text;
            int pPoint = 0;
            if (CustommerDAO.Instance.checkPhone(pPhone) != 0)
            {
                MessageBox.Show("Have");
            }
            else if (CustommerDAO.Instance.addNew(pName, pPhone, pPoint) == 1)
            {
                MessageBox.Show("Success!\n" + txtTenKhachHang.Text + "\n" + txtSDT.Text);

                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
        }
    }
}
