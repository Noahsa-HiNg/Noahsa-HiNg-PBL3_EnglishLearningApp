using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NotificationRecipient
    {
        public int Notification_ID { get; set; }
        public int Customer_ID { get; set; }
        public DateTime Received_At { get; set; }

        public NotificationRecipient() { }

        public NotificationRecipient(int notificationId, int customerId, DateTime receivedAt)
        {
            Notification_ID = notificationId;
            Customer_ID = customerId;
            Received_At = receivedAt;
        }
    }
}

