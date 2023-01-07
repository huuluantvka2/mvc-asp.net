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
    }
}
