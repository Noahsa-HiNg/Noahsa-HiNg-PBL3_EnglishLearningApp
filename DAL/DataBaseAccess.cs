﻿using System;
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
            string strCon = @" Data Source = DESKTOP-3LCRB7N\SQLEXPRESS01; Initial Catalog = PBL3; Integrated Security = True;";
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
        
    }
}
