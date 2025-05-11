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
using DTO;
using GUI.ViewModel;

namespace GUI
{
    /// <summary>
    /// Interaction logic for ShoppingCartPage.xaml
    /// </summary>
    public partial class ShoppingCartPage : Page
    {
        public ShoppingCartPage()
        {
            InitializeComponent();
            this.DataContext = new ShoppingCartCourseViewModel();
        }
        private void NavigateToCourseDetail_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Điều hướng đến trang CourseDetailPage
            if (sender is Button button && button.Tag is CourseCategory course)
            {
                // Điều hướng đến CourseDetailPage và truyền dữ liệu
                this.NavigationService?.Navigate(new CourseDetailPage(course));
            }
        }
    }
}
