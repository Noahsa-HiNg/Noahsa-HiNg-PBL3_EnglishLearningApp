using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Comment
    {
        public int Comment_ID { get; set; }
        public int Parent_Comment_ID { get; set; }
        public int Customer_ID { get; set; }
        public int Lesson_ID { get; set; }
        public string Content { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime Update_Date { get; set; }


        public Comment() { }

        public Comment(int commentId,int parent_comment_id, int customerId, int lessonId, string content, DateTime create_date,DateTime update_date)
        {
            Comment_ID = commentId;
            Parent_Comment_ID = parent_comment_id;
            Customer_ID = customerId;
            Lesson_ID = lessonId;
            Content = content;
            Create_Date = create_date;
            Update_Date = update_date;
        }
    }
}

