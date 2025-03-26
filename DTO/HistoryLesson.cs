using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HistoryLesson
    {
        public int History_ID { get; set; }
        public int Lesson_ID { get; set; }
        public int Customer_ID { get; set; }
        public DateTime Time_Stamp { get; set; }
        public double Progress { get; set; }

        public HistoryLesson() { }

        public HistoryLesson(int historyId, int lessonId, int customerId, DateTime timeStamp, double progress)
        {
            History_ID = historyId;
            Lesson_ID = lessonId;
            Customer_ID = customerId;
            Time_Stamp = timeStamp;
            Progress = progress;
        }
    }
}

