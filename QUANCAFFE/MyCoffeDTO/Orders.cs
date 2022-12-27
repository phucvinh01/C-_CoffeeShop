using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCoffee.Same;

namespace MyCoffee.MyCoffeeDAO
{
     public  class Orders
    {
        private static Orders instance;

        public static Orders Instance 
        { 
            get
            {
                if (instance == null)
                    instance = new Orders();
                return Orders.instance;
            }
            private set => instance = value; 
        }

        public void saveOrders(int Emp, int Cus, int totail, double discount, DateTime date)
        {
            string query = "SaveOrders  @CusID , @OrderDay , @totail , @discount , @EmpID";
            DataProvider.Instance.ExecutedNonQuery(query,new object[] {Cus,date,totail,discount,Emp});
        }
    }
}
