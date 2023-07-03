namespace Medication.Api.Endpoints.Common
{
    internal sealed record MedicationResult(Guid id, string name, int quantity, DateTime creationDate);
}
