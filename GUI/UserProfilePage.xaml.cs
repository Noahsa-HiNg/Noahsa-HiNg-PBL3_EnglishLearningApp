using System;
using System.Windows;
using System.Windows.Controls; // Thêm using này cho Page
using System.Windows.Navigation; // Thêm using này cho NavigationService
using DTO; // Đảm bảo bạn đã thêm Project Reference đến Project DTO
using BLL; // Đảm bảo bạn đã thêm Project Reference đến Project BLL

namespace GUI // Thay thế bằng Namespace thực tế của Project GUI của bạn (ví dụ: GUI)
{
    public partial class UserProfilePage : Page // Thay đổi từ Window sang Page
    {
        private User_AccountManagementBLL _bll;
        private Account _currentAccount; // Để lưu trữ thông tin tài khoản của người dùng hiện tại
        private Customer _currentCustomer; // Để lưu trữ thông tin khách hàng của người dùng hiện tại

        // Placeholder cho username của người dùng đang đăng nhập
        // Bạn cần thay thế bằng cách lấy username thực tế từ hệ thống đăng nhập của bạn.
        private string _loggedInUsername = "admin"; // Ví dụ: "admin"

        public UserProfilePage()
        {
            InitializeComponent();
            _bll = new User_AccountManagementBLL();
            Loaded += UserProfilePage_Loaded; // Tải dữ liệu khi Page được tải
        }

        private void UserProfilePage_Loaded(object sender, RoutedEventArgs e)
        {
            // Chỉ tải dữ liệu khi Page thực sự được hiển thị
            LoadUserProfileData();
        }

        private void LoadUserProfileData()
        {
            // Gọi BLL để lấy toàn bộ dữ liệu Profile
            UserProfileData profileData = _bll.GetUserProfileData(_loggedInUsername);

            if (profileData != null)
            {
                _currentAccount = profileData.Account; // Lưu trữ tài khoản để có thể cập nhật mật khẩu
                _currentCustomer = profileData.Customer; // Lưu trữ khách hàng để có thể cập nhật thông tin cá nhân

                // Gán dữ liệu vào các điều khiển UI
                UsernameTextBox.Text = profileData.Account?.Username;
                EmailTextBox.Text = profileData.Customer?.Email;
                FullNameTextBox.Text = profileData.Customer?.Name;
                PhoneTextBox.Text = profileData.Customer?.Phone;
            }
            else
            {
                MessageBox.Show("Không thể tải thông tin người dùng. Vui lòng thử lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                // Xóa các trường hoặc vô hiệu hóa chỉnh sửa nếu không tải được dữ liệu
                UsernameTextBox.Text = string.Empty;
                EmailTextBox.Text = string.Empty;
                FullNameTextBox.Text = string.Empty;
                PhoneTextBox.Text = string.Empty;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Sử dụng NavigationService để quay lại trang trước
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                // Nếu không có trang nào để quay lại (ví dụ: đây là trang đầu tiên),
                // bạn có thể chuyển hướng đến một trang mặc định hoặc đóng cửa sổ chứa Frame.
                MessageBox.Show("Không có trang nào để quay lại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                // Hoặc nếu Page này được nhúng trong một Window:
                // Window.GetWindow(this)?.Close(); // Đóng cửa sổ chứa Page
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentAccount == null || _currentCustomer == null)
            {
                MessageBox.Show("Không có thông tin tài khoản để cập nhật. Vui lòng tải lại trang.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool isPasswordChangeRequested = !string.IsNullOrWhiteSpace(CurrentPasswordBox.Password) ||
                                             !string.IsNullOrWhiteSpace(NewPasswordBox.Password) ||
                                             !string.IsNullOrWhiteSpace(ConfirmNewPasswordBox.Password);

            bool passwordChangeSuccessful = false;

            if (isPasswordChangeRequested)
            {
                string currentPassword = CurrentPasswordBox.Password;
                string newPassword = NewPasswordBox.Password;
                string confirmNewPassword = ConfirmNewPasswordBox.Password;

                if (string.IsNullOrWhiteSpace(currentPassword) ||
                    string.IsNullOrWhiteSpace(newPassword) ||
                    string.IsNullOrWhiteSpace(confirmNewPassword))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu cũ, mật khẩu mới và xác nhận mật khẩu để đổi.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (newPassword != confirmNewPassword)
                {
                    MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    passwordChangeSuccessful = _bll.ChangePassword(
                        _currentAccount.ID,
                        currentPassword,
                        newPassword
                    );

                    if (passwordChangeSuccessful)
                    {
                        MessageBox.Show("Mật khẩu đã được đổi thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                        CurrentPasswordBox.Clear();
                        NewPasswordBox.Clear();
                        ConfirmNewPasswordBox.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Đổi mật khẩu thất bại. Vui lòng kiểm tra mật khẩu hiện tại hoặc liên hệ quản trị viên.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi khi đổi mật khẩu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            // Cập nhật thông tin cá nhân (nếu có thay đổi)
            try
            {
                string newFullName = FullNameTextBox.Text;
                string newPhone = PhoneTextBox.Text;
                string newEmail = EmailTextBox.Text; // Giữ lại email nếu bạn muốn nó có thể chỉnh sửa

                // So sánh với dữ liệu cũ để tránh cập nhật không cần thiết
                if (_currentCustomer.Name != newFullName ||
                    _currentCustomer.Phone != newPhone ||
                    _currentCustomer.Email != newEmail)
                {
                    bool profileUpdated = _bll.UpdateCustomerProfile(
                        _currentCustomer.ID,
                        newFullName,
                        newPhone
                    );

                    if (profileUpdated)
                    {
                        MessageBox.Show("Thông tin cá nhân đã được cập nhật thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                        // Cập nhật lại _currentCustomer để phản ánh thay đổi
                        _currentCustomer.Name = newFullName;
                        _currentCustomer.Phone = newPhone;
                        _currentCustomer.Email = newEmail;
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thông tin cá nhân thất bại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else if (!isPasswordChangeRequested)
                {
                    MessageBox.Show("Không có thay đổi nào được thực hiện.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi cập nhật thông tin cá nhân: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}