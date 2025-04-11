using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Http;
using DTO;
using System.Data;
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
            command.CommandText = "proc_logic"; //proc_logic là tên procedure trong database
            command.Connection = sqlCon;
            command.Parameters.AddWithValue("@email", email);
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
                return false;
            }
            return true;
        }
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
    }
}