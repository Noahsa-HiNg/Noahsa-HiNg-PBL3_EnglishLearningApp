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
using System.Reflection;

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
                    courseCategory.Description = reader["Description"].ToString();
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
        public List<CourseCategory> ShowBuyCourseData(Account account)
        {
            List<CourseCategory> courseCategories = new List<CourseCategory>();
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            // Create the SQL command for the stored procedure
            SqlCommand command = new SqlCommand();
            command.Connection = sqlCon;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowBuyCourse";
            //            CREATE PROCEDURE ShowBuyCourse
            //    @AccountID INT
            //AS
            //BEGIN
            //    SELECT
            //        c.Category_ID,
            //        c.Name,
            //        c.Description,
            //        c.Price,
            //        c.Created_By,
            //        c.Created_By_Role,
            //        c.Created_Date,
            //        c.Updated_By,
            //        c.Updated_By_Role,
            //        c.Updated_Date,
            //        c.Is_Deleted,
            //        c.Deleted_At
            //    FROM
            //        Course_Category c
            //    INNER JOIN
            //        Payment p ON c.Category_ID = p.Category_ID
            //    WHERE
            //        p.Account_ID = @AccountID
            //    AND
            //        p.Is_Deleted = 0
            //END

            // Add parameters for the stored procedure (if necessary, e.g., account ID)
            command.Parameters.AddWithValue("@AccountID", account.ID);

            // Execute the query and read the data
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Create a new CourseCategory object and populate its properties
                CourseCategory category = new CourseCategory
                {
                    Category_ID = reader.GetOrdinal("Category_ID").ToString(),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    Created_By = reader.GetOrdinal("Created_By").ToString(),
                    Created_By_Role = reader.GetString(reader.GetOrdinal("Created_By_Role")),
                    Created_Date = reader.GetDateTime(reader.GetOrdinal("Created_Date")),
                    Update_By = reader.GetOrdinal("Updated_By").ToString(),
                    Update_By_Role = reader.GetString(reader.GetOrdinal("Updated_By_Role")),
                    Updated_Date = reader.GetDateTime(reader.GetOrdinal("Updated_Date")),
                    Is_Delete = reader.GetBoolean(reader.GetOrdinal("Is_Deleted")),
                    Delete_At = reader["Deleted_At"] != DBNull.Value ? Convert.ToDateTime(reader["Deleted_At"]) : DateTime.MinValue,
                };

                // Add the populated object to the list
                courseCategories.Add(category);
            }

            // Close the reader and connection
            reader.Close();
            sqlCon.Close();

            return courseCategories;
        }
        public List<CourseCategory> ShowUnBoughtCoursesData(Account account)
        {
            List<CourseCategory> courseCategories = new List<CourseCategory>();
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            // Create the SQL command for the stored procedure
            SqlCommand command = new SqlCommand();
            command.Connection = sqlCon;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowUnBoughtCourses";
            //            CREATE PROCEDURE ShowUnBoughtCourses
            //    @AccountID INT
            //AS
            //BEGIN
            //    SELECT
            //        c.Category_ID,
            //        c.Name,
            //        c.Description,
            //        c.Price,
            //        c.Created_By,
            //        c.Created_By_Role,
            //        c.Created_Date,
            //        c.Updated_By,
            //        c.Updated_By_Role,
            //        c.Updated_Date,
            //        c.Is_Deleted,
            //        c.Deleted_At
            //    FROM
            //        Course_Category c
            //    WHERE NOT EXISTS(
            //        SELECT 1
            //        FROM Payment p
            //        WHERE p.Account_ID = @AccountID AND p.Category_ID = c.Category_ID
            //    )
            //    AND c.Is_Deleted = 0-- Make sure that the course is not marked as deleted
            //END

            // Add parameters for the stored procedure (e.g., account ID)
            command.Parameters.AddWithValue("@AccountID", account.ID);

            // Execute the query and read the data
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Create a new CourseCategory object and populate its properties
                CourseCategory category = new CourseCategory
                {
                    Category_ID = reader.GetOrdinal("Category_ID").ToString(),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    Created_By = reader.GetOrdinal("Created_By").ToString(),
                    Created_By_Role = reader.GetString(reader.GetOrdinal("Created_By_Role")),
                    Created_Date = reader.GetDateTime(reader.GetOrdinal("Created_Date")),
                    Update_By = reader.GetOrdinal("Updated_By").ToString(),
                    Update_By_Role = reader.GetString(reader.GetOrdinal("Updated_By_Role")),
                    Updated_Date = reader.GetDateTime(reader.GetOrdinal("Updated_Date")),
                    Is_Delete = reader.GetBoolean(reader.GetOrdinal("Is_Deleted")),
                    Delete_At = reader["Deleted_At"] != DBNull.Value ? Convert.ToDateTime(reader["Deleted_At"]) : DateTime.MinValue,
                };

                // Add the populated object to the list
                courseCategories.Add(category);
            }

            // Close the reader and connection
            reader.Close();
            sqlCon.Close();

            return courseCategories;
        }


        /// SUGGESTED ADDITIONS FOR USER PROFILE VIEW
        /// Bạn cần triển khai logic DB chi tiết cho các phương thức này
        /// (ví dụ: sử dụng stored procedure GetRegisteredCoursesByAccountId và GetFavoriteCoursesByAccountId)

        public List<CourseCategory> GetRegisteredCoursesByAccountId(string accountId)
        {
            List<CourseCategory> registeredCourses = new List<CourseCategory>();
            SqlConnection sqlCon = SqlconnectionData.connnect();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                // Thay đổi Stored Procedure nếu cần thiết, ví dụ: proc_get_user_registered_courses
                SqlCommand command = new SqlCommand("proc_get_user_registered_courses", sqlCon);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AccountId", accountId); // Giả sử SP nhận AccountId

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CourseCategory course = new CourseCategory
                    {
                        Category_ID = reader["Category_ID"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        // Ánh xạ các thuộc tính khác của CourseCategory
                        // Ví dụ: Created_Date = Convert.ToDateTime(reader["Created_Date"])
                    };
                    registeredCourses.Add(course);
                }
            }
            catch (Exception ex)
            {
                // Log lỗi hoặc xử lý ngoại lệ
                Console.WriteLine("Error getting registered courses: " + ex.Message);
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return registeredCourses;
        }

        public List<CourseCategory> GetFavoriteCoursesByAccountId(string accountId)
        {
            List<CourseCategory> favoriteCourses = new List<CourseCategory>();
            SqlConnection sqlCon = SqlconnectionData.connnect();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                // Thay đổi Stored Procedure nếu cần thiết, ví dụ: proc_get_user_favorite_courses
                SqlCommand command = new SqlCommand("proc_get_user_favorite_courses", sqlCon);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AccountId", accountId); // Giả sử SP nhận AccountId

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CourseCategory course = new CourseCategory
                    {
                        Category_ID = reader["Category_ID"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        // Ánh xạ các thuộc tính khác của CourseCategory
                        // Ví dụ: Created_Date = Convert.ToDateTime(reader["Created_Date"])
                    };
                    favoriteCourses.Add(course);
                }
            }
            catch (Exception ex)
            {
                // Log lỗi hoặc xử lý ngoại lệ
                Console.WriteLine("Error getting favorite courses: " + ex.Message);
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return favoriteCourses;
        }

    }
}
