using System;
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
        public bool CheckGmail(string Gmail)
        {
            if (CheckGmailData(Gmail) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
