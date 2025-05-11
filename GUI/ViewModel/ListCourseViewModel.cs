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

            //Courses = new ObservableCollection<CourseCategory>
            //{
            //   new CourseCategory
            //   {
            //       Category_ID = "C001",
            //       Name = "Khóa học C#",
            //       Description = "Học lập trình C# từ cơ bản đến nâng cao",
            //       Price = 199.99m,
            //       Created_By = 1,
            //       Created_By_Role = "Admin",
            //       Update_By = 2,
            //       Update_By_Role = "Editor",
            //       Created_Date = DateTime.Now.AddMonths(-2),
            //       Updated_Date = DateTime.Now.AddMonths(-1),
            //       Delete_At = DateTime.MinValue,
            //       Is_Delete = false
            //   },
            //   new CourseCategory
            //   {
            //       Category_ID = "C002",
            //       Name = "Khóa học WPF",
            //       Description = "Xây dựng ứng dụng desktop với WPF",
            //       Price = 149.99m,
            //       Created_By = 1,
            //       Created_By_Role = "Admin",
            //       Update_By = 3,
            //       Update_By_Role = "Moderator",
            //       Created_Date = DateTime.Now.AddMonths(-3),
            //       Updated_Date = DateTime.Now.AddMonths(-2),
            //       Delete_At = DateTime.MinValue,
            //       Is_Delete = false
            //   },
            //   new CourseCategory
            //   {
            //       Category_ID = "C003",
            //       Name = "Khóa học ASP.NET",
            //       Description = "Phát triển web với ASP.NET Core",
            //       Price = 249.99m,
            //       Created_By = 2,
            //       Created_By_Role = "Editor",
            //       Update_By = 3,
            //       Update_By_Role = "Moderator",
            //       Created_Date = DateTime.Now.AddMonths(-4),
            //       Updated_Date = DateTime.Now.AddMonths(-3),
            //       Delete_At = DateTime.MinValue,
            //       Is_Delete = false
            //   }
            //};

        }
    }
}
