namespace Medication.Domain
{
    using Exceptions;

    public sealed class Medication
    {
        private const int MinimumRequiredQuantity = 1;

        public Guid Id { get; }

        public string Name { get; }

        public int Quantity { get; }

        public DateTime CreatedAt { get; }

        public Medication(Guid id, string name, int quantity, DateTime createdAt)
        {
            Id = (id == Guid.Empty)
                ? throw new MedicationException("Id must not be empty") 
                : id;

            Name = string.IsNullOrWhiteSpace(name) 
                ? throw new MedicationException("Invalid medication name") 
                : name;

            Quantity = quantity < MinimumRequiredQuantity 
                ? throw new MedicationException($"Medication quantity minimum is {MinimumRequiredQuantity}")
                : quantity;

            CreatedAt = createdAt;
        }
    }
}
