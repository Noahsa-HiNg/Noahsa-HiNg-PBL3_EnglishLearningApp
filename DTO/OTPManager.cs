using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTO
{
    // lớp chứa các hàm tạo OTP và lưu lại với thời gian hết hạn
    public class OTPManager
    {
        public string storedOTP { set; get; }
        public DateTime expiryTime { set; get; }

        // Hàm tạo OTP và lưu lại với thời gian hết hạn
        public  OTPManager(int length = 6, int expireMinutes = 5)
        {
            const string chars = "0123456789";
            Random random = new Random();
            storedOTP = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            expiryTime = DateTime.Now.AddMinutes(expireMinutes); // OTP hết hạn sau X phút
        }
    }
}