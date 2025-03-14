using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class History
    {
        public string ID { get; set; }
        public string Lesson_ID { get; set; }
        public string Customer_ID { get; set; }
        public DateTime Time { get; set; }

        public History() { }

        public History(string id, string lessonId, string customerId, DateTime time)
        {
            ID = id;
            Lesson_ID = lessonId;
            Customer_ID = customerId;
            Time = time;
        }
    }
}
