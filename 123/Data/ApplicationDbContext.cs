using Microsoft.EntityFrameworkCore;

namespace _123.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Định nghĩa DbSet cho các bảng trong cơ sở dữ liệu
        // Ví dụ:
        // public DbSet<Users> Users { get; set; }
    }
}
