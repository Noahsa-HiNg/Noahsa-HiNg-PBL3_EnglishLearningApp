using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Runtime.Remoting.Messaging;
namespace BLL
{
    class LessonManagementBLL:LessonValidator
    {
        LessonAccess lessonAccess = new LessonAccess();
        public string AddLesson(Lesson lesson)
        {
            if (CheckTitle(lesson.Title) == "valid_true")
            {
                lessonAccess.AddDataLesson(lesson);
            }
            return "Title_to-long";

        }
        public Lesson ShowLesson(Lesson lesson)
        {
            Lesson les = lessonAccess.ShowDataLesson(lesson.Lesson_ID);
            return les;
        }
        public string Deletelesson(Lesson lesson)
        {
            string result = lessonAccess.DeleteDataLesson(lesson.Lesson_ID);
            return result;
        }
    }
}
