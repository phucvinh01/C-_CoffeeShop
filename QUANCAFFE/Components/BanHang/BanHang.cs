
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MyCoffee.MyCoffeDTO;
using MyCoffee.MyCoffeeDAO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QUANCAFFE.Components.BanHang
{
    public partial class BanHang : UserControl
    {
        double sum;
        double totail;
        int pProductID;
        int pQuantity;
        List<OrderDetail> listOrderDetails = new List<OrderDetail>();
        bool clicked = false;
       
        public void clear()
        {
            flowPro.Controls.Clear();
        }
        public BanHang()
        {
            InitializeComponent();
            btnXoaMonDaChon.Enabled = false;
            btnThanhToan.Enabled = false;
            btnXoaTatCa.Enabled = false;
            btnSuDungDiem.Enabled = false;
            btnTimKiem.Click += BtnTimKiem_Click;
            txtTenKhachHang.TextChange += TxtTenKhachHang_TextChange;
            txtTenKhachHang.Text = "No MememberShip";
            textSearch.KeyPress += TextSearch_KeyPress;
 
        }

        private void TextSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void TxtTenKhachHang_TextChange(object sender, EventArgs e)
        {
            btnSuDungDiem.Enabled = true;
        }

        private void NumberDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            totail = double.Parse(NumberDiscount.SelectedItem.ToString()) * totail;
            txtTotail.Text = totail.ToString("c");
        }
        private void txtTenKhachHang_TextChange_1(object sender, EventArgs e)
        {
            btnSuDungDiem.Enabled = true;
        }

        public void delOrdersExist(int pProId, int pQuantity)
        {
            OrderDetail orderDetail = new OrderDetail(pProId, pQuantity);
            listOrderDetails.Remove(orderDetail);
        }



        int pIdPro;
        OrderDetail orderDetail = new OrderDetail();

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            List<Custommers> custommers = CustommerDAO.Instance.FindCustommer(textSearch.Text);
            foreach (Custommers custommers1 in custommers)
            {
                txtTenKhachHang.Text = custommers1.Name;
                txtDiem.Text = custommers1.Point.ToString();
            }
        }

        private void btnQuayLaiBanHang_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void getData()
        {
            List<Product> products = ProductDAO.Instance.LoadProductList();

            foreach (Product product in products)
            {
                if (product.Status == "Còn Hàng")
                {
                    Button button = new Button() { Width = Product.ProductWith, Height = Product.ProductHeight, BackColor = Color.Brown, ForeColor = Color.Black };
                    //if (product.Name.Contains("Cà Phê"))
                    //{
                    //    button.BackColor = Color.Brown;
                    //}
                    //else if (product.Name.Contains("Bạc"))
                    //{
                    //    button.BackColor = Color.RosyBrown;
                    //}
                    //else if (product.Name.Contains("Macchiato"))
                    //{
                    //    button.BackColor = Color.Azure;
                    //}
                    //else if (product.Name.Contains("Latte"))
                    //{
                    //    button.BackColor = Color.Indigo;
                    //}
                    //else if (product.Name.Contains("Americano"))
                    //{
                    //    button.BackColor = Color.Aqua;
                    //}
                    //else if (product.Name.Contains("Cappucino"))
                    //{
                    //    button.BackColor = Color.Chocolate;
                    //}
                    //else if (product.Name.Contains("Espresso"))
                    //{
                    //    button.BackColor = Color.DarkOrange;
                    //}
                    //else if (product.Name.Contains("Cold Brew"))
                    //{
                    //    button.BackColor = Color.DarkBlue;
                    //}
                    //else
                    //{
                    //    button.BackColor = Color.DarkRed;
                    //}
                    flowPro.Controls.Add(button);
                    button.Text = product.Name;
                    button.Click += Button_Click;
                    button.Tag = product;
                }
                if(product.Status == "Hết Hàng")
                {
                    Button button = new Button() { Width = Product.ProductWith, Height = Product.ProductHeight, BackColor = Color.Gray, ForeColor = Color.Black };
                    flowPro.Controls.Add(button);
                    button.Text = product.Name;
                    button.Enabled = true;
                }

            }

        }

        private void Button_Click(object sender, EventArgs e)
        {
            //Lấy các giá trị trong button thông qua Tag
            string proID = ((sender as Button).Tag as Product).ID.ToString();

            pProductID = ((sender as Button).Tag as Product).ID;

            string proName = ((sender as Button).Tag as Product).Name;

            string proPrice = ((sender as Button).Tag as Product).Price.ToString();


            // Kiểm tra sản phẩm tồn tại trong listview hay chưa
            // Nếu chưa thì thêm sản phẩm vào
            // Nếu có thì cập nhật số lượng +1
            if (checkEixstProInLv(proName, pProductID, pQuantity))
            {

                for (int i = 0; i < listViewBill.Items.Count; i++)
                {
                    int sub = int.Parse(listViewBill.Items[i].SubItems[2].Text);
                    if (listViewBill.Items[i].SubItems[0].Text == proName)
                    {
                        //Xóa ordersdetail
                        int index = listOrderDetails.IndexOf(orderDetail);
                        listOrderDetails.RemoveAt(index);
                        //Cập nhật số lượng
                        //thêm lại vào orderdetail
                        listViewBill.Items[i].SubItems[2].Text = (sub + 1).ToString();
                        pQuantity = int.Parse(listViewBill.Items[i].SubItems[2].Text.ToString());
                        orderDetail = new OrderDetail(pProductID, pQuantity);
                        listOrderDetails.Add(orderDetail);
                    }
                }
            }
            else
            {
                ListViewItem list = new ListViewItem(proName);
                list.SubItems.Add(proPrice);
                list.SubItems.Add(txtSoLuong.Value.ToString());
                pQuantity = (int)txtSoLuong.Value;
                listViewBill.Items.Add(list);
                orderDetail = new OrderDetail(pProductID, pQuantity);
                listOrderDetails.Add(orderDetail);
            }


            //tổng tiền = số lượng * đơn giá
            sum = double.Parse(proPrice) * (double)txtSoLuong.Value;

            totail += sum;

            txtTotail.Text = totail.ToString("c") + " VND";


            //sau khi click 1 sản phẩm thì các nút thanh toán, xóa, resert bill đc chọn
            btnThanhToan.Enabled = true;
            btnXoaTatCa.Enabled = true;
            btnXoaMonDaChon.Enabled = true;
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            List<Custommers> custommers = CustommerDAO.Instance.FindCustommer(textSearch.Text);
            foreach (Custommers custommers1 in custommers)
            {
                txtTenKhachHang.Text = custommers1.Name;
                txtDiem.Text = custommers1.Point.ToString();
            }
        }

        private void btnSuDungDiem_Click(object sender, EventArgs e)
        {
            clicked = true;
            totail -= double.Parse(txtDiem.Text);
            txtTotail.Text = totail.ToString() + ".000 VND";
        }
        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang();
            kh.Show();
        }

        private void btnXoaTatCa_Click(object sender, EventArgs e)
        {
            listViewBill.Items.Clear();
            txtTotail.Clear();
        }

        private void btnXoaMonDaChon_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in listViewBill.SelectedItems)
            {
                int pProduct, pQuanity;
                pProduct = pProductID;
                pQuanity = int.Parse(eachItem.SubItems[2].Text);
                listViewBill.Items.Remove(eachItem);
                OrderDetail orderDetail = new OrderDetail(pProductID, pQuanity);
                listOrderDetails.Remove(orderDetail);

                //trừ totail cho mổi sản phẩm được xóa
                totail -= double.Parse(eachItem.SubItems[1].Text) * double.Parse(eachItem.SubItems[2].Text);
                txtTotail.Text = totail.ToString("c") + " VND";
            }
        }
        public bool checkEixstProInLv(string pProName, int pProID, int pQuantity)
        {
            for (int i = 0; i < listViewBill.Items.Count; i++)
            {
                if (listViewBill.Items[i].SubItems[0].Text == pProName)
                {
                    return true;
                }

            }
            return false;
        }

        public void checkSubMit()
        {
            if (listViewBill.Items.Count < 0)
                btnThanhToan.Enabled = true;
        }

        public void checkbtnClear()
        {
            if (listViewBill.Items.Count < 0)
                btnXoaTatCa.Enabled = true;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            //displayname để lấy thông tin nhân viên bán hàng, qua tài khoản đăng nhập ở form đăng nhập
            //today để lấy ngày tháng thời gian order
            //Cus tên khách hàng 
            //totail là tổng tiền thanh toán
            string displayname = FMain.displayUserName;
            DateTime today = DateTime.Now;

            string Cus = txtTenKhachHang.Text;
            string totaill = txtTotail.Text;
            MessageBox.Show("PayMent!!\n" + "Nhân Viên: " + displayname + "\n" + "Ngày lập hóa đơn: " + today + "\n" + "Khách hàng: " + Cus
                + "\nTotail: " + totaill + "\n");

            //clear listview cho đơn order tiếp theo
            listViewBill.Items.Clear();
           

            //pEmpId = id nhân viên được lấy qua tài khoản đăng nhập
            //pCusId = id khách hàng được lấy qua tên khách hàng
            //pDiscounting giảm giá tổng tiền qua % được chọn từ combox discount
            //pDiscount tổng số tiền được giảm giá từ % giảm giá và điểm tích lũy của khách hàng
            int pEmpId = FMain.pEmpIds;
            int pCusId = Account.Instance.getIDCus(txtTenKhachHang.Text);
            double pDiscounting = double.Parse(NumberDiscount.SelectedItem.ToString()) * totail;
            double pDiscount = pDiscounting + double.Parse(txtDiem.Text);
            totail -= pDiscount;



            int pSum = (int)totail;
            string pPhone = textSearch.Text;
            double pPoint = (double)pSum * 0.1;


            //lưu thông tin Orders
            Orders.Instance.saveOrders(pEmpId, pCusId, pSum, pDiscount, today);
            CustommerDAO.Instance.updatePoints(pPhone, pPoint);

            //Nếu button sử dụng điểm tích lũy được click thì update điểm tích lũy khách hàng về 0
            //Bug -------- Còn TH khách hàng chọn sử dụng bao nhiêu điểm lũy 
            if (clicked == true)
                CustommerDAO.Instance.updatePointsZero(pPhone, 0);


            totail = 0;

          
            // save thông tin chi tiết hóa đơn
            //  lấy id hóa đơn qua thời gian mà đơn đc order
            int pOrderID = OrdersDetailDAO.Instance.getOrderID(today);
            foreach (OrderDetail orderDetail in listOrderDetails)
            {
                OrdersDetailDAO.Instance.saveOrderDetails(pOrderID, orderDetail.ProductID, orderDetail.Quantity);
            }

            listOrderDetails.Clear();
            btnThanhToan.Enabled = false;
            btnXoaTatCa.Enabled = false;
            btnSuDungDiem.Enabled = false;
            textSearch.Clear();
            txtDiem.Clear();
            txtTenKhachHang.Text = "No MememberShip";
            txtDiem.Text = "0";
            clicked = false;
            txtTotail.Clear();
           
            //Khách hàng không là thành viên thì tên mặc định là No MememberShip
        }
    }
}
