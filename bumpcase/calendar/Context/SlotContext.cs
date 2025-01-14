using calendar.Entites;
using Microsoft.EntityFrameworkCore;

namespace calendar.Context
{
    public class SlotContext : DbContext
    {
        virtual public DbSet<Slot> Slots => Set<Slot>();

        public SlotContext(DbContextOptions<PatientContext> options) : base(options)
        {
        }
        public SlotContext()
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: nameof(calendar));
        }
    }
}
