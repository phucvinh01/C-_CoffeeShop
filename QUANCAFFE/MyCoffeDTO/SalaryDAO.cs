using MyCoffee.Same;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffee.MyCoffeeDAO
{
    public class SalaryDAO
    {
        private static SalaryDAO instance;

        public static SalaryDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new SalaryDAO();
                return SalaryDAO.instance;

            }
            private set => instance = value;
        }

        public bool addLine(string pName, DateTime pDay, int pBegin , int pEnd, int pId)
        {
            string query = "insertLine @pName , @pDay , @pBegin , @pEnd , @pIdEmp";
            int result = DataProvider.Instance.ExecutedNonQuery(query, new object[] {pName , pDay, pBegin, pEnd , pId });
            return result > 0;
        }

        public int getIdLine(int pIdEMp, DateTime pDay)
        {
            string query = "getSalaryID @pIdEmp , @day ";
            int result = DataProvider.Instance.ExecutedNonQuery(query, new object[] { pIdEMp,pDay});
            return result;
        }
        public bool updateLine(int pid , string pName, DateTime pDay, int pEnd , int pBegin, int pIdEmp)
        {
            string query = "UpdateLine @pid , @pName , @pDay , @pEnd , @pBegin , @pIdEmp ";
            int result = DataProvider.Instance.ExecutedNonQuery(query, new object[] {pid , pName,pDay,pEnd,pBegin,pIdEmp });
            return result > 0;
        }

        public bool delLine(int pidEmp, DateTime pDay)
        { 
            string query = "delLine @pIdEmp , @pDay ";
            int result = DataProvider.Instance.ExecutedNonQuery(query, new object[] { pidEmp,pDay });
            return result > 0;
        }

        public DataTable getDayLine(DateTime pDay)
        {
            string query = "getDaySalary @pDay ";
            return DataProvider.Instance.ExecutedQuery(query, new object[] {  pDay });            
        }

        public DataTable getThongKeLuong(int pMonth)
        {
            string query = "getThongKeLuongThang @pMonth ";
            return DataProvider.Instance.ExecutedQuery(query, new object[] { pMonth });
        }

    }
}
