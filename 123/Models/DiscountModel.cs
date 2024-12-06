using System;

namespace _123.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        
        public string DiscountName { get; set; }
        
        public decimal DiscountPercent { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
