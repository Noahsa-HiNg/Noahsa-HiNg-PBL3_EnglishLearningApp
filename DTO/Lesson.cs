﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Lesson
    {
        public string Lesson_ID { get; set; }
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
        public bool Is_Delete { get; set; }
        public DateTime Delete_At { get; set; }
        public Lesson() { }

        public Lesson(string lessonId, int categoryId, string title, string description, string videoUrl, string example, int createdBy,string create_by_role, DateTime createdDate, int update_by,string update_by_role, DateTime updatedDate, DateTime delete_at, bool is_delete)
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
            Delete_At = delete_at;
            Is_Delete = is_delete;
        }
    }
}

