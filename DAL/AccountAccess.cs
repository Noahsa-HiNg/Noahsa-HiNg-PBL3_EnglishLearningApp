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
        public string ChangeName(Person person, string newname)
        {
            string result = ChangeDataName(person, newname);
            return result;
        }
        public string ChangePhone(Person person, string newphone)
        {
            string result = ChangeDataPhone(person, newphone);
            return result;
        }
        // tuấn anh mới thêm
        public int Get_quantily_Account()
        {
            int quantily = Get_quantily_Account_DATA();
            return quantily;
        }
        //
    }
}
