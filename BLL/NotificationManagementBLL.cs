using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NotificationManagementBLL
    {
        NotificationAccess notificationAccess = new NotificationAccess();
        List<Notification> ShowAllNotification()
        {
            List<Notification> notifications = notificationAccess.ShowAllNotificationData();
            return notifications;
        }
        List<Notification> ShowMyNotification(Customer customer)
        {
            List<Notification> notifications = notificationAccess.ShowMyNotificationData(customer);
            return notifications;
        }
        public void SendNotification(Customer customer, Notification notification)
        {
            notificationAccess.SendNotificationData(customer, notification);
        }
        public Notification FindNotification(string NotificationID)
        {
            Notification notification = notificationAccess.FindNotificationData(NotificationID);
            return notification;
        }
    }
}
