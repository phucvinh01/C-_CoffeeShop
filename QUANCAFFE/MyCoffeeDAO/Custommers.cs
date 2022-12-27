using MyCoffee.Same;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.MyCoffeDTO
{
    public class Custommers
    {
        private int iD;

        private string name;

        private string point;

        private string phone;

        private DataRowCollection rows;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public string Point { get => point; set => point = value; }
        public string Phone { get => phone; set => phone = value; }

        public Custommers() { }
        public Custommers(int pid, string pName, string pPoint, string pPhone)
        {
            this.ID = pid;
            this.Name = pName;
            this.Phone = pPhone;
            this.Point = pPoint;

        }

        public Custommers(DataRow row)
        {
            this.ID = (int)row["CustommerID"];
            this.Name = row["FullName"].ToString();
            this.Phone = row["PhoneNumber"].ToString();
            this.Point = row["Points"].ToString();
        }
        public Custommers(DataRowCollection rows)
        {
            this.rows = rows;
        }

        

    }
   

}
