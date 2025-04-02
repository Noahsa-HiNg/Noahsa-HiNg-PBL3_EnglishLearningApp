using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DTO;

namespace BLL
{
    class User_AccountManagementBLL
    {
        public string CheckLogicAccount(Account Account)
        {
            string ID = "";
            if (Account.Username == "")
            {
                return "NULL_Username";
            }
            if (Account.Password == "")
            {
                return "NULL_Password";
            }

            // Kiểm tra độ dài mật khẩu (ít nhất 8 ký tự)
            if (Account.Password.Length < 8)
            {
                return "WEAK_Password_Length";
            }
            // Kiểm tra ký tự đầu tiên có phải là chữ cái in hoa không
            if (!char.IsUpper(Account.Password[0]))
            {
                return "WEAK_Password_FirstUpper";
            }
            // Kiểm tra xem mật khẩu có chứa ít nhất một số không
            if (!Account.Password.Any(char.IsDigit))
            {
                return "WEAK_Password_Number";
            }
            // Kiểm tra xem mật khẩu có chứa ít nhất một ký tự đặc biệt không
            if (!Account.Password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return "WEAK_Password_SpecialChar";
            }
            return ID;
        }
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
        public bool CheckLogicEmail(string Email)
        {
            bool CheckEmail = false;
            // Thêm Hàm Check Email ở DAL
            return CheckEmail;
        }
        public string ChangePassWord(Account account, string NewPassWord)
        {
            string Result = "";
            // Thêm hàm check có đúng mậth khẩu cũ không 
            string CheckPassword = CheckLogicPassWord(NewPassWord);
            if (CheckPassword == "Valid_Password")
            {
                account.Password = NewPassWord;
                // Thêm Hàm Change Password vô database
                Result = "Change_Success";
            }
            else
            {
                return CheckPassword;
            }
            return Result;
        }
        public string CheckInfoAccount(Person person)
        {
            // Kiểm tra nhập vào là số 
            foreach (char c in person.Phone)
            {
                if (!char.IsDigit(c))
                    return "Only_Digits_Allowed";
            }
            // Kiểm tra 10 số 
            if (person.Phone.Length != 10)
            {
                return "Must_10_Digits";
            }
            // Kiểm Tra k có space 
            if (string.IsNullOrWhiteSpace(person.Email))
                return "Email_Required";
            // Kiểm tra có @ có . sau @
            if (!Regex.IsMatch(person.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return "Invalid_Email_Format";

            return "Valid";
        }
        string ChangeInfor(Person person)
        {
            string Result = "";
            if(CheckInfoAccount(person) == "Valid")
            {
                // Thêm hàm update vô database 
                Result = "Success_ChangeInfor";
            }
            else
            {
                CheckInfoAccount(person);
            }
                return Result;
        }
    }
    }

