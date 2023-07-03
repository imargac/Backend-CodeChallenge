namespace Medication.Api.Endpoints
{
    using Microsoft.EntityFrameworkCore;
    using Medication.Api.Endpoints.Common;
    using Medication.Infrastructure.Persistence;

    internal static class GetMedicationEndpoint
    {
        internal const string GetMedicationEndpointName = "GetMedication";

        public static void Map(WebApplication app, string groupTag)
        {
            app.MapGet("/medications/{id}", async (Guid id, MedicationDbContext dbContext) =>
            {
                if (id == Guid.Empty)
                {
                    return Results.Problem(statusCode: StatusCodes.Status400BadRequest, detail: $"Id must not be empty");
                }

                var medication = await dbContext.Medications.SingleOrDefaultAsync(m => m.Id == id);
                if (medication == null)
                {
                    return Results.Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Resource with id {id} not found");
                }

                var result = new MedicationResult(medication.Id, medication.Name, medication.Quantity, medication.CreatedAt);

                return Results.Ok(result);
            })
            .WithName(GetMedicationEndpointName)
            .WithTags(groupTag)
            .Produces<MedicationResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
