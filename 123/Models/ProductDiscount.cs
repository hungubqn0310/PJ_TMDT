using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123.Models
{
    public class ProductDiscount
    {
        [Key, Column(Order = 0)]  // Chỉ định khóa chính cho cột 'product_id'
        [Required]
        public string product_id { get; set; }  // product_id (string)

        [Key, Column(Order = 1)]  // Chỉ định khóa chính cho cột 'discount_id'
        [Required]
        public int discount_id { get; set; }    // discount_id (int)

        [Column("is_deleted")]
        public bool is_deleted { get; set; }    // is_deleted (bool)

        // Constructor mặc định
        public ProductDiscount() { }

        // Constructor đầy đủ
        public ProductDiscount(string productId, int discountId, bool isDeleted)
        {
            product_id = productId;
            discount_id = discountId;
            is_deleted = isDeleted;
        }
    }
}
