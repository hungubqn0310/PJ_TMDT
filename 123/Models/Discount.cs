using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123.Models
{
    public class Discounts
    {
        [Key]
        [Column("discount_id")]
        public int DiscountId { get; set; }

        [Required(ErrorMessage = "Tên giảm giá là bắt buộc.")]
        [MaxLength(255, ErrorMessage = "Tối đa 255 kí tự.")]
        [Column("discount_name")]
        public string DiscountName { get; set; }

        [Required(ErrorMessage = "Phần trăm giảm giá là bắt buộc.")]
        [Range(0, 100, ErrorMessage = "Phần trăm giảm giá phải nằm trong khoảng từ 0 đến 100.")]
        [Column("discount_percent")]
        public decimal DiscountPercent { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc.")]
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc.")]
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        // Constructor mặc định
        public Discounts() { }

        // Constructor đầy đủ
        public Discounts(int discountId, string discountName, decimal discountPercent, DateTime startDate, DateTime endDate, bool isDeleted)
        {
            DiscountId = discountId;
            DiscountName = discountName;
            DiscountPercent = discountPercent;
            StartDate = startDate;
            EndDate = endDate;
            IsDeleted = isDeleted;
        }

        public override string ToString()
        {
            return $"Discount ID: {DiscountId}, Name: {DiscountName}, Percent: {DiscountPercent}, Start: {StartDate}, End: {EndDate}, IsDeleted: {IsDeleted}";
        }
    }
}
