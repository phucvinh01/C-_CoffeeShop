using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.MyCoffeDTO
{
    public  class Product
    {
        private int iD;

        private string name;

        private string price;

        private string status;

        public static int ProductWith = 90 ;
        public static int ProductHeight = 90;
        private DataRowCollection rows;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public string Price { get => price; set => price = value; }
        public string Status { get => status; set => status = value; }

        public Product(int id, string name, string price, string status) {
            this.ID = id;
            this.Name = name;
            this.Price = price;
            this.Status = status;
        }

        public Product()
        { }

        public Product(DataRow row)
        {
            this.ID = (int)row["ProductID"];
            this.Name = row["ProductName"].ToString();
            this.Price = row["Price"].ToString();
            this.Status = row["InStock"].ToString();
        }

        public Product(DataRowCollection rows)
        {
            this.rows = rows;
        }
    }
}
