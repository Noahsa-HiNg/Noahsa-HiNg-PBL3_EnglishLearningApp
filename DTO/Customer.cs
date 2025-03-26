using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DTO
{
    public class Customer : Person
    {
        public bool Notification_Enable { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }

        public Customer() { }

        public Customer(int id, int accountId, string name, string phone, string email, bool notificationEnable, DateTime createdDate, DateTime updatedDate)
            : base(id, accountId, name, phone, email)
        {
            Notification_Enable = notificationEnable;
            Created_Date = createdDate;
            Updated_Date = updatedDate;
        }
    }
}

