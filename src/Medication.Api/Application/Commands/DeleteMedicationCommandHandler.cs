namespace Medication.Api.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Medication.Domain;

    internal sealed class DeleteMedicationCommandHandler : IRequestHandler<DeleteMedicationCommand, bool>
    {
        private readonly IMedicationRepository _medicationRepository;

        public DeleteMedicationCommandHandler(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository ?? throw new ArgumentNullException(nameof(medicationRepository));
        }

        public async Task<bool> Handle(DeleteMedicationCommand request, CancellationToken cancellationToken)
        {
            await _medicationRepository.DeleteAsync(request.Id, cancellationToken);

            return await _medicationRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
