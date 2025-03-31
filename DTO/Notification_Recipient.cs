using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NotificationRecipient
    {
        public int Recipient_ID { get; set; }
        public int Notification_ID { get; set; }
        public int Customer_ID { get; set; }
        public bool Is_Read { get; set; }
        public DateTime Received_At { get; set; }

        public NotificationRecipient() { }

        public NotificationRecipient(int recipientId, int notificationId, int customerId, bool isRead, DateTime receivedAt)
        {
            Recipient_ID = recipientId;
            Notification_ID = notificationId;
            Customer_ID = customerId;
            Is_Read = isRead;
            Received_At = receivedAt;
        }
    }
}

