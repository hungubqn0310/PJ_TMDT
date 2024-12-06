using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123.Models
{
    public class ProductSuppliers
    {
        [Key]
        [Column("product_id")]
        [Required(ErrorMessage = "Mã sản phẩm là bắt buộc.")]
        [MaxLength(50, ErrorMessage = "Mã sản phẩm không được vượt quá 50 kí tự.")]
        public string ProductId { get; set; }   // Mã sản phẩm

        [Key]
        [Column("supplier_id")]
        [Required(ErrorMessage = "Mã nhà cung cấp là bắt buộc.")]
        public int SupplierId { get; set; }   // Mã nhà cung cấp

        [Column("is_deleted")]
        [Required]
        public bool IsDeleted { get; set; } = false;  // Trạng thái xóa (mặc định là false)

        // Quan hệ với Product
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        // Quan hệ với Supplier
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }
        
        // Constructor mặc định
        public ProductSuppliers() { }

        // Constructor đầy đủ
        public ProductSuppliers(string productId, int supplierId, bool isDeleted)
        {
            ProductId = productId;
            SupplierId = supplierId;
            IsDeleted = isDeleted;
        }

        public override string ToString()
        {
            return $"ProductSupplier: ProductId = {ProductId}, SupplierId = {SupplierId}, IsDeleted = {IsDeleted}";
        }
    }
}
