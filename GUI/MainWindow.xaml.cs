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

            // Lấy ControlBarUC và đăng ký sự kiện PageChanged
            var controlBar = FindName("ControlBar") as UserControlGUI.ControlBarUC;
            if (controlBar != null)
            {
                controlBar.PageChanged += OnPageChanged;
            }

            // Gán trang mặc định
            MainFrame.Content = new ListCourse();
        }

        private void OnPageChanged(Type pageType)
        {
            // Tạo instance của Page dựa trên type
            if (Activator.CreateInstance(pageType) is Page page)
            {
                MainFrame.Content = page;
            }
        }
    }
}
