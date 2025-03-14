using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class TestHistory
    {
        public string ID { get; set; }
        public string Test_ID { get; set; }
        public string Customer_ID { get; set; }
        public DateTime Timestamp { get; set; }
        public int Mark { get; set; }

        public TestHistory() { }

        public TestHistory(string id, string testId, string customerId, DateTime timestamp, int mark)
        {
            ID = id;
            Test_ID = testId;
            Customer_ID = customerId;
            Timestamp = timestamp;
            Mark = mark;
        }
    }
}
