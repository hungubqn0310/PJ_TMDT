using System;

namespace _123.Models
{
    public class ShoppingCart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
