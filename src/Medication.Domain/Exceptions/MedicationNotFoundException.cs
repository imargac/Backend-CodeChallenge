namespace Medication.Domain.Exceptions
{
    public class MedicationNotFoundException : Exception
    {
        public MedicationNotFoundException(Guid medicationId) 
            : base($"Medication with id {medicationId} not found")
        {
        }
    }
}
