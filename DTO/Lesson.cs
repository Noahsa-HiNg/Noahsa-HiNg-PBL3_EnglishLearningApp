using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Lesson
    {
        public int Lesson_ID { get; set; }
        public int Category_ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Video_Url { get; set; }
        public string Example { get; set; }
        public int Created_By { get; set; }
        public string Create_By_Role { get; set; }
        public DateTime Created_Date { get; set; }
        public int Update_By { get; set; }
        public DateTime Updated_Date { get; set; }
        public string Update_By_Role { get; set; }

        public Lesson() { }

        public Lesson(int lessonId, int categoryId, string title, string description, string videoUrl, string example, int createdBy,string create_by_role, DateTime createdDate, int update_by,string update_by_role, DateTime updatedDate)
        {
            Lesson_ID = lessonId;
            Category_ID = categoryId;
            Title = title;
            Description = description;
            Video_Url = videoUrl;
            Example = example;
            Created_By = createdBy;
            Create_By_Role = create_by_role;
            Created_Date = createdDate;
            Update_By = update_by;
            Update_By_Role = update_by_role;
            Updated_Date = updatedDate;
        }
    }
}

