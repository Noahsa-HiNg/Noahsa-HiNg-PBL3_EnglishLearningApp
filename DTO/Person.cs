using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DTO
{
    public class Person
    {
        public int  ID { get; set; }  
        public int Account_ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Person() { }

        public Person(int id,int accountId, string name, string phone, string email)
        {
            ID = id;
            Account_ID = accountId;
            Name = name;
            Phone = phone;
            Email = email;
        }
    }
}
