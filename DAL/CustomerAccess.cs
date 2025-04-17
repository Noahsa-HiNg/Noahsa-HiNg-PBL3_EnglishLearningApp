using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class CustomerAccess:DataBaseAccess
    {
        //public string DeleteCus(Customer customer)
        //{
        //    string result = DeleteDataCus(customer);
        //    return result;
        //}
        //public void LockCus(Customer customer)
        //{
        //    LockDataCus(customer);
        //}
        //// tuấn anh 
        public int Get_Quantily_Customer_DATA()
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
                customer.Account_ID = reader["Account_ID"].ToString();
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
    }
}
