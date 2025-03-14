using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class Account
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }

        public Account() { }

        public Account(string id, string username, string password, int status)
        {
            ID = id;
            Username = username;
            Password = password;
            Status = status;
        }
    }
}
