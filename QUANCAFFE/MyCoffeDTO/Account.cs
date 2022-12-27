using MyCoffee.Same;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.MyCoffeeDAO
{
    public class Account
    {
        //singleton

        private static Account instance;

        public static Account Instance
        {
            get
            {
                if (instance == null)
                    instance = new Account();
                return Account.instance;
            }
            private set
            {
                instance = value;
            }
        }

        private Account() { }


        public bool LogIn(string username, string password)
        {
            string query = "proLogIn @username , @password" ;
            DataTable r = DataProvider.Instance.ExecutedQuery(query,new object[] { username,password});
            return r.Rows.Count > 0;
        }

        public int getIDStaff(string username)
        {
            string query = "getIDStaff @username";
            int reuslt = (int)DataProvider.Instance.ExecutedScalar(query, new object[] { username});
            return reuslt;
        }

        public int getIDCus(string pName)
        {
            string query = "getCusID @cusName";
            int reuslt = (int)DataProvider.Instance.ExecutedScalar(query, new object[] { pName });
            return reuslt;
        }

        public int newAccount(int pId , string pUserName, string pPassword, int Per)
        {
            string query = "newAccount @pID , @pUserName , @pPassword , @pPer";
            int reslt = DataProvider.Instance.ExecutedNonQuery(query, new object[] { pId,pUserName,pPassword,Per});
            return reslt;
        }

        public int getPer(string pUsername)
        {
            string query = "getPerMisstion @pUsername ";
            int reslt = (int)DataProvider.Instance.ExecutedScalar(query, new object[] { pUsername });
            return reslt;
        }

    }
}   
