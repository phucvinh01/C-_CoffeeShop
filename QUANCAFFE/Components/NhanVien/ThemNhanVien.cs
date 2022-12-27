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

namespace QUANCAFFE.Components.NhanVien
{
    public partial class ThemNhanVien : Form
    {
        int maxLength = 11;
        DateTime dateTime = DateTime.Today;
        public ThemNhanVien()
        {
            InitializeComponent();
        }
        private bool Validation()
        {
            if (txtSDT.Text.Length != 11)
            {
                MessageBox.Show("Số điện thoại phải có 11 kí tự", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            return true;
        }
        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            string pName = txtHoVaten.Text;
            string pPhone = txtSDT.Text;
            string pAdd = txtDiaChi.Text;
            DateTime pDay = txtNgaySinh.Value;
            string pPositon = comboboxRole.SelectedItem.ToString();
            string pGender = comboboxSex.SelectedItem.ToString();
            string pUserName = txtTaiKhoan.Text;
            string pPassword = txtMatKhau.Text;
            if (pName != string.Empty || pPhone != string.Empty || pAdd != string.Empty || pPositon != string.Empty || pGender != string.Empty || pUserName != string.Empty || pPassword != string.Empty)
            {
                if (!Validation())
                {
                    if (EmployeesDAO.Instance.newEmployee(pName, pDay, pAdd, pPositon, pPhone, pGender) == 1)
                    {
                        MessageBox.Show("Susses!!\n" + pName + "\n" + pPhone + "\n" + pAdd + "\n" + pPositon + "\n" + pGender + "\n" + pPhone);
                    }

                    int pId = EmployeesDAO.Instance.getIdEmp(pName);


                    int Permission = -1;
                    if (comboboxRole.SelectedItem.ToString() == "Manager")
                    {
                        Permission = 1;
                    }

                    if (Account.Instance.newAccount(pId, pUserName, pPassword, Permission) == 1)
                    {
                        MessageBox.Show("Tạo tài khoản thành công ");
                        
                    }
                    else
                    {
                        MessageBox.Show("Tạo tài khoản thất bại");
                    }
                }
                else
                {
                    MessageBox.Show("Không được để trống ô điền");
                }
            }
        }
    }
}
