using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            Main.Content = new ListCourse();
        }

        // Xử lý sự kiện thay đổi trong ô tìm kiếm
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Nếu có văn bản trong ô tìm kiếm, hiển thị nút xóa
            if (!string.IsNullOrEmpty(SearchBox.Text))
            {
                ClearSearchButton.Visibility = Visibility.Visible;
            }
            else
            {
                ClearSearchButton.Visibility = Visibility.Collapsed;
            }

            // Xử lý tìm kiếm hoặc lọc khóa học (tạm thời chỉ để hiển thị thông báo)
            // Lưu ý: Bạn có thể thêm mã để tìm kiếm trong danh sách khóa học.
            // Ví dụ: FilterCourses(SearchBox.Text);
        }

        // Xử lý sự kiện khi nhấn nút xóa trong ô tìm kiếm
        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Clear();  // Xóa văn bản trong ô tìm kiếm
            ClearSearchButton.Visibility = Visibility.Collapsed;  // Ẩn nút xóa
        }

        // Xử lý khi nhấn vào liên kết "Khóa học của bạn"
        private void MyCourse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Chuyển đến trang Khóa học của bạn");
            // Thực hiện các hành động khác nếu cần, như mở một trang chi tiết về khóa học của người dùng
        }

        // Xử lý khi nhấn vào liên kết "Bài học"
        private void Mylesson_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Chuyển đến trang Bài học");
            // Thực hiện các hành động khác nếu cần, như mở một trang chi tiết về bài học của người dùng
        }

        // Xử lý khi nhấn vào biểu tượng giỏ hàng
        private void Shoping_card_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chuyển đến giỏ hàng");
            // Thực hiện các hành động khác nếu cần, như mở trang giỏ hàng
        }

        // Xử lý khi nhấn vào biểu tượng chuông thông báo
        private void Bell_icon_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Xem thông báo");
            // Thực hiện các hành động khác nếu cần, như mở một cửa sổ thông báo
        }

        // Xử lý khi nhấn vào icon UserProfile để mở menu
        private void UserProfile_Click(object sender, RoutedEventArgs e)
        {
            // Toggle visibility của popup menu User Profile
            
        }
    }
}
