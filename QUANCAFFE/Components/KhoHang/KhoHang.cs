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

namespace QUANCAFFE.Components.KhoHang
{
    public partial class KhoHang : UserControl
    {
        BindingSource BindingSourcePro = new BindingSource();

        public KhoHang()
        {
            InitializeComponent();
            dataProduct.DataSource = BindingSourcePro;
            loadProducts();
            BingDingPro();
            dataProduct.ReadOnly = true;
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Bindding san pham
        /// </summary>
        private void BingDingPro()
        {
            idpro.DataBindings.Add(new Binding("Text", dataProduct.DataSource, "Mã Sản Phẩm", true, DataSourceUpdateMode.Never));
            txtProName.DataBindings.Add(new Binding("Text", dataProduct.DataSource, "Tên Sản Phẩm", true, DataSourceUpdateMode.Never));
            txtProPrice.DataBindings.Add(new Binding("Text", dataProduct.DataSource, "Giá", true, DataSourceUpdateMode.Never));
            cbProStatus.DataBindings.Add(new Binding("Text", dataProduct.DataSource, "Trạng thái", true, DataSourceUpdateMode.Never));
        }
        public void loadProducts()
        {
            string query = "select ProductID as 'Mã Sản Phẩm' , ProductName as 'Tên sản phẩm', Price as 'Giá' , InStock as 'Trạng thái' from Products";
            BindingSourcePro.DataSource = DataProvider.Instance.ExecutedQuery(query);
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            ThemMon tm = new ThemMon();
            tm.Show();
        }

        private void btnLuuMon_Click(object sender, EventArgs e)
        {
            loadProducts();
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
          
        }

        private void btnSuaMon_Click(object sender, EventArgs e)
        {
            int pId = int.Parse(idpro.Text);
            string pName = txtProName.Text;
            double pPrice = double.Parse(txtProPrice.Text);
            string pStatus = cbProStatus.SelectedItem.ToString();
            if (ProductDAO.Instance.updatePro(pId, pName, pPrice, pStatus))
            {
                MessageBox.Show("Update succsess!!");
                loadProducts();
            }
            else
            {
                MessageBox.Show("Opp!!Wronggg");
            }
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
