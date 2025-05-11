using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace DAL
{
    public class NotificationAccess:DataBaseAccess
    {
        public List<Notification> ShowAllNotificationData()
        {
            List<Notification> notifications = new List<Notification>();

            // Kết nối cơ sở dữ liệu
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            // Thực thi Stored Procedure
            SqlCommand cmd = new SqlCommand("ShowAllNotification", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            //            CREATE PROCEDURE ShowAllNotification
            //AS
            //BEGIN
            //    SELECT
            //        Notification_ID, 
            //        Created_By, 
            //        Created_By_Role, 
            //        Title, 
            //        Content, 
            //        Created_Date
            //    FROM
            //        Notifications
            //END

            // Đọc dữ liệu từ SqlDataReader
            SqlDataReader reader = cmd.ExecuteReader();

            // Lặp qua các bản ghi và tạo đối tượng Notification
            while (reader.Read())
            {
                Notification notification = new Notification
                {
                    Notification_ID = reader.GetInt32(reader.GetOrdinal("Notification_ID")),
                    Created_By = reader.GetInt32(reader.GetOrdinal("Created_By")),
                    Create_By_Role = reader.GetString(reader.GetOrdinal("Created_By_Role")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Content = reader.IsDBNull(reader.GetOrdinal("Content")) ? null : reader.GetString(reader.GetOrdinal("Content")),
                    Created_Date = reader.GetDateTime(reader.GetOrdinal("Created_Date"))
                };

                // Thêm thông báo vào danh sách
                notifications.Add(notification);
            }

            // Đóng reader và kết nối
            reader.Close();
            sqlCon.Close();
            return notifications;
        }
        public List<Notification> ShowMyNotificationData(Customer customer)
        {
            List<Notification> notifications = new List<Notification>();

            // Kết nối cơ sở dữ liệu
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            // Thực thi Stored Procedure
            SqlCommand cmd = new SqlCommand("ShowMyNotification", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            //            CREATE PROCEDURE ShowMyNotification
            //    @Customer_ID INT
            //AS
            //BEGIN
            //    SELECT
            //        n.Notification_ID, 
            //        n.Title, 
            //        n.Content, 
            //        n.Created_By, 
            //        n.Created_By_Role, 
            //        n.Created_Date
            //    FROM
            //        Notifications n
            //    INNER JOIN
            //        Notification_Recipients nr ON n.Notification_ID = nr.Notification_ID
            //    WHERE
            //        nr.Customer_ID = @Customer_ID
            //END
            // Thêm tham số @Customer_ID vào stored procedure
            cmd.Parameters.AddWithValue("@Customer_ID", customer.ID);

            // Đọc dữ liệu từ SqlDataReader
            SqlDataReader reader = cmd.ExecuteReader();

            // Lặp qua các bản ghi và tạo đối tượng Notification
            while (reader.Read())
            {
                Notification notification = new Notification
                {
                    Notification_ID = reader.GetInt32(reader.GetOrdinal("Notification_ID")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Content = reader.IsDBNull(reader.GetOrdinal("Content")) ? null : reader.GetString(reader.GetOrdinal("Content")),
                    Created_By = reader.GetInt32(reader.GetOrdinal("Created_By")),
                    Create_By_Role = reader.GetString(reader.GetOrdinal("Created_By_Role")),
                    Created_Date = reader.GetDateTime(reader.GetOrdinal("Created_Date"))
                };

                // Thêm Notification vào danh sách
                notifications.Add(notification);
            }

            // Đóng reader và kết nối
            reader.Close();
            sqlCon.Close();

            return notifications;
        }


        public void SendNotificationData(Customer customer, Notification notification)
        {
            // Kết nối cơ sở dữ liệu
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            // Thực thi Stored Procedure
            SqlCommand cmd = new SqlCommand("SentNotification", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            //            CREATE PROCEDURE SentNotification
            //    @Notification_ID INT,
            //    @Customer_ID INT,
            //    @Received_At DATETIME
            //AS
            //BEGIN
            //    INSERT INTO Notification_Recipients(Notification_ID, Customer_ID, Received_At)
            //    VALUES(@Notification_ID, @Customer_ID, @Received_At)
            //END

            // Thêm các tham số cần thiết vào Stored Procedure
            cmd.Parameters.AddWithValue("@Notification_ID", notification.Notification_ID);
            cmd.Parameters.AddWithValue("@Customer_ID", customer.ID);
            cmd.Parameters.AddWithValue("@Received_At", DateTime.Now); // Gửi thời gian hiện tại khi thông báo được nhận

            // Thực thi lệnh
            cmd.ExecuteNonQuery();

            // Đóng kết nối
            sqlCon.Close();
        }
        public Notification FindNotificationData(string NotificationID)
        {
            Notification notification = null;

            // Kết nối cơ sở dữ liệu
            SqlConnection sqlCon = SqlconnectionData.connnect();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            // Thực thi Stored Procedure
            SqlCommand cmd = new SqlCommand("FindNotification", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;

            // Thêm tham số vào Stored Procedure
            cmd.Parameters.AddWithValue("@NotificationID", NotificationID);
            //            CREATE PROCEDURE FindNotification
            //    @NotificationID INT
            //AS
            //BEGIN
            //    SELECT
            //        Notification_ID, 
            //        Created_By, 
            //        Created_By_Role, 
            //        Title, 
            //        Content, 
            //        Created_Date
            //    FROM
            //        Notifications
            //    WHERE
            //        Notification_ID = @NotificationID
            //END

            // Đọc dữ liệu từ SqlDataReader
            SqlDataReader reader = cmd.ExecuteReader();

            // Kiểm tra và đọc dữ liệu từ cơ sở dữ liệu
            if (reader.Read())
            {
                notification = new Notification
                {
                    Notification_ID = reader.GetInt32(reader.GetOrdinal("Notification_ID")),
                    Created_By = reader.GetInt32(reader.GetOrdinal("Created_By")),
                    Create_By_Role = reader.GetString(reader.GetOrdinal("Created_By_Role")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Content = reader.IsDBNull(reader.GetOrdinal("Content")) ? null : reader.GetString(reader.GetOrdinal("Content")),
                    Created_Date = reader.GetDateTime(reader.GetOrdinal("Created_Date"))
                };
            }

            // Đóng reader và kết nối
            reader.Close();
            sqlCon.Close();

            return notification;
        }

    }
}
