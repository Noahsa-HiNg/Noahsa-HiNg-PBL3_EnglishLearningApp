using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using BLL;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GUI.ViewModel
{
    class ListCourseViewModel
    {
        public ObservableCollection<CourseCategory> Courses { get; set; }
        public ICommand NavigateToDetailCommand { get; }
        public ListCourseViewModel()
        {
            CourseManagementBLL courseManagementBLL = new CourseManagementBLL();
            CourseCategory[] courseCategories = courseManagementBLL.ShowAllCourse();
            Courses = new ObservableCollection<CourseCategory>(courseCategories);
            NavigateToDetailCommand = new RelayCommand<CourseCategory>(NavigateToDetail);
        }
        private void NavigateToDetail(CourseCategory course)
        {
            // Điều hướng đến CourseDetailPage
            var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(new CourseDetailPage(course));
        }
    }
}
