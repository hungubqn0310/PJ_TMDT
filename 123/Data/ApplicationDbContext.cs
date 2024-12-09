using Microsoft.EntityFrameworkCore;

namespace _123.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } // Định nghĩa bảng sản phẩm
        // Thêm các DbSet khác cho các bảng khác của bạn
    }
}
