using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    // lớp chứa các hàm kiểm tra thông tin tài khoản
    public class UserDataValidatorBLL
    {
        AccountAccess acAccess = new AccountAccess();
        public string CheckLogicPassWord(string Password)
        {
            if (Password == "")
            {
                return "NULL_Password";
            }

            // Kiểm tra độ dài mật khẩu (ít nhất 8 ký tự)
            if (Password.Length < 8)
            {
                return "WEAK_Password_Length";
            }
            // Kiểm tra ký tự đầu tiên có phải là chữ cái in hoa không
            if (!char.IsUpper(Password[0]))
            {
                return "WEAK_Password_FirstUpper";
            }
            // Kiểm tra xem mật khẩu có chứa ít nhất một số không
            if (!Password.Any(char.IsDigit))
            {
                return "WEAK_Password_Number";
            }
            // Kiểm tra xem mật khẩu có chứa ít nhất một ký tự đặc biệt không
            if (!Password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return "WEAK_Password_SpecialChar";
            }
            return "Valid_Password";
        }
        public string ChecklogicUsername(string username)
        {
            if (username == "")
            {
                return "NULL_Username";
            }
            // Kiểm tra độ dài tên người dùng (ít nhất 6 ký tự)
            if (username.Length < 6)
            {
                return "WEAK_Username_Length";
            }
            // Kiểm tra xem tên người dùng có chứa khoảng trắng không
            if (username.Contains(" "))
            {
                return "WEAK_Username_Space";
            }
            return "valid_true";
        }
        public string ChecklogicName(string name)
        {
            if (name == "")
            {
                return "NULL_Name";
            }
            // Kiểm tra xem tên có chứa ký tự số không
            if (name.Any(char.IsDigit))
            {
                return "WEAK_Name_Number";
            }
            // Kiểm tra xem tên có chứa ký tự đặc biệt không
            if (name.Any(ch => !char.IsLetter(ch) && ch != ' '))
            {
                return "WEAK_Name_SpecialChar";
            }
            return "valid_true";
        }
        public string ChecklogicPhone(string phone)
        {
            if (phone == "")
            {
                return "NULL_Phone";
            }
            if (phone.Length != 10)
            {
                return "WEAK_Phone_Length";
            }
            return "valid_true";
        }
        public string ChecklogicEmail(string email)
        {
            if (email == "")
            {
                return "NULL_Email";
            }
            // Kiểm tra định dạng email
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return "WEAK_Email_Format";
            }
            //kiểm tra xem email có tồn tại trong database không
            if (acAccess.CheckEmail(email) == true)
            {
                return "WEAK_Email_Exist";
            }
            return "valid_true";
        }
    }
}



