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

        public Administrator(int id, int accountId, string name, string phone, string email)
            : base(id, accountId, name, phone, email)
        {
        }
    }
}