﻿using System;
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
        AccountAccess acAccess = new AccountAccess();
        public string CheckLogin(Account account)
        {
            if (account.Username == "")
            {
                return "NULL_Username";
            }
            if (account.Password == "")
            {
                return "NULL_Password";
            }
            string info = acAccess.CheckLoginData(account);
            return info;
        }
        public string[] SignUp(Account account, Customer customer)
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
            return ResultACC;
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
        public int CreateID_Customer()
        {
            int id = acAccess.Get_Quantily_Account_DATA() + 1;
            return id;
        }
    }
}  
