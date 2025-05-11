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
            Account account = new Account();
            User_AccountManagementBLL user_AccountManagementBLL = new User_AccountManagementBLL();
            account = user_AccountManagementBLL.FindAccount(UserSession.Instance.ID);
            List<CourseCategory> courseCategories = courseManagementBLL.ShowUnBoughtCourse(account);
            Courses = new ObservableCollection<CourseCategory>(courseCategories);
        }
    }
}
