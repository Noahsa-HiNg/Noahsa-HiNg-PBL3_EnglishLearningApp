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
        public string Avatar { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }

        public Account() { }

        public Account(string id, string username, string password,string avatar,string role, int status)
        {
            ID = id;
            Username = username;
            Avatar = avatar;
            Password = password;
            Role = role;
            Status = status;

        }
    }
}
