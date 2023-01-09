using Microsoft.EntityFrameworkCore;

namespace TodosMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {

        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }
    }
}
