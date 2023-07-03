namespace Medication.Domain.Exceptions
{
    public class MedicationException : Exception
    {
        public MedicationException(string message) : base(message)
        {
        }
    }
}
