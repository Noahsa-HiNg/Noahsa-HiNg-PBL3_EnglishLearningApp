using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Security.Cryptography;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Xml.Linq;
using System.Security.Principal;
namespace DAL
{
    public class AccountAccess : DataBaseAccess
    {
        public string CheckLoginData(string username, string password)
        {
            string ID_user = null;
            bool Lock;
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CheckLoginData"; //proc_logic là tên procedure trong database
            command.Connection = sqlCon;
            command.Parameters.AddWithValue("@user", username);
            command.Parameters.AddWithValue("@pass", password);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ID_user =reader.GetInt32(0).ToString();
                    //Lock = reader.GetBoolean(5);
                    //if (Lock) return "LockedAccount";
                }
                reader.Close();
                sqlCon.Close();
            }
            else
            {
                return "UsenamePassworkFalt";
            }
            return ID_user;

        }
        // tuấn anh thêm
        public bool CheckEmailData(string email)
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
        public int CreateCusID()
        {
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CreateCusID";
            //            CREATE PROCEDURE CreateCusID
            //AS
            //BEGIN
            //    SELECT TOP 1 Customer_ID
            //    FROM Customers
            //    ORDER BY Customer_ID DESC;
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
        public string AddDataCustomer(Customer customer, Account account)
        {
            int CusID = CreateCusID();
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
            SqlCommand cusCmd = new SqlCommand("InsertCustomer", sqlCon, transaction);
            
            //            CREATE PROCEDURE InsertCustomer 
            //    @Account_ID INT,
            //    @Name NVARCHAR(100),
            //    @Phone VARCHAR(20),
            //    @Email NVARCHAR(100),
            //    @Notification BIT,
            //    @Created_Date DATETIME,
            //AS
            //BEGIN
            //    INSERT INTO Customers(
            //        Account_ID,
            //        Name,
            //        Phone,
            //        Email,
            //        Notification,
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
            //        @Notification,
            //        @Created_Date,
            //        0,
            //        NULL,
            //        NULL
            //    );
            //            END;
            cusCmd.CommandType = CommandType.StoredProcedure;
            cusCmd.Parameters.AddWithValue("@Account_ID", AccID);
            cusCmd.Parameters.AddWithValue("@Name", customer.Name);
            cusCmd.Parameters.AddWithValue("@Phone", customer.Phone);
            cusCmd.Parameters.AddWithValue("@Email", customer.Email);
            cusCmd.Parameters.AddWithValue("@Notification",1);
            cusCmd.Parameters.AddWithValue("@Created_Date", Create_Date);
            cusCmd.ExecuteNonQuery();
            transaction.Commit();
            return "Add_Success";

        }
        public Account FindAccountData(string accountID)
        {
            Account account = null;
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "FindAccount";
            command.Connection = sqlCon;
            command.Parameters.AddWithValue("@AccountID", accountID); // Giả sử tham số truyền vào là @AccountID

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                account = new Account();
                account.ID = reader["Account_ID"].ToString();
                account.Username = reader["Username"].ToString();
                account.Password = reader["Password"].ToString();
                account.Role = reader["Role"].ToString();
                // Thêm các thuộc tính khác nếu có
            }
            reader.Close();
            sqlCon.Close();
            return account;
        }
        public DTO.Account ShowDataInforAccountByUsername(string username)
        {
            // ... logic của bạn để lấy thông tin tài khoản ...
            // Ví dụ:
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlCommand command = new SqlCommand("proc_show_account_by_username", sqlCon); // Kiểm tra tên SP của bạn
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Username", username);

            SqlDataReader reader = command.ExecuteReader();
            DTO.Account account = null;
            if (reader.Read())
            {
                account = new DTO.Account
                {
                    ID = reader["Account_ID"].ToString(),
                    Username = reader["Username"].ToString(),
                    Password = reader["Password"].ToString()
                    // Ánh xạ các thuộc tính khác
                };
            }
            reader.Close();
            sqlCon.Close();
            return account;
        }

    }
}
