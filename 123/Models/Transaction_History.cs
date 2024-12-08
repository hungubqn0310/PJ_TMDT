using System;

namespace _123.Models
{
    public class TransactionHistory
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }

        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }

        
    }
}
