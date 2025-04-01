using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Payment
    {
        public int Payment_ID { get; set; }
        public int Category_ID { get; set; }
        public int Customer_ID { get; set; }
        public decimal Amount { get; set; }
        public string Payment_Method { get; set; }
        public string Status { get; set; }
        public DateTime Processing_Date { get; set; }

        public Payment() { }

        public Payment(int paymentId, int categoryId, int customerId, decimal amount, string paymentMethod, string status, DateTime processing_date)
        {
            Payment_ID = paymentId;
            Category_ID = categoryId;
            Customer_ID = customerId;
            Amount = amount;
            Payment_Method = paymentMethod;
            Status = status;
            Processing_Date = processing_date;
        }
    }
}