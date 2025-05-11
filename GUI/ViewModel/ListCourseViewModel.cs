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
        public ListCourseViewModel()
        {
            CourseManagementBLL courseManagementBLL = new CourseManagementBLL();
            CourseCategory[] courseCategories = courseManagementBLL.ShowAllCourse();
            Courses = new ObservableCollection<CourseCategory>(courseCategories);
        }

    }
}
