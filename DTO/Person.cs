using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Person
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gmail { get; set; }
        public string Address { get; set; }
        public string Account_ID { get; set; }
        public Person() { }
        public Person(string id, string name, string phone, string gmail, string address, string accountId)
        {
            ID = id;
            Name = name;
            Phone = phone;
            Gmail = gmail;
            Address = address;
            Account_ID = accountId;
        }
    }
}
