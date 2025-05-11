using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using DTO;
using DAL;

namespace BLL
{

    public delegate string MyDelegate(string str);
    // lớp kiểm tra chức năng đăng nhập,đăng ký & quên mật khẩu
    public class User_AccountManagementBLL : UserDataValidatorBLL
    {
        private AccountAccess _accountAccess;
        private CustomerAccess _customerAccess;
        private CourseAccess _courseAccess;
        private LessonAccess _lessonAccess;

        public User_AccountManagementBLL()
        {
            _accountAccess = new AccountAccess();
            _customerAccess = new CustomerAccess();
            _courseAccess = new CourseAccess();
            _lessonAccess = new LessonAccess();
        }

        // Constructor with injected dependencies for testing/flexibility
        public User_AccountManagementBLL(
            AccountAccess accountAccess,
            CustomerAccess customerAccess,
            CourseAccess courseAccess,
            LessonAccess lessonAccess)
        {
            _accountAccess = accountAccess;
            _customerAccess = customerAccess;
            _courseAccess = courseAccess;
            _lessonAccess = lessonAccess;
        }

        public UserProfileData GetUserProfileData(string username)
        {
            UserProfileData profileData = new UserProfileData();

            try
            {
                // 1. Get Account Info
                Account account = _accountAccess.ShowDataInforAccountByUsername(username);
                profileData.Account = account;

                if (account != null && !string.IsNullOrEmpty(account.ID))
                {
                    // 2. Get Customer Info (assuming Account_ID is used as Customer_ID in proc_show_customer)
                    // If proc_show_customer actually needs Customer_ID, you'll need to
                    // get Customer_ID first or modify your SP.
                    // Based on your CustomerAccess.ShowDataInforCus(string customerId),
                    // it expects Customer_ID, but inside it maps to customer.Account_ID.
                    // So, for now, I'll pass the Account.ID assuming it's used for lookup.
                    (Customer customer, Account linkedAccount) = _customerAccess.ShowDataInforCus(account.ID); // Passing Account.ID as Customer_ID
                    profileData.Customer = customer;

                    // 3. Get User-Specific Course Data
                    // These methods are SUGGESTED and need implementation in DAL/CourseAccess.cs
                    profileData.RegisteredCourses = _courseAccess.GetRegisteredCoursesByAccountId(account.ID);
                    profileData.FavoriteCourses = _courseAccess.GetFavoriteCoursesByAccountId(account.ID);

                    // 4. Get User-Specific Lesson Data
                    // These methods are SUGGESTED and need implementation in DAL/LessonAccess.cs
                    profileData.LessonHistory = _lessonAccess.GetLessonHistoryByAccountId(account.ID);
                    profileData.FavoriteLessons = _lessonAccess.GetFavoriteLessonsByAccountId(account.ID);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetUserProfileData: {ex.Message}");
                // Optionally rethrow or return null/empty data
            }

            return profileData;
        }
        AccountAccess acAccess = new AccountAccess();
        public string CheckLogin(string username, string password)
        {
            if (username == "")
            {
                return "NULL_Username";
            }
            if (password == "")
            {
                return "NULL_Password";
            }
            string info = acAccess.CheckLoginData(username, password);
            return info;
        }
        public bool SignUp(Account account, Customer customer)
        {
            bool ResultACC = true;
            string[] information = { account.Username, account.Password, customer.Name, customer.Phone, customer.Email };
            MyDelegate[] methods = { ChecklogicUsername, CheckLogicPassWord, ChecklogicName, ChecklogicPhone, ChecklogicEmail };

            for (int i = 0; i < information.Length; i++)
            {
                if (methods[i](information[i]) != "valid_true")
                {
                    ResultACC = ResultACC && true;
                }
            }
            return ResultACC;
        }
        public void AddCus(Account account, Customer customer)
        {
            acAccess.AddDataCustomer(customer, account);
        }
        public static bool SendOTP(string recipientEmail, string otpCode)
        {
            try
            {
                var fromAddress = new MailAddress("Noobita.Vn@gmail.com");
                var toAddress = new MailAddress(recipientEmail);
                const string frommpass = "fypc thnw ptss bitk";
                const string subject = "Your OTP Code";
                string body = $"Your OTP code is: {otpCode}";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, frommpass),
                    Timeout = 200000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string CheckOTP(string OTP, OTPManager OTPmanager)
        {
            if (DateTime.Now > OTPmanager.expiryTime) // Kiểm tra OTP có hết hạn không
            {
                return "OTP_expired";
            }
            if (OTP == OTPmanager.storedOTP)
            {
                return "OTP_valid";
            }
            else
            {
                return "OTP_not_valid";
            }
        }
        public string AddAccountCustomer(Account account, Customer customer)
        {
            ///Add Cus
            return "Add_valid";
        }
        public string ForGivePassword(Person person, string newPassWord, OTPManager OTPmanager, string OTP)
        {
            string result = "";
            if (SendOTP(person.Email, OTPmanager.storedOTP))
            {
                if (CheckOTP(OTP, OTPmanager) == "OTP_valid")
                {
                    acAccess.ChangeDataPassword(person, newPassWord);
                    result = "Forgive_valid";
                }
                else
                {
                    result = CheckOTP(OTP, OTPmanager);
                }
            }
            else
            {
                result = "Can't send Email";
            }

            return result;
        }
        public string ChangePassWord(Person person, string newpassword)
        {
            string result = "";
            if (acAccess.ChangeDataPassword(person, newpassword) == "Password_updated_successfully.")
            {
                result = "ChangePassSuccess";
            }
            else
            {
                result = acAccess.ChangeDataPassword(person, newpassword);
            }
            return result;
        }
        public string ChangeName(Person person, string newname)
        {
            string result = "";
            if (acAccess.ChangeDataName(person, newname) == "Name_updated_successfully.")
            {
                result = "ChangeNameSuccess";
            }
            else
            {
                result = acAccess.ChangeDataName(person, newname);
            }
            return result;
        }
        public string ChangePhone(Person person, string newphone)
        {
            string result = "";
            if (acAccess.ChangeDataPhone(person, newphone) == "Phone_updated_successfully.")
            {
                result = "ChangePhoneSuccess";
            }
            else
            {
                result = acAccess.ChangeDataPhone(person, newphone);
            }
            return result;
        }
        public Account FindAccount(string accountID)
        {
            Account account = acAccess.FindAccountData(accountID);
            return account;
        }
        public object GetUserByAccountID(string accountID)
        {
            object user = acAccess.GetUserByAccountIDData(accountID);
            return user;
        }
    }
}  
