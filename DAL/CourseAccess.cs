using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;

namespace DAL
{
    public class CourseAccess : DataBaseAccess
    {
        public void AddDataCourse(CourseCategory courseCategory)
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            using (SqlCommand command = new SqlCommand("proc_addcourse", sqlCon))
            {
                command.CommandType = CommandType.StoredProcedure;
                //                CREATE PROCEDURE proc_addcourse
                //    @Name NVARCHAR(255),
                //    @Description NVARCHAR(MAX),
                //    @Price DECIMAL(10, 2),
                //    @Created_By INT,
                //    @Created_By_Role NVARCHAR(50),
                //    @Created_Date DATETIME
                //AS
                //BEGIN
                //    INSERT INTO Course_Category
                //                (
                //        Name, Description, Price,
                //        Created_By, Created_By_Role, Created_Date,
                //                Is_Deleted
                //                )
                //    VALUES
                //    (
                //        @Name, @Description, @Price,
                //        @Created_By, @Created_By_Role, @Created_Date,
                //        0-- Mặc định chưa bị xóa
                //    )
                //END

                command.Parameters.AddWithValue("@Name", courseCategory.Name);
                command.Parameters.AddWithValue("@Description", courseCategory.Description);
                command.Parameters.AddWithValue("@Price", courseCategory.Price);
                command.Parameters.AddWithValue("@Created_By", courseCategory.Created_By);
                command.Parameters.AddWithValue("@Created_By_Role", courseCategory.Created_By_Role);
                command.Parameters.AddWithValue("@Created_Date", courseCategory.Created_Date);
                int result = command.ExecuteNonQuery();
            }
        }
        public CourseCategory ShowDataCorse(string ID)
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlCommand command = new SqlCommand("proc_showcourse", sqlCon);
            //            CREATE PROCEDURE proc_showcourse
            //    @Category_ID INT
            //AS
            //BEGIN
            //    SELECT *
            //    FROM Course_Category
            //    WHERE Category_ID = @Category_ID
            //END
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Category_ID", ID);
            SqlDataReader reader = command.ExecuteReader();
            CourseCategory course = new CourseCategory();

            if (reader.Read())
            {
                course.Category_ID = Convert.ToString(reader["Category_ID"]);
                course.Name = Convert.ToString(reader["Name"]);
                course.Description = Convert.ToString(reader["Description"]);
                course.Price = Convert.ToDecimal(reader["Price"]);
                course.Created_By = Convert.ToString(reader["Created_By"]);
                course.Created_By_Role = Convert.ToString(reader["Created_By_Role"]);
                course.Created_Date = Convert.ToDateTime(reader["Created_Date"]);

                course.Update_By = reader["Updated_By"] != DBNull.Value ? Convert.ToString(reader["Updated_By"]) : "0";
                course.Update_By_Role = Convert.ToString(reader["Updated_By_Role"]);
                course.Updated_Date = reader["Updated_Date"] != DBNull.Value ? Convert.ToDateTime(reader["Updated_Date"]) : DateTime.MinValue;

                course.Is_Delete = Convert.ToBoolean(reader["Is_Deleted"]);
                course.Delete_At = reader["Deleted_At"] != DBNull.Value ? Convert.ToDateTime(reader["Deleted_At"]) : DateTime.MinValue;

                sqlCon.Close();
                return course;
            }

            sqlCon.Close();
            return null;
        }
        public void DeleteDataCourse(string ID)
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlCommand command = new SqlCommand("proc_deletecourse", sqlCon);
            command.CommandType = CommandType.StoredProcedure;
            //            CREATE PROCEDURE proc_deletecourse
            //    @Category_ID INT
            //AS
            //BEGIN
            //    SET NOCOUNT ON;

            //            UPDATE Course_Category
            //    SET
            //        Is_Deleted = 1,
            //        Deleted_At = GETDATE()
            //    WHERE Category_ID = @CategoryID;
            //            END;
            command.Parameters.AddWithValue("@Category_ID", ID);
            sqlCon.Close();
        }
        public List<CourseCategory> ShowAllDataCourse()
        {
            List<CourseCategory> courseCategories = new List<CourseCategory>();
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.Connection = sqlCon;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowAllCourse";
//            CREATE PROCEDURE ShowAllCourse
//AS
//            BEGIN
//                SELECT
//                    Category_ID, 
//                    Name
//                FROM
//                    Course_Category
//                WHERE
//                    Is_Deleted = 0
//            END
            SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CourseCategory courseCategory = new CourseCategory();
                    courseCategory.Category_ID = reader["Category_ID"].ToString();
                    courseCategory.Name = reader["Name"].ToString();
                    courseCategory.Price = Convert.ToDecimal(reader["Price"]); // Explicit conversion added here  
                    courseCategories.Add(courseCategory);
                }
                
        
            reader.Close();
            sqlCon.Close();
            return courseCategories;
        }
        public void RestoreDataCourse(CourseCategory courseCategory)
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "proc_restorecourse";
            //            CREATE PROCEDURE proc_restorecourse
            //    @CategoryID INT
            //AS
            //BEGIN
            //    SET NOCOUNT ON;

            //            UPDATE Course_Category
            //    SET
            //        Is_Deleted = 0,
            //        Deleted_At = NULL
            //    WHERE Category_ID = @CategoryID AND Is_Deleted = 1;
            //            END;
            command.Connection = sqlCon;
            command.Parameters.AddWithValue("@Category",courseCategory.Category_ID);
            command.ExecuteNonQuery();
            sqlCon.Close();
        }
        public List<CourseCategory> ShowDataDeletedCourse()
        {
            List<CourseCategory> courseCategories = new List<CourseCategory>();
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.Connection = sqlCon;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowDeletedCourse";
            //            CREATE PROCEDURE ShowDeletedCourse
            //AS
            //BEGIN
            //    SELECT
            //        Category_ID, 
            //        Name,
            //        Delete_At
            //    FROM
            //        Course_Category
            //    WHERE
            //        Is_Deleted = 1
            //END
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                CourseCategory courseCategory = new CourseCategory();
                courseCategory.Category_ID = reader["Category_ID"].ToString();
                courseCategory.Name = reader["Name"].ToString();
                courseCategory.Delete_At = (DateTime)reader["Delete_At"];
                courseCategories.Add(courseCategory);
            }
            reader.Close();
            sqlCon.Close();
            return courseCategories;
        }
    }
}
