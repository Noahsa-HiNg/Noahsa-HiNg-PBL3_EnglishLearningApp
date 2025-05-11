using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BLL;
using DAL;
using DTO;

namespace GUI
{
    public partial class LoginGUI : Window
    {
        public string ID_user;
        private bool isVisiblePW = false;
        private readonly User_AccountManagementBLL _userBLL = new User_AccountManagementBLL();

        public LoginGUI()
        {
            InitializeComponent();

            // Đặt focus vào ô username khi khởi động
            txtUsername.Focus();
        }

        //private void BtnLogin_Click(object sender, RoutedEventArgs e)
        //{
        //    AttemptLogin();
        //}

        //private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        AttemptLogin();
        //    }
        //}

        private void ForgotPassword_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Please contact admin to reset your password", "Forgot Password",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //private void SignIn_Click(object sender, MouseButtonEventArgs e)
        //{
        //    MessageBox.Show("You are already on the login page", "Information",
        //        MessageBoxButton.OK, MessageBoxImage.Information);
        //}

        //private void AttemptLogin()
        //{
        //    string username = txtUsername.Text.Trim();
        //    string password = txtPassword.Password;
        //    bool rememberMe = chkRememberMe.IsChecked ?? false;

        //    if (ValidateInput(username, password))
        //    {
        //        try
        //        {
        //            string loginResult = AuthenticateUser(username, password);

        //            if (loginResult != "NULL_Username" && loginResult != "NULL_Password")
        //            {
        //                HandleSuccessfulLogin(loginResult, rememberMe);
        //            }
        //            else
        //            {
        //                MessageBox.Show(GetLoginErrorMessage(loginResult), "Login Failed",
        //                    MessageBoxButton.OK, MessageBoxImage.Error);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Login error: {ex.Message}", "Error",
        //                MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //}

        //private bool ValidateInput(string username, string password)
        //{
        //    if (string.IsNullOrWhiteSpace(username))
        //    {
        //        MessageBox.Show("Please enter your email or username", "Error",
        //            MessageBoxButton.OK, MessageBoxImage.Warning);
        //        txtUsername.Focus();
        //        return false;
        //    }

        //    if (string.IsNullOrWhiteSpace(password))
        //    {
        //        MessageBox.Show("Please enter your password", "Error",
        //            MessageBoxButton.OK, MessageBoxImage.Warning);
        //        txtPassword.Focus();
        //        return false;
        //    }

        //    return true;
        //}

        //private string AuthenticateUser(string username, string password)
        //{
        //    Account account = new Account
        //    {
        //        Username = username,
        //        Password = password
        //    };

        //    AccountAccess acAccess = new AccountAccess();
        //    string infor = acAccess.CheckLoginData(account);
        //    this.ID_user = infor;
        //    return infor;
        //}

        //private void HandleSuccessfulLogin(string userInfo, bool rememberMe)
        //{
        //    if (rememberMe)
        //    {
        //        SaveLoginInfo(txtUsername.Text, txtPassword.Password);
        //    }

        //    MessageBox.Show("Login successful!", "Success",
        //        MessageBoxButton.OK, MessageBoxImage.Information);

        //    // Mở cửa sổ chính và đóng cửa sổ đăng nhập
        //    MainWindow mainWindow = new MainWindow();
        //    mainWindow.Show();
        //    this.Close();
        //}

        //private string GetLoginErrorMessage(string errorCode)
        //{
        //    return errorCode switch
        //    {
        //        "NULL_Username" => "Username does not exist",
        //        "NULL_Password" => "Incorrect password",
        //        _ => "Login failed. Please try again."
        //    };
        //}

        //private void SaveLoginInfo(string username, string password)
        //{
        //    // Lưu thông tin đăng nhập (có thể sử dụng Properties.Settings hoặc file config)
        //    //Properties.Settings.Default.Username = username;
        //    //Properties.Settings.Default.Password = password;
        //    //Properties.Settings.Default.Save();
        //}

        private void pwbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(!isVisiblePW) txtboxPassword.Text = pwbPassword.Password;
        }



        //Ẩn/hiện mật khẩu
        private void toggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (isVisiblePW) //Nếu mật khẩu đang hiển thị
            {
                pwbPassword.Password = txtboxPassword.Text;
                pwbPassword.Visibility = Visibility.Visible;
                txtboxPassword.Visibility = Visibility.Collapsed;
                toggleButton.Content = "🙈";
            }
            else //Nếu mật khẩu dang ẩn
            {
                txtboxPassword.Text = pwbPassword.Password;
                txtboxPassword.Visibility = Visibility.Visible; 
                pwbPassword.Visibility = Visibility.Collapsed ;
                toggleButton.Content = "👁";
            }

            isVisiblePW = !isVisiblePW;
        }

        private void textBoxPassword_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if(isVisiblePW) pwbPassword.Password = txtboxPassword.Text;
        }

        private void SignUp_Click(object sender, MouseButtonEventArgs e)
        {
            Register_GUI registerWindow = new Register_GUI();
            registerWindow.Show();
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = pwbPassword.Password; 
            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                txtMessage.Text = "Vui lòng nhập đầy đủ thông tin!";
                txtMessage.Visibility = Visibility.Visible;
                return;
            }
            
            string item = _userBLL.CheckLogin(username, password);
            if (item == "LockedAccount")
            {
                txtMessage.Text = "Tài khoản của bạn đã bị khóa.";
                txtMessage.Visibility = Visibility.Visible;
                return;
            }

            if(item == "UsenamePassworkFalt")
            {
                txtMessage.Text = "Tên đăng nhập hoặc mật khẩu chưa đúng.";
                txtMessage.Visibility = Visibility.Visible;
                return;
            }
            User_AccountManagementBLL user_AccountManagementBLL = new User_AccountManagementBLL();
            var user = user_AccountManagementBLL.FindAccount(item);
            if(user != null)
            {
                UserSession.Instance.ID = user.ID;
                UserSession.Instance.Username = user.Username;
                UserSession.Instance.Password = user.Password;
                UserSession.Instance.Avatar = user.Avatar;
                UserSession.Instance.Role = user.Role;
                UserSession.Instance.Status = user.Status;
            }
            txtMessage.Text = "Đăng nhập thành công!";
            txtMessage.Visibility = Visibility.Visible;
            txtMessage.Foreground = Brushes.Green;

            //Remember me
            if (chkRememberMe.IsChecked == true)
            {
                // Người dùng muốn ghi nhớ
                Properties.Settings.Default.RememberMe = true;
                Properties.Settings.Default.LastUsername = username;
            }
            else
            {
                // Người dùng không muốn ghi nhớ
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.LastUsername = ""; // Xóa tên đăng nhập cũ nếu có
            }

            // Lưu các thay đổi vào tệp cấu hình

            Properties.Settings.Default.Save();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.MainFrame.Navigate(new ListCourse());

            // Đóng cửa sổ LoginGUI
            this.Close();
        }
        public void Logout()
        {
            // Xóa thông tin đăng nhập
            UserSession.Instance.ID = null;
            UserSession.Instance.Username = null;
            UserSession.Instance.Password = null;
            UserSession.Instance.Avatar = null;
            UserSession.Instance.Role = null;
            UserSession.Instance.Status = 0;
            // Xóa thông tin ghi nhớ
            Properties.Settings.Default.RememberMe = false;
            Properties.Settings.Default.LastUsername = "";
            Properties.Settings.Default.Save();
        }
    }
}