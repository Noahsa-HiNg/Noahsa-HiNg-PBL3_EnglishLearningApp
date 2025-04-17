using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LessonAccess : DataBaseAccess
    {
        public string AddDataLesson(Lesson lesson)
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlCommand command = new SqlCommand("proc_addlesson", sqlCon);
            command.CommandType = CommandType.StoredProcedure;
//            Procedure
//            CREATE PROCEDURE proc_addlesson
//    @Category_ID INT,
//    @Title NVARCHAR(255),
//    @Description NVARCHAR(MAX) = NULL,
//    @Video_URL NVARCHAR(MAX) = NULL,
//    @Example NVARCHAR(MAX) = NULL,
//    @Created_By INT,
//    @Created_By_Role NVARCHAR(10) = NULL,
//    @Created_Date DATETIME,
//    @Updated_By INT = NULL,
//    @Updated_By_Role NVARCHAR(10) = NULL,
//    @Updated_Date DATETIME = NULL,
//    @Is_Deleted BIT
//AS
//BEGIN
//    INSERT INTO Lesson(
//        Category_ID, Title, Description, Video_URL, Example,
//        Created_By, Created_By_Role, Created_Date,
//        Updated_By, Updated_By_Role, Updated_Date,
//        Is_Deleted
//    )
//    VALUES(
//        @Category_ID, @Title, @Description, @Video_URL, @Example,
//        @Created_By, @Created_By_Role, @Created_Date,
//        @Updated_By, @Updated_By_Role, @Updated_Date,
//        @Is_Deleted
//    )
//END
            // Truyền các tham số cho Stored Procedure
            command.Parameters.AddWithValue("@Category_ID", lesson.Category_ID);
            command.Parameters.AddWithValue("@Title", lesson.Title);
            command.Parameters.AddWithValue("@Description", lesson.Description);
            command.Parameters.AddWithValue("@Video_URL", lesson.Video_Url);
            command.Parameters.AddWithValue("@Example", lesson.Example);
            command.Parameters.AddWithValue("@Created_By", lesson.Created_By);
            command.Parameters.AddWithValue("@Created_By_Role", lesson.Create_By_Role);
            command.Parameters.AddWithValue("@Created_Date", lesson.Created_Date);
            command.Parameters.AddWithValue("@Updated_By", lesson.Update_By);
            command.Parameters.AddWithValue("@Updated_By_Role", lesson.Update_By_Role);
            command.Parameters.AddWithValue("@Updated_Date", lesson.Updated_Date);
            int rowsAffected = command.ExecuteNonQuery();
            sqlCon.Close();
            return "Add_Success";
        }
        public Lesson ShowDataLesson(string ID)
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlCommand command = new SqlCommand("proc_showlesson", sqlCon);
            command.CommandType = CommandType.StoredProcedure;
            //Procedure 
//            CREATE PROCEDURE proc_addlesson
//    @Category_ID INT,
//    @Title NVARCHAR(255),
//    @Description NVARCHAR(MAX) = NULL,
//    @Video_URL NVARCHAR(MAX) = NULL,
//    @Example NVARCHAR(MAX) = NULL,
//    @Created_By INT,
//    @Created_By_Role NVARCHAR(10) = NULL,
//    @Created_Date DATETIME,
//    @Updated_By INT = NULL,
//    @Updated_By_Role NVARCHAR(10) = NULL,
//    @Updated_Date DATETIME = NULL,
//    @Is_Deleted BIT
//AS
//BEGIN
//    INSERT INTO Lesson(
//        Category_ID, Title, Description, Video_URL, Example,
//        Created_By, Created_By_Role, Created_Date,
//        Updated_By, Updated_By_Role, Updated_Date,
//        Is_Deleted
//    )
//    VALUES(
//        @Category_ID, @Title, @Description, @Video_URL, @Example,
//        @Created_By, @Created_By_Role, @Created_Date,
//        @Updated_By, @Updated_By_Role, @Updated_Date,
//        @Is_Deleted
//    )
//END
            command.Parameters.AddWithValue("@Lesson_ID", ID);

            SqlDataReader reader = command.ExecuteReader();

            Lesson lesson = new Lesson();

            if (reader.Read())
            {
                lesson.Lesson_ID = Convert.ToString(reader["Lesson_ID"]);
                lesson.Category_ID = Convert.ToInt32(reader["Category_ID"]);
                lesson.Title = Convert.ToString(reader["Title"]);
                lesson.Description = Convert.ToString(reader["Description"]);
                lesson.Video_Url = Convert.ToString(reader["Video_Url"]);
                lesson.Example = Convert.ToString(reader["Example"]);
                lesson.Created_By = Convert.ToInt32(reader["Created_By"]);
                lesson.Create_By_Role = Convert.ToString(reader["Created_By_Role"]);
                lesson.Created_Date = Convert.ToDateTime(reader["Created_Date"]);
                lesson.Update_By = Convert.ToInt32(reader["Updated_By"]);
                lesson.Update_By_Role = Convert.ToString(reader["Updated_By_Role"]);
                lesson.Updated_Date = Convert.ToDateTime(reader["Updated_Date"]);
                return lesson;
            }

            return null;
            sqlCon.Close();
        }
        public string DeleteDataLesson(string ID)
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if(sqlCon.State == ConnectionState.Closed)
        {
                sqlCon.Open();
            }

            SqlCommand command = new SqlCommand("proc_deletelesson", sqlCon);
            command.CommandType = CommandType.StoredProcedure;
            //procedure
            //CREATE PROCEDURE proc_deletelesson
            //    @Lesson_ID
            //AS
            //BEGIN
            // Xóa trong các Table khác 
            //DELETE FROM Coure_Category WHERE Lesson_ID = @Lesson_ID;

            //...................
            //Xóa trong table Lesson 
            //DELETE FROM Lesson WHERE Lesson_Id = @Lesson_ID;
            //END
            // Thêm tham số đầu vào cho stored procedure
            command.Parameters.AddWithValue("@Lesson_ID", ID);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                return "Delete_sucess";
            }
            else
            {
                return "Null_Lesson";
            }
            sqlCon.Close();
        }
    }
}
