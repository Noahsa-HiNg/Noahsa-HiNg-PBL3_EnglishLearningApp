using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class LessonBuyer
    {
        public int Lesson_Buyer_ID { get; set; }
        public int Lesson_ID { get; set; }
        public int Customer_ID { get; set; }

        public LessonBuyer() { }

        public LessonBuyer(int lessonBuyerId, int lessonId, int customerId)
        {
            Lesson_Buyer_ID = lessonBuyerId;
            Lesson_ID = lessonId;
            Customer_ID = customerId;
        }
    }
}
