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
            if (UserSession.Instance.Role != "Admin")
            {
                // Hide admin-only buttons
                Statistics.Visibility = Visibility.Collapsed;
                ManagerEditor.Visibility = Visibility.Collapsed;
            }
            if (UserSession.Instance.Role != "Editor" && UserSession.Instance.Role != "Admin")
            {
                // Hide admin-only buttons
                ManagerCustomer.Visibility = Visibility.Collapsed;            
            }
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

        private void ManagerCustomer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToPage(new ManagerCustomerPage());
        }

        private void ManagerEditor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToPage(new ManagerEditorPage());
        }

        private void Statistics_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToPage(new StatisticsPage());
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox != null)
            {
                string searchText = searchBox.Text;
                FindCourseByText(searchText);
            }
        }
        private void FindCourseByText(string searchText)
        {
            // Lấy MainWindow và MainFrame
            var mainWindow = Application.Current.MainWindow as MainWindow;
            var currentPage = mainWindow?.MainFrame.Content;

            if (currentPage is ListCourse listCoursePage)
            {
                
                
            }
            else if (currentPage is ShoppingCartPage shoppingCartPage)
            {
                
                
            }
            else if (currentPage is MyCoursePage myCoursePage)
            {
                // Gọi phương thức tìm kiếm của MyCoursePage
                
            }
            
        }
    }
}
