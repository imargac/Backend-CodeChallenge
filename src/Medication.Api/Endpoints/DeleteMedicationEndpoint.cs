namespace Medication.Api.Endpoints
{
    using MediatR;
    using Medication.Api.Application.Commands;
    using Medication.Domain.Exceptions;

    internal static class DeleteMedicationEndpoint
    {
        public static void Map(WebApplication app, string groupTag)
        {
            app.MapDelete("/medications/{id}", async (Guid id, ISender sender) =>
            {
                var deleteCommand = new DeleteMedicationCommand(id);
                try
                {
                    await sender.Send(deleteCommand);
                }
                catch (MedicationNotFoundException)
                {
                    return Results.Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Resource with id {id} not found");
                }

                return Results.NoContent();
            })
            .WithOpenApi(generatedOperation =>
            {
                var idParameter = generatedOperation.Parameters[0];
                idParameter.Description = "Example: AAD94187-25A0-45B4-8E97-21C4EFBFA1D0";
                return generatedOperation;
            })
            .WithTags(groupTag)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
