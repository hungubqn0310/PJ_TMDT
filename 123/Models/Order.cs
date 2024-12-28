using System;

namespace _123.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsDeleted { get; set; }

        public virtual User User { get; set; }

    }
}
