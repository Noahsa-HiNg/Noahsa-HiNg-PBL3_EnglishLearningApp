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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GUI.ViewModel;

namespace GUI.UserControlGUI
{
    /// <summary> 
    /// Interaction logic for ControlBarUC.xaml
    /// </summary>
    public partial class ControlBarUC : UserControl
    {

        public ControlBarViewModel Viewmodel { get; set; }
        public event Action<Type> PageChanged; // Sự kiện để thông báo thay đổi Page

        public ControlBarUC()
        {
            InitializeComponent();
            if (UserSession.Instance.Role == "Admin")
            {
                ManagerCustomer.Visibility = Visibility.Visible;
                ManagerEditor.Visibility = Visibility.Visible;
                Statistics.Visibility = Visibility.Visible;
            }
            else
            {
                ManagerCustomer.Visibility = Visibility.Collapsed;
                ManagerEditor.Visibility = Visibility.Collapsed;
                Statistics.Visibility = Visibility.Collapsed;
            }
            if (UserSession.Instance.Role == "Editor")
            {
                ManagerEditor.Visibility = Visibility.Visible;
                MyCourse.Visibility = Visibility.Collapsed;
            } 
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            PageChanged?.Invoke(typeof(ListCourse)); // Gửi thông báo để hiển thị ListCourse
        }

        private void Shoping_card_Click(object sender, RoutedEventArgs e)
        {
            PageChanged?.Invoke(typeof(ShoppingCartPage)); // Gửi thông báo để hiển thị ShoppingCartPage
        }

        private void MyCourse_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PageChanged?.Invoke(typeof(MyCoursePage)); // Gửi thông báo để hiển thị MyCoursePage
        }

        private void Bell_icon_Click(object sender, RoutedEventArgs e)
        {
            PageChanged?.Invoke(typeof(NotificationPage)); // Gửi thông báo để hiển thị NotificationPage
        }

        private void UserProfile_Click(object sender, RoutedEventArgs e)
        {
            PageChanged?.Invoke(typeof(UserProfilePage)); // Gửi thông báo để hiển thị UserProfilePage
        }

        private void ManagerCustomer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        

        private void ManagerEditor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Statistics_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
