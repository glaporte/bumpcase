using calendar.Entites;
using Microsoft.EntityFrameworkCore;

namespace calendar.Context
{
    public class VeterinarianContext : DbContext
    {
        virtual public DbSet<Veterinarian> Veterinarians => Set<Veterinarian>();

        public VeterinarianContext(DbContextOptions<VeterinarianContext> options) : base(options)
        {
        }
        public VeterinarianContext()
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: nameof(calendar));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Veterinarian>().ToTable(nameof(Veterinarian));
            modelBuilder.Entity<Veterinarian>()
                .HasMany(e => e.Customers)
                .WithOne(e => e.FavoriteVeterinarian)
                .HasForeignKey(e => e.FavoriteVeterinarianId)
                .IsRequired(false);

            modelBuilder.Entity<Veterinarian>()
            .HasMany(e => e.Meetings)
            .WithOne(e => e.Veterinarian)
            .HasForeignKey(e => e.VeterinarianId)
            .IsRequired();

            modelBuilder.Entity<Veterinarian>()
            .HasMany(e => e.Slots)
            .WithOne(e => e.Veterinarian)
            .HasForeignKey(e => e.VeterinarianId)
            .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

    }
}
