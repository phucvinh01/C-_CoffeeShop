using MyCoffee.Same;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.MyCoffeeDAO
{
    public class EmployeesDAO
    {

        //single-ton
        private static EmployeesDAO instance;

        public static EmployeesDAO Instance
        {
            get
            {
                if (instance == null) instance = new EmployeesDAO();
                return EmployeesDAO.instance;
            }
            private set => instance = value;
        }


        // lấy id nhân viên qua tên
        public int getIdEmp(string pName)
        {
            string query = "getIdEmp @pName";
            int result = (int)DataProvider.Instance.ExecutedScalar(query, new object[] { pName });
            return result;
        }


        //thêm mới nhân viên
        public int newEmployee(string pName, DateTime pDay, string pAddress, string pPosition, string pPhone, string pGender)
        {
            string query = "newEmployee @pName , @pDay , @pAddress , @pPosition , @pPhone , @pGender";
            int result = DataProvider.Instance.ExecutedNonQuery(query, new object[] { pName, pDay, pAddress, pPosition, pPhone, pGender });
            return result;
        }


        //sửa nhân viên
        public bool updateEmp(int pId, string pName, DateTime pDay, string pAddress, string pPosition, string pPhone, string pGender)
        {
            string query = "iUpdate @pId , @pName , @pDay , @pAddr ,  @pPosition , @pPhone , @pGender";
            int result = DataProvider.Instance.ExecutedNonQuery(query, new object[] { pId, pName, pDay, pAddress, pPosition, pPhone, pGender });
            return result > 0;
        }

        //xóa nhân viên
        public bool idelEmp(int pId)
        {
            string query = "delEmp @pId";
            int result = DataProvider.Instance.ExecutedNonQuery(query, new object[] { pId });
            return result > 0;
        }

        // tìm nhân viên

        public DataTable iFindEmp(string pFind)
        {
            string query = "iFindEmp @pFind";
            return DataProvider.Instance.ExecutedQuery(query, new object[] { pFind });
        }
    }
}
