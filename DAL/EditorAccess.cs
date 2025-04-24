using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class EditorAccess : DataBaseAccess
    {
        public string DeleteDataEditor(Editor editor)
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "proc_delete_editor";
            //            CREATE PROCEDURE proc_delete_editor 
            //    @EditorID INT
            //AS
            //BEGIN
            //    SET NOCOUNT ON;

            //            UPDATE Editor
            //    SET
            //        Is_Deleted = 1,
            //        Deleted_At = GETDATE()
            //    WHERE Editor_ID = @EditorID;
            //            END;
            command.Connection = sqlCon;
            command.Parameters.AddWithValue("@EditorID", editor.Account_ID);
            command.ExecuteNonQuery();
            return "DeleteEditor_successfully";
        }
        public (Editor, Account) ShowDataInforEditor(string editorID)
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlCommand command = new SqlCommand();
            command.Connection = sqlCon;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "proc_show_editor";
            //            CREATE PROCEDURE proc_show_editor
            //                @Editor_ID INT
            //            AS
            //BEGIN
            //    SELECT
            //        e.Editor_ID,
            //        e.Account_ID,
            //        e.Name,
            //        e.Phone,
            //        e.Email,
            //        e.Permission 
            //        e.Status 
            //        e.Created_date,
            //        e.Updated_date,
            //        a.username,
            //        a.password
            //    FROM
            //        Editor e
            //    INNER JOIN
            //        Account a ON e.Account_ID = a.Account_ID
            //    WHERE
            //        e.Editor_ID = @Editor_ID
            //END
            command.Parameters.AddWithValue("@Customer_ID", editorID);

            SqlDataReader reader = command.ExecuteReader();

            Editor editor = null;
            Account account = null;

            if (reader.Read())
            {
                editor = new Editor();
                account = new Account();

                // Gán dữ liệu cho Editor
                editor.ID = reader["Editor_ID"].ToString();
                editor.Account_ID = reader["Account_ID"].ToString();
                editor.Name = reader["Name"].ToString();
                editor.Phone = reader["Editor_ID"].ToString();
                editor.Email = reader["gmail"].ToString();
                editor.Permissions = reader["Permission"].ToString();
                editor.Status = reader["Status"].ToString();
                editor.Created_Date = Convert.ToDateTime(reader["created_date"]);
                editor.Updated_Date = Convert.ToDateTime(reader["updated_date"]);

                // Gán dữ liệu cho Account
                account.ID = reader["Account_ID"].ToString();
                account.Username = reader["username"].ToString();
                account.Password = reader["password"].ToString();
            }

            reader.Close();
            sqlCon.Close();

            return (editor, account);
        }
        public string ChangeDataPermiss(Editor editor)
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "proc_logic";
            command.Connection = sqlCon;
            return "Change_Permiss_Success";
        }
        public int CreateEditorID()
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CreateEditorID";
            //            CREATE PROCEDURE CreateEditorID
            //AS
            //BEGIN
            //    SELECT TOP 1 Editor_ID
            //    FROM Editor 
            //    ORDER BY Editor_ID DESC;
            //            END;
            object result = command.ExecuteScalar();
            int ID = Convert.ToInt32(result);
            return ID;
        }
        public int CreateAccountID()
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CreateAccountID";
            //            CREATE PROCEDURE CreateCusID
            //AS
            //BEGIN
            //    SELECT TOP 1 Account_ID
            //    FROM Account
            //    ORDER BY Account_ID DESC;
            //            END;
            object result = command.ExecuteScalar();
            int ID = Convert.ToInt32(result);
            return ID;
        }
        public string AddDataEditor(Editor editor, Account account)
        {
            int EditID = CreateEditorID();
            int AccID = CreateAccountID();
            DateTime Create_Date = DateTime.Now;
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlTransaction transaction = sqlCon.BeginTransaction(); // Bắt đầu giao dịch

            // 1. Thêm Account
            SqlCommand accCmd = new SqlCommand("InsertAccount", sqlCon, transaction);
            accCmd.CommandType = CommandType.StoredProcedure;
            accCmd.Parameters.AddWithValue("@Account_ID", AccID);
            //CREATE PROCEDURE InsertAccount
            //    @Account_ID INT,
            //    @username NVARCHAR(50),
            //    @password NVARCHAR(255),
            //    @avatar NVARCHAR(MAX),
            //    @role NVARCHAR(20)
            //AS
            //BEGIN
            //    INSERT INTO Account(
            //        Account_ID,
            //        username,
            //        password,
            //        avatar,
            //        role
            //    )
            //    VALUES(
            //        @Account_ID,
            //        @username,
            //        @password,
            //        @avatar,
            //        @role
            //    );
            //            END;
            accCmd.Parameters.AddWithValue("@username", account.Username);
            accCmd.Parameters.AddWithValue("@password", account.Password);
            accCmd.Parameters.AddWithValue("@avatar", DBNull.Value); // avatar có thể null
            accCmd.Parameters.AddWithValue("@role", account.Role);
            accCmd.ExecuteNonQuery();

            // 2. Thêm Customer
            SqlCommand cusCmd = new SqlCommand("InsertEditor", sqlCon, transaction);

            //            CREATE PROCEDURE InsertEditor 
            //    @Account_ID INT,
            //    @Name NVARCHAR(100),
            //    @Phone VARCHAR(20),
            //    @Email NVARCHAR(100),
            //    @Permission NVARCHAR(100),
            //    @Status NVARCHAR(10)
            //    @Created_Date DATETIME,
            //AS
            //BEGIN
            //    INSERT INTO Editor(
            //        Account_ID,
            //        Name,
            //        Phone,
            //        Email,
            //        Permission,
            //        Status,
            //        Created_Date,
            //        Is_Deleted,
            //        Updated_Date,
            //        Deleted_At
            //    )
            //    VALUES(
            //        @Account_ID,
            //        @Name,
            //        @Phone,
            //        @Email,
            //        @Permission
            //        @Status
            //        @Created_Date,
            //        0,
            //        NULL,
            //        NULL
            //    );
            //            END;
            cusCmd.CommandType = CommandType.StoredProcedure;
            cusCmd.Parameters.AddWithValue("@Account_ID", AccID);
            cusCmd.Parameters.AddWithValue("@Name", editor.Name);
            cusCmd.Parameters.AddWithValue("@Phone", editor.Phone);
            cusCmd.Parameters.AddWithValue("@Email", editor.Email);
            cusCmd.Parameters.AddWithValue("@@Permission", editor.Permissions);
            cusCmd.Parameters.AddWithValue("@Status", editor.Status);
            cusCmd.Parameters.AddWithValue("@Created_Date", Create_Date);
            cusCmd.ExecuteNonQuery();
            transaction.Commit();
            return "Add_Success";
        }
        public List<Editor> ShowAllDataEditor()
        {
            List<Editor> editors = new List<Editor>();
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.Connection = sqlCon;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowAllEditor";
            //            CREATE PROCEDURE ShowAllEditor
            //AS
            //BEGIN
            //    SELECT
            //        Editor_ID, 
            //        Name
            //    FROM
            //        Editor
            //    WHERE
            //        Is_Deleted = 0
            //END
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Editor editor = new Editor();
                editor.ID = reader["Editor_ID"].ToString();
                editor.Name = reader["Name"].ToString();
                editors.Add(editor);
            }
            reader.Close();
            sqlCon.Close();
            return editors;
        }
        public void RestoreDataEditor(Editor editor)
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "proc_restoreditor";
            //            CREATE PROCEDURE proc_restoreditor
            //    @EditorID INT
            //AS
            //BEGIN
            //    SET NOCOUNT ON;

            //            UPDATE Editor
            //    SET
            //        Is_Deleted = 0,
            //        Deleted_At = NULL
            //    WHERE Editor_ID = @EditorID AND Is_Deleted = 1;
            //            END;
            command.Connection = sqlCon;
            command.Parameters.AddWithValue("@EditorID", editor.ID);
            command.ExecuteNonQuery();
            sqlCon.Close();
        }
        public List<Editor> ShowDataDeleteEditor()
        {
            List<Editor> editors = new List<Editor>();
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.Connection = sqlCon;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ShowDeleteEditor";
            //            CREATE PROCEDURE ShowDeleteEditor
            //AS
            //BEGIN
            //    SELECT
            //        Editor_ID, 
            //        Name,
            //        Deleted_At
            //    FROM
            //        Editor
            //    WHERE
            //        Is_Deleted = 1
            //END
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Editor editor = new Editor();
                editor.ID = reader["Editor_ID"].ToString();
                editor.Name = reader["Name"].ToString();
                editor.Delete_At = (DateTime)reader["Delete_At"];
                editors.Add(editor);
            }
            reader.Close();
            sqlCon.Close();
            return editors;
        }
    }
}
