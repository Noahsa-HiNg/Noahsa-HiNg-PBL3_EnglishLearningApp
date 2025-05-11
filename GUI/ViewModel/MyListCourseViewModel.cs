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
            Account account = new Account();
            User_AccountManagementBLL user_AccountManagementBLL = new User_AccountManagementBLL();
            account = user_AccountManagementBLL.FindAccount(UserSession.Instance.ID);
            List<CourseCategory> courseCategories = courseManagementBLL.ShowBuyCourse(account);
            Courses = new ObservableCollection<CourseCategory>(courseCategories);
        }
        //mylistcourse được gọi
        
    }
}
