using System;
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
        public string Create_By_Role { get; set; }
        public DateTime Created_Date { get; set; }
        public int Update_By { get; set; }
        public string Update_By_Role { get; set; }
        public DateTime Updated_Date { get; set; }

        public Test() { }

        public Test(int testId, int categoryId, string title, string description, int timeLimit, int questionCount, int createdBy,string create_by_role, DateTime createdDate,int update_by,string update_by_role, DateTime updatedDate)
        {
            Test_ID = testId;
            Category_ID = categoryId;
            Title = title;
            Description = description;
            Time_Limit = timeLimit;
            Question_Count = questionCount;
            Created_By = createdBy;
            Create_By_Role = create_by_role;
            Created_Date = createdDate;
            Update_By = update_by;
            Update_By_Role = update_by_role;
            Updated_Date = updatedDate;

        }
    }
}
