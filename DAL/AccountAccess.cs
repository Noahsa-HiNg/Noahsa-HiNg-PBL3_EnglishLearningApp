﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class AccountAccess : DataBaseAccess
    {
        public string CheckLogin(Account account)
        {
            string infor = CheckLoginData(account);
            return infor;
        }
        public bool CheckEmail(string Gmail)
        {
            if (CheckEmailData(Gmail) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string ChangePassword(Person person, string newpassword)
        {
            string result = ChangeDataPassword(person,newpassword);
            return result;
        }
    }
}
