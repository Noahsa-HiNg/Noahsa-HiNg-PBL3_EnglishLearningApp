using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class WishList
    {
        public string ID { get; set; }
        public string Lesson_ID { get; set; }
        public string Customer_ID { get; set; }

        public WishList() { }

        public WishList(string id, string lessonId, string customerId)
        {
            ID = id;
            Lesson_ID = lessonId;
            Customer_ID = customerId;
        }
    }
}
