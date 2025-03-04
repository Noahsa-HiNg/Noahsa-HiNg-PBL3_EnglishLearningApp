using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Http;
namespace DAL
{
    public class SqlConnectionData
    {
        static string strCon = " ";//nhap sever SQL vao
        static SqlConnection sqlCon = null;
        public static void Connect()
        {
            if(sqlCon == null)
            {
                SqlConnection sqlCon = new SqlConnection(strCon);
                //concac
            }
        }
    }
}
