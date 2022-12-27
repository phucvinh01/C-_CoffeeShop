using MyCoffee.MyCoffeeDAO;
using MyCoffee.Same;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QUANCAFFE.Components.NhanVien
{
    public partial class NhanVien : UserControl
    {
        int maxLength = 11;
        DateTime dateTime = DateTime.Today;
        BindingSource BindingSourceEmp = new BindingSource();
        public NhanVien()
        {
            InitializeComponent();
            loadEmpList();
            dataNhanVien.DataSource = BindingSourceEmp;
            dataNhanVien.SelectionChanged += DataNhanVien_SelectionChanged;
            btnChinhSuaNhanVien.Click += BtnChinhSuaNhanVien_Click;
            BingDingEmp();
            dataNhanVien.ReadOnly = true;

        }

        private void BtnChinhSuaNhanVien_Click(object sender, EventArgs e)
        {
            int pId = int.Parse(Id.Text);
            string pName = txtHoVaten.Text;
            DateTime pDay = txtNgaySinh.Value;
            string pAddr = txtDiaChi.Text;
            string pPhone = txtSDT.Text;
            string pPosition = comboboxRole.SelectedItem.ToString();
            string pGender = comboboxSex.SelectedItem.ToString();
            string message = "cập nhật thành công";
            string title = "Cập nhật";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon messageBoxIcon = MessageBoxIcon.Question;
            DialogResult result = MessageBox.Show(message, title, buttons, messageBoxIcon);

            if (EmployeesDAO.Instance.updateEmp(pId, pName, pDay, pAddr, pPosition, pPhone, pGender))
            {
                MessageBox.Show("Cập nhật thành công!");
                if (result == DialogResult.Yes)
                {
                    loadEmpList();
                }

            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }


        }
        private void btnXoaNhanVien_Click(object sender, EventArgs e)
        {
            int pId = int.Parse(Id.Text);
            string message = "Bạn chắc chắn muốn xóa";
            string title = "Xóa nhân viên";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon messageBoxIcon = MessageBoxIcon.Question;
            DialogResult result = MessageBox.Show(message, title, buttons, messageBoxIcon);
            if (result == DialogResult.Yes)
            {
                if (EmployeesDAO.Instance.idelEmp(pId))
                {
                    MessageBox.Show("Xóa thành công!");
                    
                }
            }
            else
            {
                this.Hide();
            }
            loadEmpList();
        }

        private void DataNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            btnXoaNhanVien.Enabled = true;
            btnChinhSuaNhanVien.Enabled = true;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
       

        //Thêm nhân viên
        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            ThemNhanVien nv = new ThemNhanVien();
            nv.Show();
            
        }
        //Load Nhân viên
        public void loadEmpList()
        {
            string query = "select *  from Employees";

            BindingSourceEmp.DataSource = DataProvider.Instance.ExecutedQuery(query);
        }
        //Bindding Nhân viên
        private void BingDingEmp()
        {
            Id.DataBindings.Add(new Binding("Text", dataNhanVien.DataSource, "EmployeeID", true, DataSourceUpdateMode.Never));
            txtHoVaten.DataBindings.Add(new Binding("Text", dataNhanVien.DataSource, "FullName", true, DataSourceUpdateMode.Never));
            txtSDT.DataBindings.Add(new Binding("Text", dataNhanVien.DataSource, "NumberPhone", true, DataSourceUpdateMode.Never));
            txtNgaySinh.DataBindings.Add(new Binding("Value", dataNhanVien.DataSource, "BirthOfDay", true, DataSourceUpdateMode.Never));
            txtDiaChi.DataBindings.Add(new Binding("Text", dataNhanVien.DataSource, "Address", true, DataSourceUpdateMode.Never));
            comboboxSex.DataBindings.Add(new Binding("Text", dataNhanVien.DataSource, "Gender", true, DataSourceUpdateMode.Never));
            comboboxRole.DataBindings.Add(new Binding("Text", dataNhanVien.DataSource, "Position", true, DataSourceUpdateMode.Never));
        }
        
        private void btnLuuNhanVien_Click(object sender, EventArgs e)
        {
            loadEmpList();
        }

        private void BtnSearchEmp_Click(object sender, EventArgs e)
        {
            string pFind = txtTimKiem.Text;
            DataTable dataTable = EmployeesDAO.Instance.iFindEmp(pFind);
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Employee" + pFind + "not esxit");
            }
            else
            {
                BindingSourceEmp.DataSource = dataTable;
            }
        }
    }
}
