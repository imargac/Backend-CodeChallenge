namespace Medication.Domain
{
    public interface IMedicationRepository
    {
        Task<Medication> AddAsync(Medication medication, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
