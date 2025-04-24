using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Editor : Person
    {
        public string Permissions { get; set; }
        public string Status { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }
        public DateTime Delete_At { get; set; }
        public Editor() { }

        public Editor(string id, string accountId, string name, string phone, string email, string permissions, string status, DateTime createdDate, DateTime updatedDate, DateTime delete_At)
            : base(id, accountId, name, phone, email)
        {
            Permissions = permissions;
            Status = status;
            Created_Date = createdDate;
            Updated_Date = updatedDate;
            Delete_At = delete_At;
        }
    }
}