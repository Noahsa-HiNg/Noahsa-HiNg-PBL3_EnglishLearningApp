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
        public bool Is_Delete { get; set; }
        public DateTime Delete_At { get; set; }
        public Customer() { }

        public Customer(string id, string accountId, string name, string phone, string email, bool notificationEnable, DateTime createdDate, DateTime updatedDate, DateTime delete_At, bool is_Delete)
            : base(id, accountId, name, phone, email)
        {
            Notification_Enable = notificationEnable;
            Created_Date = createdDate;
            Updated_Date = updatedDate;
            Delete_At = delete_At;
            Is_Delete = is_Delete;
        }

    }   
}

