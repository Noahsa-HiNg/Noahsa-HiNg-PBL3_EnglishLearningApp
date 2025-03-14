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
    class Customer : Person
    {
        public string Status;
        public Customer() { }
        public Customer(string id, string name, string phone, string gmail, string address, string accountId)
        : base(id, name, phone, gmail, address, accountId) { }
    }
}
