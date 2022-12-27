using MyCoffee.MyCoffeDTO;
using MyCoffee.Same;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.MyCoffeeDAO
{
    public class ProductDAO
    {
        private static ProductDAO instance;

     
        public static ProductDAO Instance 
        {
            get
            {
                if (instance == null)
                    instance = new ProductDAO();
                return ProductDAO.instance;
             }
            set
            { 
                instance = value;
            }
        }

        private ProductDAO() { }

        public List<Product> LoadProductList()
        {
            List<Product> products = new List<Product>();

            DataTable data = DataProvider.Instance.ExecutedQuery("GetProductList");

            foreach(DataRow item in data.Rows)
            {
                Product product = new Product(item);
                products.Add(product);
            }

            return products;
        }

        public bool newPro(string pName, double pPrice, string pStatus, int pSup = 1)
        {
            string query = "newPro @pName ,  @pPrice , @pInStock , @pSup";
            int result = DataProvider.Instance.ExecutedNonQuery(query, new object[] {pName,pPrice,pStatus,pSup});
            return result > 0;
        }
        public bool updatePro(int pId, string pName, double pPrice, string pStatus, int sup = 1)
        {
            string query = "iUpdatePro @pId , @pName , @pPrice , @pStatus ,  @sup";
            int result = DataProvider.Instance.ExecutedNonQuery(query, new object[] { pId, pName, pPrice , pStatus , sup });
            return result > 0;
        }

        public bool deletePro(int pId)
        {
            string query = "iDelete @pId ";
            int result = DataProvider.Instance.ExecutedNonQuery(query, new object[] { pId });
            return result > 0;
        }


        public DataTable iFindProduct(string pFind)
        {
            string query = "iFindProduct @pFind";
            return DataProvider.Instance.ExecutedQuery(query, new object[] { pFind });
        }

    }
}
