using System;
using System.Net;
using System.Net.Mail; // Cần thêm namespace này
using System.Threading.Tasks; // Cần cho SendMailAsync

public static class EmailSender
{
    // Thông tin cấu hình Gmail SMTP
    private const string SmtpServer = "smtp.gmail.com";
    private const int SmtpPort = 587; // Port chuẩn cho TLS/STARTTLS
    private const string SenderEmail = "duypanda273@gmail.com"; // Địa chỉ Gmail của bạn
    private const string SenderPassword = "mfsi sojz kgyk xjli"; // Mật khẩu ứng dụng của Gmail

    public static async Task SendOtpEmailAsync(string recipientEmail, string otpCode)
    {
        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(SenderEmail); // Người gửi
            mail.To.Add(recipientEmail); // Người nhận
            mail.Subject = "Mã Xác nhận OTP của bạn"; // Chủ đề email
            mail.Body = $"Mã xác nhận OTP của bạn là: <strong>{otpCode}</strong>. Mã này có hiệu lực trong vòng 5 phút."; // Nội dung email
            mail.IsBodyHtml = true; // Đặt là true nếu nội dung là HTML

            using (SmtpClient smtp = new SmtpClient(SmtpServer, SmtpPort))
            {
                smtp.Credentials = new NetworkCredential(SenderEmail, SenderPassword); // Thông tin đăng nhập
                smtp.EnableSsl = true; // Bật SSL/TLS
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                try
                {
                    await smtp.SendMailAsync(mail); // Gửi email bất đồng bộ
                    Console.WriteLine($"Đã gửi OTP đến {recipientEmail}"); // Log hoặc xử lý thông báo thành công
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi gửi email: {ex.Message}"); // Log hoặc xử lý lỗi
                    // Ném lỗi hoặc xử lý lỗi gửi email tại đây
                    throw new Exception($"Không thể gửi email đến {recipientEmail}. Vui lòng thử lại.", ex);
                }
            }
        }
    }
}