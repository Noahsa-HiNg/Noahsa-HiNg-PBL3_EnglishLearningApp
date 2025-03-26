using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TestResponse
    {
        public int Response_ID { get; set; }
        public int Attempt_ID { get; set; }
        public int Question_ID { get; set; }
        public string Selected_Option { get; set; }
        public bool Is_Correct { get; set; }

        public TestResponse() { }

        public TestResponse(int responseId, int attemptId, int questionId, string selectedOption, bool isCorrect)
        {
            Response_ID = responseId;
            Attempt_ID = attemptId;
            Question_ID = questionId;
            Selected_Option = selectedOption;
            Is_Correct = isCorrect;
        }
    }
}

