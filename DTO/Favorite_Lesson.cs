using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FavoriteLesson
    {
        public int Favorite_ID { get; set; }
        public int Lesson_ID { get; set; }
        public int Customer_ID { get; set; }

        public FavoriteLesson() { }

        public FavoriteLesson(int favoriteId, int lessonId, int customerId)
        {
            Favorite_ID = favoriteId;
            Lesson_ID = lessonId;
            Customer_ID = customerId;
        }
    }
}

