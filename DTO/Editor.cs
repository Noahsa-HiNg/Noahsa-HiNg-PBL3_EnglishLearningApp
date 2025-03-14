using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class Editor : Person
    {
        public string Status;
        public Editor() { }
        public Editor(string id, string name, string phone, string gmail, string address, string accountId, string status)
        : base(id, name, phone, gmail, address, accountId)
        {
            Status = status;
        }
    }
}
