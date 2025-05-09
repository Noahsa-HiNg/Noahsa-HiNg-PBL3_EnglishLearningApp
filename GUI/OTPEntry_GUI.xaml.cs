using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GUI
{
    /// <summary>
    /// Interaction logic for OTPEntry_GUI.xaml
    /// </summary>
    public partial class OTPEntry_GUI : Window
    {
        private string _correctOtp; // Mã OTP đúng
        private DateTime _otpExpirationTime; // Thời gian mã OTP hết hạn
        private DispatcherTimer _timer; // Timer để đếm ngược thời gian hiệu lực
        private const int OtpValidityMinutes = 5; // Thời gian hiệu lực của OTP (phút)
        private const int ResendDelaySeconds = 60; // Thời gian chờ để gửi lại OTP (giây)

        // Event để thông báo khi người dùng yêu cầu gửi lại OTP
        public event EventHandler ResendOtpRequested;

        // Hàm tạo nhận mã OTP đúng và thời gian tạo mã
        public OTPEntry_GUI(string correctOtp)
        {
            InitializeComponent();
            _correctOtp = correctOtp;
            _otpExpirationTime = DateTime.Now.AddMinutes(OtpValidityMinutes); // Tính thời gian hết hạn
            StartTimer(); // Bắt đầu đếm ngược
        }

        // Hàm tạo bổ sung nếu bạn muốn truyền thời gian hết hạn cụ thể
        public OTPEntry_GUI(string correctOtp, DateTime expirationTime)
        {
            InitializeComponent();
            _correctOtp = correctOtp;
            _otpExpirationTime = expirationTime;
            StartTimer(); // Bắt đầu đếm ngược
        }

        private void StartTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1); // Cập nhật mỗi giây
            _timer.Tick += Timer_Tick; // Gắn sự kiện Tick
            _timer.Start(); // Bắt đầu timer

            // Đặt timer để cho phép nút Gửi lại sau ResendDelaySeconds
            DispatcherTimer resendTimer = new DispatcherTimer();
            resendTimer.Interval = TimeSpan.FromSeconds(ResendDelaySeconds);
            resendTimer.Tick += (s, e) =>
            {
                btnResend.IsEnabled = true; // Bật nút Gửi lại
                ((DispatcherTimer)s).Stop(); // Dừng timer này
            };
            resendTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan remainingTime = _otpExpirationTime - DateTime.Now;

            if (remainingTime <= TimeSpan.Zero)
            {
                _timer.Stop();
                txtTimer.Text = "Mã OTP đã hết hạn.";
                btnVerify.IsEnabled = false; // Vô hiệu hóa nút xác nhận
                MessageBox.Show("Mã OTP của bạn đã hết hạn. Vui lòng yêu cầu gửi lại mã mới.", "Hết hạn OTP", MessageBoxButton.OK, MessageBoxImage.Warning);
                // Bạn có thể đóng cửa sổ ở đây hoặc để người dùng nhấn nút gửi lại
                // this.DialogResult = false; // Đặt kết quả là thất bại
                // this.Close();
            }
            else
            {
                txtTimer.Text = $"Mã OTP hết hạn sau: {remainingTime:m\\:ss}"; // Định dạng mm:ss
            }
        }

        private void btnVerify_Click(object sender, RoutedEventArgs e)
        {
            string enteredOtp = txtOtp.Text.Trim();

            // Kiểm tra xem mã OTP đã hết hạn chưa
            if (DateTime.Now > _otpExpirationTime)
            {
                MessageBox.Show("Mã OTP đã hết hạn. Vui lòng yêu cầu gửi lại mã mới.", "Hết hạn OTP", MessageBoxButton.OK, MessageBoxImage.Warning);
                // Không làm gì thêm, chờ người dùng gửi lại hoặc đóng cửa sổ
                return; // Thoát khỏi phương thức
            }

            // So sánh mã OTP nhập vào với mã đúng (so sánh không phân biệt chữ hoa/thường nếu cần)
            if (enteredOtp == _correctOtp)
            {
                MessageBox.Show("Xác nhận OTP thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                _timer.Stop(); // Dừng timer
                this.DialogResult = true; // Đặt kết quả cửa sổ là TRUE (thành công)
                this.Close(); // Đóng cửa sổ
            }
            else
            {
                MessageBox.Show("Mã OTP không đúng. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                txtOtp.Clear(); // Xóa nội dung ô nhập OTP
                txtOtp.Focus(); // Đặt focus lại vào ô nhập
            }
        }

        private void btnResend_Click(object sender, RoutedEventArgs e)
        {
            // Vô hiệu hóa lại nút gửi lại ngay lập tức
            btnResend.IsEnabled = false;

            // Thông báo cho cửa sổ hoặc logic gọi nó biết rằng cần gửi lại OTP
            // Sử dụng Event để thực hiện việc này là cách tốt
            ResendOtpRequested?.Invoke(this, EventArgs.Empty);

            // Cập nhật thời gian hết hạn mới và khởi động lại timer
            _otpExpirationTime = DateTime.Now.AddMinutes(OtpValidityMinutes);
            btnVerify.IsEnabled = true; // Bật lại nút xác nhận nếu trước đó bị vô hiệu hóa
            StartTimer(); // Bắt đầu lại timer đếm ngược và timer cho nút Gửi lại
        }

        // Dừng timer khi cửa sổ đóng để tránh rò rỉ bộ nhớ
        private void Window_Closed(object sender, EventArgs e)
        {
            _timer?.Stop();
        }

        public void UpdateOtpInfo(string newOtp, DateTime newExpiration)
        {
            _correctOtp = newOtp;
            _otpExpirationTime = newExpiration;
            // Reset timer và giao diện nếu cần thiết (ví dụ: đặt lại text timer và bật lại nút xác nhận)
            // Bạn có thể dừng timer cũ và gọi StartTimer() lại ở đây
            _timer?.Stop(); // Dừng timer cũ
            StartTimer(); // Bắt đầu timer mới với thời gian hết hạn mới
            btnVerify.IsEnabled = true; // Đảm bảo nút xác nhận được bật lại
            txtOtp.Clear(); // Xóa nội dung ô nhập cũ
        }

    }
}
