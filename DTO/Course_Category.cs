using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CourseCategory
    {
        public int Category_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Created_By { get; set; }
        public string Created_By_Role { get; set; }
        public int Update_By { get;set }
        public string Update_By_Role { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }

        public CourseCategory() { }

        public CourseCategory(int categoryId, string name, string description, decimal price, int createdBy,string created_by_role,int update_by,string update_by_role, DateTime createdDate, DateTime updatedDate)
        {
            Category_ID = categoryId;
            Name = name;
            Description = description;
            Price = price;
            Created_By = createdBy;
            Created_By_Role = created_by_role;
            Update_By = update_by;
            Update_By_Role = update_by_role;
            Created_Date = createdDate;
            Updated_Date = updatedDate;
        }
    }
}
