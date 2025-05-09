using System;
using System.Text;

public static class OtpHelper
{
    // Tạo mã OTP số ngẫu nhiên có độ dài xác định
    public static string GenerateOtp(int length = 6) // Mặc định dài 6 chữ số
    {
        const string digits = "0123456789";
        StringBuilder otp = new StringBuilder();
        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            otp.Append(digits[random.Next(digits.Length)]);
        }

        return otp.ToString();
    }

    // Bạn có thể thêm hàm tạo OTP với cả chữ và số nếu cần
    // public static string GenerateAlphaNumericOtp(int length = 8) { ... }
}