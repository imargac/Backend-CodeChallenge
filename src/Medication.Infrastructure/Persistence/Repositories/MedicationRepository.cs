namespace Medication.Infrastructure.Persistence.Repositories
{
    using System;
    using System.Threading;
    using Microsoft.EntityFrameworkCore;
    using Medication.Domain;
    using Medication.Domain.Exceptions;

    internal class MedicationRepository : IMedicationRepository
    {
        private readonly MedicationDbContext _context;

        public MedicationRepository(MedicationDbContext context)
        {
            _context = context;
        }

        public async Task<Medication> AddAsync(Medication medication, CancellationToken cancellationToken = default)
        {
            var entityEntry = await _context.Medications.AddAsync(medication, cancellationToken);

            return entityEntry.Entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var medication = await _context.Medications.SingleOrDefaultAsync(m => m.Id == id, cancellationToken) 
                ?? throw new MedicationNotFoundException(id);

            _context.Medications.Remove(medication);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0;
        }
    }
}
