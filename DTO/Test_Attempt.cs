using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TestAttempt
    {
        public int Attempt_ID { get; set; }
        public int Customer_ID { get; set; }
        public int Test_ID { get; set; }
        public DateTime Start_Time { get; set; }
        public DateTime End_Time { get; set; }
        public double Score { get; set; }
        public string Status { get; set; }
        public int Elapsed_Time { get; set; }
        public bool Can_Resume { get; set; }

        public TestAttempt() { }

        public TestAttempt(int attemptId, int customerId, int testId, DateTime startTime, DateTime endTime, double score, string status,int elapsed_time,bool can_resume)
        {
            Attempt_ID = attemptId;
            Customer_ID = customerId;
            Test_ID = testId;
            Start_Time = startTime;
            End_Time = endTime;
            Score = score;
            Status = status;
            Elapsed_Time = elapsed_time;
            Can_Resume = can_resume;

        }
    }
}

