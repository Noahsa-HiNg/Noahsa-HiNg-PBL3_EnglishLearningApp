using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class Administrator : Person
    {
        public Administrator() { }
        public Administrator(string id, string name, string phone, string gmail, string address, string accountId)
        : base(id, name, phone, gmail, address, accountId) { }
    }
}
