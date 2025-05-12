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
    /// Interaction logic for ManagerCoursePage.xaml
    /// </summary>
    public partial class ManagerCoursePage : Page
    {
        public ManagerCoursePage()
        {
            InitializeComponent();
            this.DataContext = new ManagerListCourseViewModel(); // Fixed instantiation issue
        }

        private void NavigateToLessonDetail_Click(object sender, RoutedEventArgs e)
        {
            // Điều hướng đến trang CourseDetailPage
            if (sender is Button button && button.Tag is CourseCategory course)
            {
                // Điều hướng đến CourseDetailPage và truyền dữ liệu
                this.NavigationService?.Navigate(new ManagerCourseDetailPage(course));
            }
        }

        
        private void Add_Course_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
