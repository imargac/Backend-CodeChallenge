namespace Medication.Api.Endpoints
{
    using MediatR;
    using Medication.Api.Application.Commands;
    using System.Net.Mime;

    internal static class CreateMedicationEndpoint
    {
        public static void Map(WebApplication app, string groupTag)
        {
            app.MapPost("/medications", async (CreateMedicationCommand createMedicationRequest, ISender sender) =>
            {
                var result = await sender.Send(createMedicationRequest);

                return Results.CreatedAtRoute(GetMedicationEndpoint.GetMedicationEndpointName, new { result.Id }, result);
            })
            .WithTags(groupTag)
            .Accepts<CreateMedicationCommand>(MediaTypeNames.Application.Json)
            .Produces<CreateMedicationResult>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}
