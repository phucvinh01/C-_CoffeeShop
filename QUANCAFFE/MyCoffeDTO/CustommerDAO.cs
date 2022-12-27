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
    public class CustommerDAO
    {
        private static CustommerDAO instance;

        public static CustommerDAO Instance {
            get
            {
                if(instance == null) instance = new CustommerDAO() ;
                return CustommerDAO.instance;
            } 
            
            private set => instance = value; }

        private CustommerDAO() { }


        //thêm khách hàng
        public int addNew(string pName, string pPhone, int pPoint = 0)
        {
            string query = "AddNewCus @pName , @pPhone , @pPoint";
            int result;
            result = DataProvider.Instance.ExecutedNonQuery(query, new object[] {pName,pPhone,pPoint});
            return result;
        }


        //kiểm tra số điện thoại khi thêm khách hàng
        public int checkPhone(string pPhone)
        {
            string query = "checkPhone @pPhone";
            int result;
            result = (int)DataProvider.Instance.ExecutedScalar(query, new object[] { pPhone });
            return result;
        }


        //update điểm tích lũy sau mổi lần mua hàng
        public int updatePoints(string pPhone, double pPoints)
        {
            string query = "updatePoints @pPhone , @pPoint";
            int result;
            result = (int)DataProvider.Instance.ExecutedNonQuery(query, new object[] { pPhone , pPoints });
            return result;
        }

        //update điểm tích lũy thành 0 khi khách hàng sử dụng điểm tích lũy để mua hàng
        public int updatePointsZero(string pPhone, double pPoints = 0)
        {
            string query = "updatePointsZero @pPhone , @pPoint";
            int result;
            result = (int)DataProvider.Instance.ExecutedNonQuery(query, new object[] { pPhone, pPoints });
            return result;
        }


        // tìm khách hàng trên màng hình bán hàng ==> kết quả tìm cho ra: tên và điểm tích lũy
        public List<Custommers> FindCustommer(string pPhone)
        {
            List<Custommers> custommer = new List<Custommers>();

            string query = "findCustommers @pPhone";

            DataTable data = DataProvider.Instance.ExecutedQuery(query,new object[] {pPhone});

            foreach (DataRow item in data.Rows)
            {
                Custommers custommers = new Custommers(item);
                custommer.Add(custommers);
            }

            return custommer;
        }

        // tìm khách hàng trong from manager ==> kểt quả là một datatable
        public DataTable iFindCus(string pFind)
        {
            string query = "iFindCus @pFind";
            return DataProvider.Instance.ExecutedQuery(query, new object[] { pFind });
        }

    }
}
