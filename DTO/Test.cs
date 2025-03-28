﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Test
    {
        public int Test_ID { get; set; }
        public int Category_ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Time_Limit { get; set; }
        public int Question_Count { get; set; }
        public int Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }

        public Test() { }

        public Test(int testId, int categoryId, string title, string description, int timeLimit, int questionCount, int createdBy, DateTime createdDate, DateTime updatedDate)
        {
            Test_ID = testId;
            Category_ID = categoryId;
            Title = title;
            Description = description;
            Time_Limit = timeLimit;
            Question_Count = questionCount;
            Created_By = createdBy;
            Created_Date = createdDate;
            Updated_Date = updatedDate;
        }
    }
}
