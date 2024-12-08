using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123.Models
{
    public class Products
    {
        [Key]
        [Column("product_id")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự.")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự.")]
        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc.")]
        [Column("price")]
        [Range(0.01, 9999999.99, ErrorMessage = "Giá phải lớn hơn 0.")]
        public decimal Price { get; set; }

        [Column("material_id")]
        public int? MaterialId { get; set; }

        [Column("category_id")]
        public int? CategoryId { get; set; }

        [MaxLength(255, ErrorMessage = "Tối đa 255 kí tự.")]
        [Column("image_url")]
        public string ImageUrl { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
        public virtual Category Category { get; set; }
        public virtual Material Material { get; set; }
        // Constructor mặc định
        public Products() { }

        // Constructor đầy đủ
        public Products(string productId, string productName, string description, decimal price, int? materialId, int? categoryId, string imageUrl, bool isDeleted)
        {
            ProductId = productId;
            ProductName = productName;
            Description = description;
            Price = price;
            MaterialId = materialId;
            CategoryId = categoryId;
            ImageUrl = imageUrl;
            IsDeleted = isDeleted;
        }

        public override string ToString()
        {
            return $"Product ID: {ProductId}, Name: {ProductName}, Price: {Price}, Material ID: {MaterialId}, Category ID: {CategoryId}, IsDeleted: {IsDeleted}";
        }
    }
}
