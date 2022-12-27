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

namespace QUANCAFFE.Components.KhachHang
{
    public partial class KhachHang : UserControl
    {
        public KhachHang()
        {
            InitializeComponent();
            loadCustomerList();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void loadCustomerList()
        {
            string query = "select *  from Custommers ";

            dataGridViewCustomer.DataSource = DataProvider.Instance.ExecutedQuery(query);
        }

        private void BtnSearchCus_Click(object sender, EventArgs e)
        {
            string pFind = txtTimKiem.Text;
            DataTable dataTable = CustommerDAO.Instance.iFindCus(pFind);
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Custommer" + pFind + "not esxit");
            }
            else
            {
                dataGridViewCustomer.DataSource = dataTable;
            }
        }
    }
}
