using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123.Models
{
    public class Materials
    {
        [Key]
        [Column("material_id")]
        public int MaterialId { get; set; }

        [Required(ErrorMessage = "Tên nguyên liệu là bắt buộc.")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự.")]
        [Column("material_name")]
        public string MaterialName { get; set; }

        [MaxLength(500, ErrorMessage = "Tối đa 500 kí tự.")]
        public string Description { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        // Constructor mặc định
        public Materials() { }

        // Constructor đầy đủ
        public Materials(int materialId, string materialName, string description, bool isDeleted)
        {
            MaterialId = materialId;
            MaterialName = materialName;
            Description = description;
            IsDeleted = isDeleted;
        }

        public override string ToString()
        {
            return $"Material ID: {MaterialId}, Name: {MaterialName}, Description: {Description}, IsDeleted: {IsDeleted}";
        }
    }
}
