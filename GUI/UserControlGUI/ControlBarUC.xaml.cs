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
        public ControlBarUC()
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new ControlBarViewModel();
        }
        private void NavigateToPage(Page page)
        {
            // Tìm Frame trong MainWindow và điều hướng đến Page
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(page);
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new ListCourse());
        }
        private void Shoping_card_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new ShoppingCartPage());
        }
        private void MyCourse_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigateToPage(new MyCoursePage());
        }
        private void Bell_icon_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new NotificationPage());
        }

        private void UserProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new UserProfilePage());
        }
    }
}
