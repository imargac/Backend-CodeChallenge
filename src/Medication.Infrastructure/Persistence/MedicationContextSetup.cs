namespace Medication.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Medication.Domain;

    public static class MedicationContextSetup
    {
        public static void Prepare(MedicationDbContext context, ILogger logger)
        {
            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();
            }

            if (!context.Medications.Any())
            {
                for (int i = 1; i < 100; i++)
                {
                    context.Medications.Add(new Medication(Guid.NewGuid(), $"Medication {i}", (25 * i) % 100 + i, DateTime.UtcNow));
                }
                context.SaveChanges();
            }
        }
    }
}
