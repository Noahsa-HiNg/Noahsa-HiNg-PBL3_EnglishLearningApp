using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DTO;
using DAL;
using System.Collections.ObjectModel;
namespace GUI.ViewModel
{
    
    internal class MyListCourseViewModel
    {
        public ObservableCollection<CourseCategory> Courses { get; set; }
        public MyListCourseViewModel()
        {
            CourseManagementBLL courseManagementBLL = new CourseManagementBLL();
            CourseCategory[] courseCategories = courseManagementBLL.ShowAllCourse();
            Courses = new ObservableCollection<CourseCategory>(courseCategories);
        }
        //mylistcourse được gọi
        
    }
}
