using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123.Models
{
    public class Suppliers
    {
        [Key]
        [Column("supplier_id")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Tên nhà cung cấp là bắt buộc.")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự.")]
        public string SupplierName { get; set; }

        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự.")]
        public string ContactName { get; set; }

        [Column("phone_number")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 kí tự.")]
        public string PhoneNumber { get; set; }

        [MaxLength(255, ErrorMessage = "Tối đa 255 kí tự.")]
        public string Address { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự.")]
        public string Email { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        // Constructor mặc định
        public Suppliers() { }

        // Constructor đầy đủ
        public Suppliers(int supplierId, string supplierName, string contactName, string phoneNumber, string address, string email, bool isDeleted)
        {
            SupplierId = supplierId;
            SupplierName = supplierName;
            ContactName = contactName;
            PhoneNumber = phoneNumber;
            Address = address;
            Email = email;
            IsDeleted = isDeleted;
        }

        public override string ToString()
        {
            return $"Supplier ID: {SupplierId}, SupplierName: {SupplierName}, ContactName: {ContactName}, PhoneNumber: {PhoneNumber}, Address: {Address}, Email: {Email}, IsDeleted: {IsDeleted}";
        }
    }
}
