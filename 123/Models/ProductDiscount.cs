using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123.Models
{
    public class ProductDiscounts
    {
        [Key]  // ps_id là khóa chính
        [Column("pd_id")]
        public int PdId { get; set; }   // Khóa chính của bảng Product_Supplier

        [Column("product_id")]
        [Required(ErrorMessage = "Mã sản phẩm là bắt buộc.")]
        [MaxLength(50, ErrorMessage = "Mã sản phẩm không được vượt quá 50 kí tự.")]
        public string ProductId { get; set; }   // Mã sản phẩm

        [Column("discount_id")]
        [Required(ErrorMessage = "Mã khuyến mãi là bắt buộc.")]
        public int DiscountId { get; set; }   // Mã nhà cung cấp

        [Column("is_deleted")]
        [Required]
        public bool IsDeleted { get; set; } = false;  // Trạng thái xóa (mặc định là false)

        // Quan hệ với Product
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        // Quan hệ với Supplier
        [ForeignKey("SupplierId")]
        public virtual Discount Discount { get; set; }

        // Constructor mặc định
        public ProductDiscounts() { }

        // Constructor đầy đủ
        public ProductDiscounts(int pdId, string productId, int discountId, bool isDeleted)
        {
            PdId = pdId;
            ProductId = productId;
            DiscountId = discountId;
            IsDeleted = isDeleted;
        }

        public override string ToString()
        {
            return $"ProductDiscount: PdId = {PdId}, ProductId = {ProductId}, DiscountId = {DiscountId}, IsDeleted = {IsDeleted}";
        }
    }
}
