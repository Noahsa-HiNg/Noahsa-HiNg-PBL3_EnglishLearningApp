using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class Test
    {
        public int Test_ID { get; set; }
        public int Level { get; set; }
        public int Number_Of_Question { get; set; }
        public int Time_Of_Test { get; set; }

        public Test() { }

        public Test(int testId, int level, int numberOfQuestion, int timeOfTest)
        {
            Test_ID = testId;
            Level = level;
            Number_Of_Question = numberOfQuestion;
            Time_Of_Test = timeOfTest;
        }
    }
}
