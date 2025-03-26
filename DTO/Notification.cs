using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Notification
    {
        public int Notification_ID { get; set; }
        public int Created_By { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created_Date { get; set; }

        public Notification() { }

        public Notification(int notificationId, int createdBy, string title, string content, DateTime createdDate)
        {
            Notification_ID = notificationId;
            Created_By = createdBy;
            Title = title;
            Content = content;
            Created_Date = createdDate;
        }
    }
}

