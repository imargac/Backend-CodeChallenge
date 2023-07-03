namespace Medication.Api.Extensions
{
    using Medication.Infrastructure.Persistence;

    internal static class ApplicationExtensions
    {
        public static void PrepareDatabase(this WebApplication app) 
        {
            using var scope = app.Services.CreateScope();
            var scopedProvider = scope.ServiceProvider;
            try
            {
                var medicationDbContext = scopedProvider.GetRequiredService<MedicationDbContext>();
                MedicationContextSetup.Prepare(medicationDbContext, app.Logger);
            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "Error preparing the Database.");
            }
        }
    }
}
