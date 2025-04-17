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
            //CREATE PROCEDURE proc_delete_editor 
            //    @EditorID INT
            //AS
            //BEGIN
            //DELETE FROM Editor WHERE Editor_ID = @EditorID;
            //END
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
        public int Get_Quantily_Editor_DATA()
        {
            int sl = 0;
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Get_quantily_Editor";
            command.Connection = sqlCon;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    sl = reader.GetInt32(0);
                }
                reader.Close();
                sqlCon.Close();
            }
            return sl;
        }
    }
}
