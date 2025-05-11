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
    internal class ListLessonViewModel
    {
        public ObservableCollection<Lesson> Lessons { get; set; }
        public ListLessonViewModel(CourseCategory course)
        {
            LessonManagementBLL lessonManagementBLL = new LessonManagementBLL();
            var lessons = lessonManagementBLL.ShowLesOfCourse(course.Category_ID);
            Lessons = new ObservableCollection<Lesson>(lessons);
        }
        private void NavigateToDetail(Lesson lesson)
        {
            // Điều hướng đến CourseDetailPage
            var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(new LessonDetailPage(lesson));
        }
    }
}
