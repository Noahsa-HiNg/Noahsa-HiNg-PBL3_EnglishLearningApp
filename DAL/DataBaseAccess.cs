using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Http;
using DTO;
using System.Data;
using System.Reflection;
namespace DAL
{
    public class SqlconnectionData
    {
        public static SqlConnection connnect()
        {
            string strCon = @" Data Source = DESKTOP - 03GMFL8; Initial Catalog = PBL3_DB; Integrated Security = True;";
            SqlConnection sqlcon = null;
            if (sqlcon == null)
            {
                sqlcon = new SqlConnection(strCon);
            }
            return sqlcon;
        }
    }

    public class DataBaseAccess
    {
        public static string CheckLoginData(Account account)
        {
            string ID_user = null;
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CheckLoginData"; //proc_logic là tên procedure trong database
            command.Connection = sqlCon;
            command.Parameters.AddWithValue("@user", account.Username);
            command.Parameters.AddWithValue("@pass", account.Password);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ID_user = reader.GetString(0);
                }
                reader.Close();
                sqlCon.Close();
            }
            else
            {
                return "taikhoanmatkhaukhongchinhxac";
            }
            return ID_user;

        }
        // tuấn anh thêm
        public static bool CheckEmailData(string email)
        {

            string ID_user = null;
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CheckGmailExist"; // nhớ tạo procedure trong database
            command.Connection = sqlCon;
            command.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
                reader.Close();
                sqlCon.Close();
            }
            else
            {
                return false;
            }
        }
        public int Get_quantily_Account_DATA()
        {
            int sl = 0;
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Get_quantily_Account";
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
        public int Get_quantily_Customer_DATA()
        {
            int sl = 0;
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Get_quantily_Customer";
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
        // tuấn anh thêm
        public string ChangeDataPassword(Person person, string newpassword)
        {
            try
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
                command.Parameters.AddWithValue("@Id", person.Account_ID);
                command.Parameters.AddWithValue("@NewPassword", newpassword);
                command.ExecuteNonQuery();
                return "Password_updated_successfully.";
            }
            catch (SqlException ex)
            {
                return "Not_Found_Account";
            }
        }
        public string ChangeDataName(Person person, string newname)
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
            command.Parameters.AddWithValue("@Id", person.Account_ID);
            command.Parameters.AddWithValue("@NewName", newname);
            command.ExecuteNonQuery();
            return "Name_updated_successfully.";
        }
        public string ChangeDataPhone(Person person, string newphone)
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
            command.Parameters.AddWithValue("@Id", person.Account_ID);
            command.Parameters.AddWithValue("@NewPhone", newphone);
            command.ExecuteNonQuery();
            return "Phone_updated_successfully.";
        }
        public string DeleteDataCus(Customer customer)
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "proc_delete_customer";
            //CREATE PROCEDURE proc_delete_customer
            //    @AccountID INT
            //AS
            //BEGIN
            // Xóa trong các Table khác 
            //DELETE FROM Payment WHERE CustomerID = @AccountID;
            //DELETE FROM NotificationRecipient WHERE CustomerID = @AccountID;
            //...................
            //Xóa trong table Customer 
            //DELETE FROM Customers WHERE Account_ID = @AccountID;
            //END
            command.Connection = sqlCon;
            command.Parameters.AddWithValue("@AccountID", customer.Account_ID);
            command.ExecuteNonQuery();
            return "DeleteCus_successfully";
        }
        public void LockDataCus(Customer customer)
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
        }
        public (Customer, Account) ShowDataInforCus(string customerId)
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlCommand command = new SqlCommand();
            command.Connection = sqlCon;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "proc_show_customer";
            //            CREATE PROCEDURE proc_show_customer
            //                @Customer_ID INT
            //            AS
            //BEGIN
            //    SELECT
            //        c.Customer_ID,
            //        c.Account_ID,
            //        c.Name,
            //        c.Phone,
            //        c.Email,
            //        c.Notification_Enabled,
            //        c.Created_date,
            //        c.Updated_date,
            //        a.username,
            //        a.password
            //    FROM
            //        Customer c
            //    INNER JOIN
            //        Account a ON c.Account_ID = a.Account_ID
            //    WHERE
            //        c.Customer_ID = @Customer_ID
            //END
            command.Parameters.AddWithValue("@Customer_ID", customerId);

            SqlDataReader reader = command.ExecuteReader();

            Customer customer = null;
            Account account = null;

            if (reader.Read())
            {
                customer = new Customer();
                account = new Account();

                // Gán dữ liệu cho Customer
                customer.ID = reader["Customer_ID"].ToString();
                customer.Account_ID =reader["Account_ID"].ToString();
                customer.Name = reader["Name"].ToString();
                customer.Phone = reader["Phone"].ToString();
                customer.Email = reader["Gmail"].ToString();
                customer.Notification_Enable = Convert.ToBoolean(reader["Notification_enabled"]);
                customer.Created_Date = Convert.ToDateTime(reader["Created_date"]);
                customer.Updated_Date = Convert.ToDateTime(reader["Updated_date"]);

                // Gán dữ liệu cho Account
                account.ID = reader["Account_ID"].ToString();
                account.Username = reader["username"].ToString();
                account.Password = reader["password"].ToString();
            }

            reader.Close();
            sqlCon.Close();

            return (customer, account);
        }
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
    }
}
