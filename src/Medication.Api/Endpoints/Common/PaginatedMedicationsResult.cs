namespace Medication.Api.Endpoints.Common
{
    internal sealed record PaginatedMedicationsResult(int PageIndex, int PageSize, long Count, IEnumerable<MedicationResult> Medications);
}
