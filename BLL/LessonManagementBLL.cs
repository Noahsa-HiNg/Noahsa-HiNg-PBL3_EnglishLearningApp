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
            return "Title_to_long";

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
        public Lesson[] ShowAllLesson()
        {
            List<Lesson> lessons = new List<Lesson>();
            lessons = lessonAccess.ShowAllDataLesson();
            return lessons.ToArray();
        }
        public void RestoreLesson(Lesson lesson)
        {
            lessonAccess.RestoreDataLesson(lesson);
        }
        public Lesson[] ShowDeleteLesson()
        {
            List<Lesson> lessons = new List<Lesson>();
            lessons = lessonAccess.ShowDataDeleteLesson();
            return lessons.ToArray();
        }
        public void DeleteLesOfCoures(string CategoryID)
        {
            lessonAccess.DeleteLesOfCourse(CategoryID);
        }
        public Lesson[] ShowLesOfCourse(string CategoryID)
        {
            List<Lesson> lessons = new List<Lesson>();
            lessons = lessonAccess.ShowDataLesOfCourse(CategoryID);
            return lessons.ToArray();
        } 
        public void RestoreLesOfCourse(string CategoryId)
        {
            lessonAccess.RestoreLesOfCourse(CategoryId);
        }
    }
}
