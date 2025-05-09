using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL;
using DTO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Register_GUI.xaml
    /// </summary>
    public partial class Register_GUI : Window
    {
        private bool isVisiblePass = false;
        private bool isVisibleRePass = false;
        private readonly User_AccountManagementBLL _userBLL;
        private readonly UserDataValidatorBLL _userDataVal;

        private readonly Uri checkIconUri = new Uri("pack://application:,,,/Images/check.png");
        private readonly Uri crossIconUri = new Uri("pack://application:,,,/Images/delete.png");

        public Register_GUI()
        {
            InitializeComponent();
            _userBLL = new User_AccountManagementBLL();
            _userDataVal = new UserDataValidatorBLL();
        }
        private void toggleButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (isVisiblePass)
            {
                txtPassword.Password = txtboxPassword.Text;
                txtboxPassword.Visibility = Visibility.Collapsed;
                txtPassword.Visibility = Visibility.Visible;
                toggleButton.Content = "🙈";
            }
            else
            {
                txtboxPassword.Text = txtPassword.Password;
                txtboxPassword.Visibility = Visibility.Visible;
                txtPassword.Visibility = Visibility.Collapsed;
                toggleButton.Content = "👁";
            }
            isVisiblePass = !isVisiblePass;
        }

        private void toggleButton_Click_2(object sender, RoutedEventArgs e)
        {
            if (isVisibleRePass)
            {
                txtRePassword.Password = txtboxRePassword.Text;
                txtboxRePassword.Visibility = Visibility.Collapsed;
                txtRePassword.Visibility = Visibility.Visible;
                retoggleButton.Content = "🙈";
            }
            else
            {
                txtboxRePassword.Text = txtRePassword.Password;
                txtboxRePassword.Visibility = Visibility.Visible;
                txtRePassword.Visibility = Visibility.Collapsed;
                retoggleButton.Content = "👁";
            }
            isVisibleRePass = !isVisibleRePass;
        }

        private async void BtnSignUp_Click(object sender, RoutedEventArgs e)
        { 
            //Lấy dữ liệu từ các controls
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password; // Lấy từ PasswordBox
            string repeatPassword = txtRePassword.Password;
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phoneNumber = txtPhone.Text.Trim();

            // 2. Kiểm tra cơ bản phía client

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(repeatPassword) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Dừng lại nếu thiếu thông tin
            }

            if (password != repeatPassword)
            {
                MessageBox.Show("Mật khẩu và mật khẩu nhập lại không khớp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Dừng lại nếu mật khẩu không khớp
            }



            // Tạo đối tượng DTO để chứa dữ liệu
            Customer newCustomer = new Customer
            {
                Name = name,
                Email = email,
                Phone = phoneNumber,
                Notification_Enable = true,
                Created_Date = DateTime.Now,
            };

            Account newAcc = new Account
            {
                Username = username,
                Password = password,
                Role = "Customer",
            };

            // Gọi hàm xử lý đăng ký ở lớp BLL
            try
            {
                bool registrationResult = _userBLL.SignUp(newAcc, newCustomer);

                if (registrationResult)
                {
                    string generatedOtp = OtpHelper.GenerateOtp();
                    string userEmail = email; // Lấy email của người dùng
                    DateTime otpCreationTime = DateTime.Now; // Ghi lại thời gian tạo OTP

                    // Lưu generatedOtp và otpCreationTime vào nơi nào đó để kiểm tra sau (ví dụ: trong bộ nhớ tạm cho phiên đăng ký, hoặc tốt hơn là cơ sở dữ liệu)
                    // Ví dụ đơn giản (chỉ dùng tạm trong demo):
                    // var pendingVerification = new { Otp = generatedOtp, CreationTime = otpCreationTime, Email = userEmail };
                    // Lưu pendingVerification vào một danh sách tạm thời

                    try
                    {
                        // Gửi email OTP
                        await EmailSender.SendOtpEmailAsync(userEmail, generatedOtp);

                        // Sau khi gửi thành công, hiển thị cửa sổ nhập OTP
                        OTPEntry_GUI otpWindow = new OTPEntry_GUI(generatedOtp, otpCreationTime.AddMinutes(5)); // Truyền mã và thời gian hết hạn

                        // Đăng ký xử lý sự kiện gửi lại
                        otpWindow.ResendOtpRequested += (s, e) =>
                        {
                            // Logic để tạo mã OTP mới, gửi lại email
                            string newOtp = OtpHelper.GenerateOtp();
                            DateTime newExpiration = DateTime.Now.AddMinutes(5);
                            // Lưu newOtp và newExpiration
                            // Gửi email OTP mới
                            // Cập nhật lại thông tin OTP trong cửa sổ OTP
                            ((OTPEntry_GUI)s).UpdateOtpInfo(newOtp, newExpiration); // Cần thêm phương thức này vào OtpEntryWindow
                            MessageBox.Show("Mã OTP mới đã được gửi.", "Gửi lại thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                        };


                        // Hiển thị cửa sổ OTP dưới dạng Dialog
                        bool? result = otpWindow.ShowDialog();

                        // Kiểm tra kết quả khi cửa sổ đóng
                        if (result == true)
                        {
                            // OTP đã được xác nhận thành công
                            MessageBox.Show("Đăng ký hoàn tất và xác thực email thành công!", "Thành công");
                            // Thực hiện hành động tiếp theo, ví dụ: chuyển đến trang chủ
                        }
                        else
                        {
                            // Xác nhận OTP thất bại (người dùng đóng cửa sổ hoặc mã hết hạn)
                            MessageBox.Show("Xác thực email không thành công.", "Thất bại");
                            // Có thể yêu cầu người dùng thử lại hoặc thực hiện hành động khác
                        }
                        MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        // Có thể đóng cửa sổ đăng ký hoặc chuyển sang màn hình đăng nhập
                        // this.Close();

                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi nếu gửi email thất bại
                        MessageBox.Show($"Không thể gửi mã xác nhận. Vui lòng kiểm tra lại địa chỉ email hoặc thử lại sau. Chi tiết lỗi: {ex.Message}", "Lỗi gửi email", MessageBoxButton.OK, MessageBoxImage.Error);
                        // Có thể không hiển thị cửa sổ OTP nếu gửi email lỗi
                    }

                    
                }
                else
                {
                    // BLL nên cung cấp lý do thất bại cụ thể hơn nếu có thể,
                    // nhưng ở đây chỉ hiển thị thông báo chung
                    MessageBox.Show("Đăng ký thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (ArgumentException argEx) // Bắt lỗi validation cụ thể từ BLL
            {
                MessageBox.Show($"Lỗi dữ liệu: {argEx.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex) // Bắt các lỗi không mong muốn khác
            {
                // Ghi log lỗi ở đây thì tốt hơn là chỉ hiển thị
                MessageBox.Show($"Đã xảy ra lỗi không mong muốn: {ex.Message}", "Lỗi hệ thống", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Hàm xử lý sự kiện cho liên kết "Sign in" 
        private void BtnSignIn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Mở cửa sổ đăng nhập
            LoginGUI loginWindow = new();
            loginWindow.Show();
            this.Close(); // Đóng cửa sổ đăng ký
        }

        // Hàm kiểm tra sự trùng khớp và cập nhật UI
        private void ValidatePasswordMatch()
        {
            string password = txtPassword.Password; // Lấy mật khẩu gốc
            string repeatPassword = txtRePassword.Password; // Lấy mật khẩu nhập lại

            // Nếu ô nhập lại mật khẩu rỗng, ẩn các chỉ báo và thoát
            if (string.IsNullOrEmpty(repeatPassword))
            {
                pnlPasswordStatus.Visibility = Visibility.Hidden; // Ẩn cả panel chứa status
                return;
            }

            // Nếu ô nhập lại không rỗng, hiển thị panel status
            pnlPasswordStatus.Visibility = Visibility.Visible;

            // So sánh hai mật khẩu
            if (password == repeatPassword)
            {
                // Mật khẩu trùng khớp
                imgPasswordMatchStatus.Source = new BitmapImage(checkIconUri); // Đặt ảnh dấu tích
                txtPasswordMatchStatus.Text = "Mật khẩu trùng khớp";         // Đặt text
                txtPasswordMatchStatus.Foreground = Brushes.Green;            // Đặt màu chữ xanh
            }
            else
            {
                // Mật khẩu không khớp
                imgPasswordMatchStatus.Source = new BitmapImage(crossIconUri); // Đặt ảnh dấu X
                txtPasswordMatchStatus.Text = "Mật khẩu không khớp";        // Đặt text
                txtPasswordMatchStatus.Foreground = Brushes.Red;              // Đặt màu chữ đỏ
            }
        }

        //Đồng bộ giữa 2 Passwordbox và TxtPassword, kiểm tra trùng khớp mật khẩu
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!isVisiblePass)
            {
                txtboxPassword.Text = txtPassword.Password;
            }
            //Kiểm tra mật khẩu trungf khớp
            ValidatePasswordMatch();

            //Kiểm tra mật khẩu hợp lệ
            string Password = txtPassword.Password;
            if (string.IsNullOrEmpty(Password))
            {
                txtPasswordValid_1.Foreground = Brushes.Black;
                txtPasswordValid_2.Foreground = Brushes.Black;
                txtPasswordValid_3.Foreground = Brushes.Black;
                txtPasswordValid_4.Foreground = Brushes.Black;
                return;
            }

            // Kiểm tra độ dài mật khẩu (ít nhất 8 ký tự)
            if (Password.Length < 8)       txtPasswordValid_1.Foreground = Brushes.Red;
            else                           txtPasswordValid_1.Foreground = Brushes.Green;
            // Kiểm tra ký tự đầu tiên có phải là chữ cái in hoa không
            if (!char.IsUpper(Password[0])) txtPasswordValid_2.Foreground = Brushes.Red;
            else                            txtPasswordValid_2.Foreground = Brushes.Green;
            // Kiểm tra xem mật khẩu có chứa ít nhất một số không
            if (!Password.Any(char.IsDigit)) txtPasswordValid_3.Foreground = Brushes.Red;
            else                             txtPasswordValid_3.Foreground = Brushes.Green;
            // Kiểm tra xem mật khẩu có chứa ít nhất một ký tự đặc biệt không
            if (!Password.Any(ch => !char.IsLetterOrDigit(ch))) txtPasswordValid_4.Foreground = Brushes.Red;
            else                                                txtPasswordValid_4.Foreground = Brushes.Green;
            return;


        }

        private void textBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isVisiblePass)
            {
                txtPassword.Password = txtboxPassword.Text;
            }

        }

        private void txtRePassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!isVisibleRePass)
            {
                txtboxRePassword.Text = txtRePassword.Password;
            }

            //Kiểm tra mật khẩu trungf khớp
            ValidatePasswordMatch();
        }

        private void txtboxRePassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isVisibleRePass)
            {
                txtRePassword.Password = txtboxRePassword.Text;
            }
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            string username = txtUsername.Text;
            if (string.IsNullOrEmpty(username))
            {
                txtUsernameValid_1.Foreground = Brushes.Black;
                txtUsernameValid_2.Foreground = Brushes.Black;
                return;
            }

            //Kiem tra do dai
            if (username.Length < 6)  txtUsernameValid_1.Foreground = Brushes.Red;

            else  txtUsernameValid_1.Foreground = Brushes.Green;
            
            // Kiểm tra xem tên người dùng có chứa khoảng trắng không
            if (username.Contains(" "))     txtUsernameValid_2.Foreground = Brushes.Red;
            
            else  txtUsernameValid_2.Foreground = Brushes.Green; 

        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = txtName.Text;
            if (string.IsNullOrEmpty(name))
            {
                txtNameValid_1.Foreground = Brushes.Black;
                txtNameValid_2.Foreground = Brushes.Black;
                return;
            }
            // Kiểm tra xem tên có chứa ký tự số không
            if (name.Any(char.IsDigit)) txtNameValid_1.Foreground = Brushes.Red;
            else                        txtNameValid_1.Foreground = Brushes.Green;
            // Kiểm tra xem tên có chứa ký tự đặc biệt không
            if (name.Any(ch => !char.IsLetter(ch) && ch != ' ')) txtNameValid_2.Foreground = Brushes.Red;
            else                                                 txtNameValid_2.Foreground = Brushes.Green;
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            string email = txtEmail.Text;

            if (string.IsNullOrEmpty(email))
            {
                txtEmailValid_1.Foreground = Brushes.Black;
                txtEmailValid_1.Text = "Định dạng: example@gmail.com";
                txtEmailValid_2.Visibility = Visibility.Hidden;
                return;
            }

            // Kiểm tra định dạng email
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                txtEmailValid_1.Foreground = Brushes.Red;
                txtEmailValid_1.Text = "Định dạng không hợp lệ";
            }
            else
            {
                txtEmailValid_1.Foreground = Brushes.Green;
                txtEmailValid_1.Text = "Định dạng hợp lệ";
            }
            //kiểm tra xem email có tồn tại trong database không
            if (_userBLL.isExitedMail(email))
            {
                txtEmailValid_2.Visibility = Visibility.Visible;
            }
        }
    }
}
