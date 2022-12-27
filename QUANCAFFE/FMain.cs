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

namespace QUANCAFFE
{
    public partial class FMain : Form
    {

        public static string displayUserName;
        public static int pEmpIds;
        public FMain()
        {
            InitializeComponent();
            txtMatKhau.PasswordChar = '*';
            txtMatKhau.KeyDown += TxtMatKhau_KeyDown;

        }

        

        private void FMain_Load(object sender, EventArgs e)
        {
            MainForm.Hide();
            label2.Hide();
            label3.Hide();
            banHang1.Hide();
            nhanVien1.Hide();
            thongKe1.Hide();
            khoHang1.Hide();
            lichSu1.Hide();
            khachHang1.Hide();
            btnBanHang.Click += BtnBanHang_Click;
            btnLogin.Click += BtnLogin_Click;
            btnLogout.Click += BtnLogout_Click;
            btnNhanVien.Click += BtnNhanVien_Click;
            btnDoanhThu.Click += BtnDoanhThu_Click;
            btnCaiDat.Click += BtnCaiDat_Click;
            btnKhachHang.Click += BtnKhachHang_Click;
            btnKhoHang.Click += BtnKhoHang_Click;
        }

        private void BtnKhoHang_Click(object sender, EventArgs e)
        {
            label2.Hide();
            label3.Hide();
            banHang1.Hide();
            nhanVien1.Hide();
            thongKe1.Hide();
            khoHang1.Show();
            lichSu1.Hide();
            khachHang1.Hide();
        }

        private void BtnKhachHang_Click(object sender, EventArgs e)
        {
            label2.Hide();
            label3.Hide();
            banHang1.Hide();
            nhanVien1.Hide();
            thongKe1.Hide();
            khoHang1.Hide();
            lichSu1.Hide();
            khachHang1.Show();
        }

        private void BtnCaiDat_Click(object sender, EventArgs e)
        {
            label2.Hide();
            label3.Hide();
            banHang1.Hide();
            nhanVien1.Hide();
            thongKe1.Hide();
            khoHang1.Hide();
            lichSu1.Show();
            khachHang1.Hide();
        }

        private void BtnDoanhThu_Click(object sender, EventArgs e)
        {
            label2.Hide();
            label3.Hide();
            banHang1.Hide();
            nhanVien1.Hide();
            thongKe1.Show();
            khoHang1.Hide();
            lichSu1.Hide();
            khachHang1.Hide();
        }

        private void BtnNhanVien_Click(object sender, EventArgs e)
        {
            label2.Hide();
            label3.Hide();
            banHang1.Hide();
            nhanVien1.Show();
            thongKe1.Hide();
            khoHang1.Hide();
            lichSu1.Hide();
            khachHang1.Hide();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {

            MainForm.Hide();
            label2.Hide();
            label3.Hide();
            banHang1.Hide();
            nhanVien1.Hide();
            thongKe1.Hide();
            khoHang1.Hide();
            lichSu1.Hide();
            khachHang1.Hide();

            Application.Restart();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtTaiKhoan.Text;
            string password = txtMatKhau.Text;
            displayUserName = username;
            if (LogIn(username, password))
            {
                MainForm.Show();
                txtTenNhanVien.Text = displayUserName.ToString();
                pEmpIds = Account.Instance.getIDStaff(displayUserName);
                checkPerminsstion();

            }
            else
            {

                label3.Show();
                label3.Text = "Tài Khoản mật khẩu không đúng";

            }

        }
        private void TxtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
         
            if (e.KeyCode == Keys.Enter)
            {
                string username = txtTaiKhoan.Text;
                string password = txtMatKhau.Text;
                displayUserName = username;
                if (LogIn(username, password))
                {
                    MainForm.Show();
                    txtTenNhanVien.Text = displayUserName.ToString();
                    pEmpIds = Account.Instance.getIDStaff(displayUserName);
                    checkPerminsstion();
                }
                else
                {

                    label3.Show();
                    label3.Text = "Tài Khoản mật khẩu không đúng";
                }
            }
            
        }

        private void BtnBanHang_Click(object sender, EventArgs e)
        {
            label2.Hide();
            label3.Hide();
            banHang1.Show();
            nhanVien1.Hide();
            thongKe1.Hide();
            khoHang1.Hide();
            lichSu1.Hide();
            khachHang1.Hide();
            banHang1.clear();
            banHang1.getData(); 

        }

        public void checkPerminsstion()
        {
            string pUserName = txtTaiKhoan.Text;
            int r = Account.Instance.getPer(pUserName);
            if(r == -1)
            {
                btnCaiDat.Enabled = false;
                btnKhoHang.Enabled = false;
                btnNhanVien.Enabled = false;
                btnDoanhThu.Enabled = false;
            }
        }

        #region Method
        public bool LogIn(string username, string password)
        {
            return Account.Instance.LogIn(username, password);
        }
        #endregion
    }
}
