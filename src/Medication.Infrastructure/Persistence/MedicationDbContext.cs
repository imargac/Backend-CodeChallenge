namespace Medication.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Medication.Domain;

    public sealed class MedicationDbContext : DbContext
    {
        public DbSet<Medication> Medications { get; set; }

        public MedicationDbContext(DbContextOptions<MedicationDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(MedicationDbContext).Assembly);
        }
    }
}
