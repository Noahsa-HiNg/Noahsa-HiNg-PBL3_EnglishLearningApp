using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class Notification
    {
        public string ID { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }

        public Notification() { }

        public Notification(string id, string message, string status)
        {
            ID = id;
            Message = message;
            Status = status;
        }
    }
}
