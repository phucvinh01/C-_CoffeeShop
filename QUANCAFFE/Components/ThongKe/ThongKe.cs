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

namespace QUANCAFFE.Components.ThongKe
{
    public partial class ThongKe : UserControl
    {
        DateTime dateTime = DateTime.Today;
        BindingSource BindingSourceLine = new BindingSource();
        public ThongKe()
        {
            InitializeComponent();
            Load += ThongKe_Load;
            btnSearchLine.Click += BtnSearchLine_Click;
            bunifuButton1.Click += BunifuButton1_Click;
            btnThongKe.Click += BtnThongKe_Click;
           
        }

        private void BtnThongKe_Click(object sender, EventArgs e)
        {
            rpDay rpDay = new rpDay();
            ReportDoanhThuNgay reportDoanhThuNgay = new ReportDoanhThuNgay();
            reportDoanhThuNgay.Show();

        }

        private void BunifuButton1_Click(object sender, EventArgs e)
        {
            BillReport billReport = new BillReport();
            rpBill rpBill = new rpBill();
            rpBill.Show();
        }

        

        private void BtnSearchLine_Click(object sender, EventArgs e)
        {
            DateTime pDay = dateTimeWork.Value;
            BindingSourceLine.DataSource = SalaryDAO.Instance.getDayLine(pDay);
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            dateTimeWork.Value = dateTime;
            loadThongKe();
            loadThongKeSP();
            ThongKeDanhThu.Show();
            loadThongKeLuong();
            ThongKeBangLuong.Hide();
            XepLineNhanVien.Hide();
            gridviewEmpLine.DataSource = BindingSourceLine;
            loadEmp(dateTime);
            BingDingLine();
        }

        public void loadThongKeLuong()
        {
            DateTime dateTime = DateTime.Now;
            int pMonth = dateTime.Month;
            dataGridViewThongKeLuong.DataSource = SalaryDAO.Instance.getThongKeLuong(pMonth);
        }
        private void btnQuayLai_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void loadThongKe()
        {
            string query = "select * from view_PrDaily";
            DataTable dataTable = DataProvider.Instance.ExecutedQuery(query);
            dataGridViewDoanhThu.DataSource = dataTable;
        }
        public void loadThongKeSP()
        {
            string query ="select Amount, ProductName , Sales from view_Max , (select MAX(Amount) as maxlt from view_Max) as lt where lt.maxlt = Amount";
            DataTable dataTable = DataProvider.Instance.ExecutedQuery(query);
            GridViewSPBanChay.DataSource = dataTable;
        }
        public void loadComboBoxEmp()
        {
            string query1 = "select FullName from Employees";
            DataTable dataTable1 = DataProvider.Instance.ExecutedQuery(query1);
            txtNameEmpLine.DataSource = dataTable1;
            txtNameEmpLine.DisplayMember = "FullName";
            txtNameEmpLine.ValueMember = "FullName";
            gridviewEmpLine.AllowUserToAddRows = true;
        }
        public void loadEmp(DateTime dateTime)
        {
            gridviewEmpLine.ReadOnly = true;
            string query = "select IdEmp as 'Mã', EmpName as 'Tên', Position as 'Vị Trí', NameLine as 'Line', TimeBegin as 'Bắt Đầu', TimeEnd as 'Kết Thúc' from getListLineToDay( @daytime )";
            BindingSourceLine.DataSource = DataProvider.Instance.ExecutedQuery(query, new object[] { dateTime });
            loadComboBoxEmp();

        }
        public void BingDingLine()
        {
            txtNameEmpLine.DataBindings.Add(new Binding("Text", gridviewEmpLine.DataSource, "Tên", true, DataSourceUpdateMode.Never));
            txtTimeBegin.DataBindings.Add(new Binding("Text", gridviewEmpLine.DataSource, "Bắt đầu", true, DataSourceUpdateMode.Never));
            txtTimeEnd.DataBindings.Add(new Binding("Text", gridviewEmpLine.DataSource, "Kết Thúc", true, DataSourceUpdateMode.Never));
            cbNameLine.DataBindings.Add(new Binding("Text", gridviewEmpLine.DataSource, "Line", true, DataSourceUpdateMode.Never));
        }
        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            ThongKeDanhThu.Show();
            ThongKeBangLuong.Hide();
            XepLineNhanVien.Hide();
        }

        private void btnBangLuong_Click(object sender, EventArgs e)
        {
            ThongKeDanhThu.Hide();
            ThongKeBangLuong.Show();
            XepLineNhanVien.Hide();
        }

        private void btnXepLine_Click(object sender, EventArgs e)
        {
            ThongKeDanhThu.Hide();
            ThongKeBangLuong.Hide();
            XepLineNhanVien.Show();
        }

        private void btnThemLine_Click(object sender, EventArgs e)
        {
            string pName = txtNameEmpLine.SelectedValue.ToString();
            int pid = EmployeesDAO.Instance.getIdEmp(pName);
            int pBegin = int.Parse(txtTimeBegin.Text);
            int pEnd = int.Parse(txtTimeEnd.Text);
            string pLine = cbNameLine.SelectedItem.ToString();
            DateTime pDay = dateTimeWork.Value;
            if (SalaryDAO.Instance.addLine(pLine, pDay, pBegin, pEnd, pid))
            {
                MessageBox.Show("Succsess!!");
            }
            else
            {
                MessageBox.Show("Wrongg!!");

            }
            loadEmp(dateTime);
        }

        private void btnXoaLine_Click(object sender, EventArgs e)
        {
            string pNameEmp = txtNameEmpLine.SelectedValue.ToString();
            string pName = cbNameLine.SelectedItem.ToString();
            DateTime pDay = dateTimeWork.Value;
            int pidEmp = EmployeesDAO.Instance.getIdEmp(pNameEmp);
            int pid = SalaryDAO.Instance.getIdLine(pidEmp, pDay);
            int pEnd = int.Parse(txtTimeEnd.Text);
            int pBegin = int.Parse(txtTimeBegin.Text);

            if (SalaryDAO.Instance.delLine(pidEmp, pDay))
            {
                MessageBox.Show("Suses!!");
            }
            else
            {
                MessageBox.Show("Worong");
            }
            loadEmp(dateTime);
        }

        private void btnSuaLine_Click(object sender, EventArgs e)
        {
            string pNameEmp = txtNameEmpLine.SelectedValue.ToString();
            string pName = cbNameLine.SelectedItem.ToString();
            DateTime pDay = dateTimeWork.Value;
            int pidEmp = EmployeesDAO.Instance.getIdEmp(pNameEmp);
            int pid = SalaryDAO.Instance.getIdLine(pidEmp, pDay);
            int pEnd = int.Parse(txtTimeEnd.Text);
            int pBegin = int.Parse(txtTimeBegin.Text);

            if (SalaryDAO.Instance.updateLine(pid, pName, pDay, pEnd, pBegin, pidEmp))
            {
                MessageBox.Show("Suses!!");
            }
            else
            {
                MessageBox.Show("Worong");
            }
            loadEmp(dateTime);
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
