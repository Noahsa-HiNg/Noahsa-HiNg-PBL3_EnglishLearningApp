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
        public int Customer_ID { get; set; }
        public int Lesson_ID { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        public Comment() { }

        public Comment(int commentId, int customerId, int lessonId, string content, DateTime timestamp)
        {
            Comment_ID = commentId;
            Customer_ID = customerId;
            Lesson_ID = lessonId;
            Content = content;
            Timestamp = timestamp;
        }
    }
}

