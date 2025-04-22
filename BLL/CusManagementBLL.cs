
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    class CusManagementBLL:CusValidatorBLL
    {
        CustomerAccess cusaccess = new CustomerAccess();
        AccountAccess acAccess = new AccountAccess();
        public string[] AddCustomer(Account account, Customer customer)
        {
            string[] ResultACC = new string[5];
            string[] information = { account.Username, account.Password, customer.Name, customer.Phone, customer.Email };
            MyDelegate[] methods = { ChecklogicUsername, CheckLogicPassWord, ChecklogicName, ChecklogicPhone, ChecklogicEmail };
            for (int i = 0; i < ResultACC.Length; i++)
            {
                ResultACC[i] = "valid_true";
            }
            for (int i = 0; i < information.Length; i++)
            {
                if (methods[i](information[i]) != "valid_true")
                {
                    ResultACC[i] = methods[i](information[i]);
                }
            }
            int flag = 1;
            for (int i = 0; i < ResultACC.Length; i++)
            {
                if (ResultACC[i] != "valid_true")
                {
                    flag = 0;
                    break;
                }
            }
            if (flag == 1)
            {
                acAccess.AddDataCustomer(customer, account);
            }
            return ResultACC;
        }
        public string DeleteCustomer(Customer customer)
        {
            string result = cusaccess.DeleteDataCus(customer);
            return result;
        }
        public void LockCus(Customer customer)
        {
            cusaccess.LockDataCus(customer);
        }
        public (Customer,Account) ShowInforCus(Customer customer)
        {
            return cusaccess.ShowDataInforCus(customer.ID);
        }
        public Customer[] ShowAllCustomer()
        {
            List<Customer> customers = new List<Customer>();
            customers = cusaccess.ShowAllDataCustomer();
            return customers.ToArray();
        }
        public void RestorCustomer(Customer customer)
        {
            cusaccess.RestoreDataCustomer(customer);
        }
        public Customer[] ShowDeleteCustomer()
        {
            List<Customer> customers = new List<Customer>();
            customers = cusaccess.ShowDataDeleteCustomer();
            return customers.ToArray();
        }
    }
}
