using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Administrator : Person
    {
        public Administrator() { }

        public Administrator(string id, string accountId, string name, string phone, string email)
            : base(id, accountId, name, phone, email)
        {
        }
    }
}