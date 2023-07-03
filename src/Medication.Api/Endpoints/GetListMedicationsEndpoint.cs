namespace Medication.Api.Endpoints
{
    using Microsoft.EntityFrameworkCore;
    using Medication.Api.Endpoints.Common;
    using Medication.Infrastructure.Persistence;

    internal static class GetListMedicationsEndpoint
    {
        private const int MinPageSize = 1;
        private const int MinPageIndex = 1;

        public static void Map(WebApplication app, string groupTag)
        {
            app.MapGet("/medications", async (int pageSize, int pageIndex, MedicationDbContext dbContext) =>
            {
                if (!AreParametersValid(pageSize, pageIndex, out var message))
                {
                    return Results.Problem(statusCode: StatusCodes.Status400BadRequest, detail: message);
                }

                var totalNumberMedications = await dbContext.Medications.LongCountAsync();
                var medications = await dbContext.Medications
                    .OrderBy(m => m.CreatedAt)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToListAsync();

                var paginatedResult = new PaginatedMedicationsResult(
                    pageIndex,
                    pageSize,
                    totalNumberMedications,
                    medications.Select(m => new MedicationResult(m.Id, m.Name, m.Quantity, m.CreatedAt)));

                return Results.Ok(paginatedResult);
            })
            .WithTags(groupTag)
            .WithOpenApi(generatedOperation =>
            {
                var pageSizeParameter = generatedOperation.Parameters[0];
                pageSizeParameter.Description = $"{pageSizeParameter.Name} must be greater or equal than {MinPageSize}";
                var pageIndexParameter = generatedOperation.Parameters[1];
                pageIndexParameter.Description = $"{pageIndexParameter.Name} must be greater or equal than {MinPageIndex}";
                return generatedOperation;
            })
            .Produces<PaginatedMedicationsResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
        }

        private static bool AreParametersValid(int pageSize, int pageIndex, out string message)
        {
            if (pageSize < MinPageSize)
            {
                message = $"{nameof(pageSize)} must be greater or equal than {MinPageSize}";
                return false;
            }

            if (pageIndex < MinPageIndex)
            {
                message = $"{nameof(pageIndex)} must be greater or equal than {MinPageIndex}";
                return false;
            }

            message = string.Empty;
            return true;
        }
    }
}
