using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCoffee.Same;

namespace MyCoffee.MyCoffeeDAO
{
    class OrdersDetailDAO
    {

        private static OrdersDetailDAO instance;

        public static OrdersDetailDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new OrdersDetailDAO();
                return OrdersDetailDAO.instance;
            }
            private set => instance = value;
        }

        private OrdersDetailDAO() { }


        public int getOrderID(DateTime orderDay)
        {
            string query = "getOrderID @orderDay";
            int relust = (int)DataProvider.Instance.ExecutedScalar(query, new object[] { orderDay });
            return relust;
        }

        public void saveOrderDetails(int pOrderID, int pProductID, int pQuantity)
        {
            string qurey = "saveOrderDetails @OrderID , @ProductID , @Quantity";
            DataProvider.Instance.ExecutedNonQuery(qurey, new object[] {pOrderID,pProductID,pQuantity});
        }
    }
}
