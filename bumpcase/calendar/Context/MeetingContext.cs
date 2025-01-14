using calendar.Entites;
using Microsoft.EntityFrameworkCore;

namespace calendar.Context
{
    public class MeetingContext : DbContext
    {
        virtual public DbSet<Meeting> Meetings => Set<Meeting>();

        public MeetingContext(DbContextOptions<MeetingContext> options) : base(options)
        {
        }
        public MeetingContext()
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: nameof(calendar));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>().ToTable(nameof(Meeting));
            modelBuilder.Entity<Meeting>().HasIndex(e => new { e.CustomerId, e.VeterinarianId, e.PatientId, e.Date }).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
