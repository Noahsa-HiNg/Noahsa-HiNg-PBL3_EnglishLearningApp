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
using BLL;
using DAL;
using DTO;
namespace GUI
{
    /// <summary>
    /// Interaction logic for LoginGUI.xaml
    /// </summary>
    public partial class LoginGUI : Window
    {
        public LoginGUI()
        {
            InitializeComponent();

            // Đăng ký sự kiện cho nút đăng nhập
            btnSignIn.Click += BtnSignIn_Click;

            // Sự kiện nhấn Enter trong ô mật khẩu
            txtPassword.KeyDown += TxtPassword_KeyDown;

            // Sự kiện click "Forgot password"
            var forgotPasswordText = FindName("forgotPasswordText") as TextBlock;
            if (forgotPasswordText != null)
            {
                forgotPasswordText.MouseDown += ForgotPasswordText_MouseDown;
            }

            // Sự kiện click "Sign in" (ở dòng Already have an account)
            var signInText = FindName("signInText") as TextBlock;
            if (signInText != null)
            {
                signInText.MouseDown += SignInText_MouseDown;
            }
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            bool rememberMe = chkRememberMe.IsChecked ?? false;

            // Validate input
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter your email or username", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your password", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Xử lý đăng nhập
            try
            {
                // TODO: Thêm logic đăng nhập thực tế
                string result = AuthenticateUser(username, password, rememberMe);
                bool isLoginSuccessful = (result != "NULL_Username" && result != "NULL_Password") ? true : false;


                if (isLoginSuccessful)
                {
                    MessageBox.Show("Login successful!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Mở cửa sổ chính sau khi đăng nhập thành công
                    OpenMainDashboard();
                }
                else
                {
                    MessageBox.Show(result, "Login Failed",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login error: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSignIn_Click(sender, e);
            }
        }

        private void ForgotPasswordText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // TODO: Xử lý quên mật khẩu
            MessageBox.Show("Forgot password feature will be implemented soon", "Information",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SignInText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // TODO: Xử lý chuyển đến màn hình đăng nhập
            MessageBox.Show("You are already on the sign in page", "Information",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private string AuthenticateUser(string username, string password, bool rememberMe)
        {
            // TODO: Thêm logic xác thực thực tế
            // Tạm thời dùng tài khoản mẫu để test
            Account account = new Account();
            account.Username = username;
            account.Password = password;

            AccountAccess acAccess = new AccountAccess();
            string infor = acAccess.CheckLoginData(account);
            return infor;
        }

        private void SaveLoginInfo(string username, string password)
        {
            // TODO: Lưu thông tin đăng nhập vào file hoặc settings
        }

        private void OpenMainDashboard()
        {
            // TODO: Mở cửa sổ chính sau khi đăng nhập
            // var dashboard = new DashboardWindow();
            // dashboard.Show();
            // this.Close();
        }
    }
}
