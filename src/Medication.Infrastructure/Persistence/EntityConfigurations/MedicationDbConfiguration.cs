namespace Medication.Infrastructure.Persistence.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Medication.Domain;

    internal class MedicationDbConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.ToTable("Medications");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).ValueGeneratedNever();
            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.Quantity).IsRequired();
            builder.Property(m => m.CreatedAt).IsRequired();
        }
    }
}
