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

        [NotMapped] // Đánh dấu trường này không được ánh xạ tới cơ sở dữ liệu
        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc.")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Column("phone_number")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 kí tự.")]
        public string PhoneNumber { get; set; }

        [MaxLength(255, ErrorMessage = "Tối đa 255 kí tự.")]
        public string Address { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
        //public bool TermsAccepted { get; set; }


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
        public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "Email là bắt buộc")]
    [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
    public string Email { get; set; }
}

public class ResetPasswordViewModel
{
    [Required(ErrorMessage = "Email là bắt buộc")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Mật khẩu mới là bắt buộc")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

    public string Token { get; set; }
}
    }
}