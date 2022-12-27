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

namespace QUANCAFFE.Components.LichSu
{
    public partial class LichSu : UserControl
    {
        public LichSu()
        {
            InitializeComponent();
            loadHistory();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void loadHistory()
        {
            string query = "select OrderDay as 'Thời gian' , Discount as 'Giảm giá' , Totail as 'Tổng' from Orders";
            DataGridViewHis.DataSource = DataProvider.Instance.ExecutedQuery(query);
        }

    }
}
