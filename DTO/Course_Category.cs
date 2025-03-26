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
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }

        public CourseCategory() { }

        public CourseCategory(int categoryId, string name, string description, decimal price, int createdBy, DateTime createdDate, DateTime updatedDate)
        {
            Category_ID = categoryId;
            Name = name;
            Description = description;
            Price = price;
            Created_By = createdBy;
            Created_Date = createdDate;
            Updated_Date = updatedDate;
        }
    }
}
