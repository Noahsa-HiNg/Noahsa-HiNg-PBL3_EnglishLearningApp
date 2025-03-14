using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class Payment
    {
        public int Lesson_ID { get; set; }
        public int Customer_ID { get; set; }
        public int Amount { get; set; }
        public bool Status { get; set; }

        public Payment() { }

        public Payment(int lessonId, int customerId, int amount, bool status)
        {
            Lesson_ID = lessonId;
            Customer_ID = customerId;
            Amount = amount;
            Status = status;
        }
    }
}
