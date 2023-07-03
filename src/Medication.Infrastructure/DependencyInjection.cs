namespace Medication.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Medication.Domain;
    using Medication.Infrastructure.Persistence;
    using Medication.Infrastructure.Persistence.Repositories;
    using Medication.Infrastructure.Services;

    public static class DependencyInjection
    {
        public static IServiceCollection AddMedicationInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMedicationRepository, MedicationRepository>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            var useOnlyInMemoryDatabase = false;
            if (configuration["UseOnlyInMemoryDatabase"] != null)
            {
                useOnlyInMemoryDatabase = bool.Parse(configuration["UseOnlyInMemoryDatabase"]!);
            }

            if (useOnlyInMemoryDatabase)
            {
                services.AddDbContext<MedicationDbContext>(c =>
                   c.UseInMemoryDatabase("MedicationDb"));
            }
            else
            {
                services.AddDbContext<MedicationDbContext>(c =>
                    c.UseSqlServer(configuration.GetConnectionString("MedicationsConnection")));
            }

            return services;
        }
    }
}
