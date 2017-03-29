using Microsoft.EntityFrameworkCore;

namespace DojoActivity.Models
{
    public class DojoActivityContext : DbContext
    {
        public DojoActivityContext(DbContextOptions<DojoActivityContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activitys { get; set; }
        public DbSet<Participant> Participants { get; set; }
        
    }
}