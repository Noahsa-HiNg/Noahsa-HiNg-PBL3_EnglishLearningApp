using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class CourseManagementBLL
    {
        CourseAccess courseAccess = new CourseAccess();
        LessonManagementBLL lessonManagementBLL = new LessonManagementBLL();
        public void AddCourse(CourseCategory courseCategory)
        {
            courseAccess.AddDataCourse(courseCategory);
        }
        public (CourseCategory,Lesson[]) ShowCourse(CourseCategory courseCategory)
        {
            CourseCategory course = courseAccess.ShowDataCorse(courseCategory.Category_ID);
            Lesson[] lessons = lessonManagementBLL.ShowLesOfCourse(courseCategory.Category_ID);
            return (course,lessons);
        }
        public void DeleteCourse(CourseCategory courseCategory)
        {
            courseAccess.DeleteDataCourse(courseCategory.Category_ID);
            lessonManagementBLL.DeleteLesOfCoures(courseCategory.Category_ID);
        }
        public CourseCategory[] ShowAllCourse()
        {
            List<CourseCategory> courseCategories = new List<CourseCategory>();
            courseCategories = courseAccess.ShowAllDataCourse();
            return courseCategories.ToArray();
        }
        public void RestoreCourse(CourseCategory courseCategory)
        {
            courseAccess.RestoreDataCourse(courseCategory);
            lessonManagementBLL.RestoreLesOfCourse(courseCategory.Category_ID);
        }
        public List<CourseCategory> SearchCourse(string searchTerm, List<CourseCategory> courseCategories)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return courseCategories;

            searchTerm = searchTerm.ToLower();

            var filtered = courseCategories
                .Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.ToLower().Contains(searchTerm)
                         || !string.IsNullOrEmpty(c.Description) && c.Description.ToLower().Contains(searchTerm))
                .ToList();

            return filtered;
        }
        public List<CourseCategory> Cou
    }
}
