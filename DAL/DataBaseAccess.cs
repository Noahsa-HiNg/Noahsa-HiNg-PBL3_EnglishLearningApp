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
            string strCon = null;//Nhap sever data
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
            command.CommandText = "proc_logic"; //proc_logic là tên procedure trong database
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
        public static bool CheckGmailData(string Gmail)
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
            command.Parameters.AddWithValue("@gmail", Gmail);
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
                return true;
            }
            return false;

        }

    }
}