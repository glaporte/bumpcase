using calendar.Entites;
using Microsoft.EntityFrameworkCore;

namespace calendar.Context
{
    public class PatientContext : DbContext
    {
        virtual public DbSet<Patient> Patients => Set<Patient>();

        public PatientContext(DbContextOptions<PatientContext> options) : base(options)
        {
        }
        public PatientContext()
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: nameof(calendar));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable(nameof(Patient));

            modelBuilder.Entity<Patient>()
            .HasMany(e => e.Meetings)
            .WithOne(e => e.Patient)
            .HasForeignKey(e => e.PatientId)
            .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
