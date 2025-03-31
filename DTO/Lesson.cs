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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Video_Url { get; set; }
        public string Example { get; set; }
        public int Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }

        public Lesson() { }

        public Lesson(int lessonId, int categoryId, string name, string description, string videoUrl, string example, int createdBy, DateTime createdDate, DateTime updatedDate)
        {
            Lesson_ID = lessonId;
            Category_ID = categoryId;
            Name = name;
            Description = description;
            Video_Url = videoUrl;
            Example = example;
            Created_By = createdBy;
            Created_Date = createdDate;
            Updated_Date = updatedDate;
        }
    }
}

