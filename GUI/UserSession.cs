using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class UserSession
    {
        private static UserSession _instance;

        // Singleton instance
        public static UserSession Instance => _instance ??= new UserSession();

        // Private constructor to prevent instantiation
        private UserSession() { }

        // Properties to store user information
        public string ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(ID);
    }
}
