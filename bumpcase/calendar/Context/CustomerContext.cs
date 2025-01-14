using calendar.Entites;
using Microsoft.EntityFrameworkCore;

namespace calendar.Context
{
    public class CustomerContext : DbContext
    {
        virtual public DbSet<Customer> Customers => Set<Customer>();

        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }
        public CustomerContext()
        { }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: nameof(calendar));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable(nameof(Customer));

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Patients)
                .WithOne(e => e.Owner)
                .HasForeignKey(e => e.OwnerId)
                .IsRequired();



            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Meetings)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
