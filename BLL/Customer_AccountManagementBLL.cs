using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL
{
    class Customer_AccountManagementBLL : User_AccountManagementBLL
    {
        public string SignUpACC (Account account)
        {
            string ResultACC = "";
            if(CheckLogicPassWord(account.Password) == "Valid_Password")
            {
                // Thêm Hàm Insert vô database 
                ResultACC = "Success_SignUpAccount";
            }
            else
            {
                ResultACC = CheckLogicPassWord(account.Password);
            }
            return ResultACC;
        }
        public string SignUpCus (Customer customer)
        {
            string ResultCus = "";
            if (CheckInfoAccount(customer) == "Valid")
            {
                // Thêm hàm insert vô database
                ResultCus = "Sucess_SignUpCus";
            }
            else
            {
                ResultCus = CheckInfoAccount(customer);
            }

            return ResultCus;

        }
    }
}
