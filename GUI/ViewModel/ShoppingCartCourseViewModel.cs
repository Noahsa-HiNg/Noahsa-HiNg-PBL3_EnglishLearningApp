using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DTO;

namespace GUI.ViewModel
{
    internal class ShoppingCartCourseViewModel
    {
        public ObservableCollection<CourseCategory> Courses { get; set; }
        public ShoppingCartCourseViewModel()
        {
            CourseManagementBLL courseManagementBLL = new CourseManagementBLL();
            CourseCategory[] courseCategories = courseManagementBLL.ShowAllCourse();
            Courses = new ObservableCollection<CourseCategory>(courseCategories);
        }
    }
}
