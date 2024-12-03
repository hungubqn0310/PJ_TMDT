using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123.Models
{
    public class Users
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Tên người dùng là bắt buộc.")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Column("phone_number")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 kí tự.")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        // Constructor mặc định
        public Users() { }

        // Constructor đầy đủ
        public Users(int userId, string username, string password, string email, string phoneNumber, string address, bool isDeleted)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            IsDeleted = isDeleted;
        }

        public override string ToString()
        {
            return $"User  ID: {UserId}, Username: {Username}, Email: {Email}, Phone: {PhoneNumber}, Address: {Address}, IsDeleted: {IsDeleted}";
        }
    }
}