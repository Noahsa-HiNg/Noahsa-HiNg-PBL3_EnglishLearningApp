using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class CustomerAccess:DataBaseAccess
    {
        public string DeleteCus(Customer customer)
        {
            string result = DeleteDataCus(customer);
            return result;
        }
        public void LockCus(Customer customer)
        {
            LockDataCus(customer);
        }
    }
}
