using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123.Models
{
    public class Categories
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên danh mục là bắt buộc.")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự.")]
        public string CategoryName { get; set; }

        [MaxLength(500, ErrorMessage = "Tối đa 500 kí tự.")]
        public string Description { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        // Constructor mặc định
        public Categories() { }

        // Constructor đầy đủ
        public Categories(int categoryId, string categoryName, string description, bool isDeleted)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            Description = description;
            IsDeleted = isDeleted;
        }

        public override string ToString()
        {
            return $"Category ID: {CategoryId}, CategoryName: {CategoryName}, Description: {Description}, IsDeleted: {IsDeleted}";
        }
    }
}
