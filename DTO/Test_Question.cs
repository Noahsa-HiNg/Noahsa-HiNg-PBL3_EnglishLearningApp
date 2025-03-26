using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TestQuestion
    {
        public int Question_ID { get; set; }
        public int Test_ID { get; set; }
        public string Question_Text { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Correct_Answer { get; set; }

        public TestQuestion() { }

        public TestQuestion(int questionId, int testId, string questionText, string option1, string option2, string option3, string option4, string correctAnswer)
        {
            Question_ID = questionId;
            Test_ID = testId;
            Question_Text = questionText;
            Option1 = option1;
            Option2 = option2;
            Option3 = option3;
            Option4 = option4;
            Correct_Answer = correctAnswer;
        }
    }
}

