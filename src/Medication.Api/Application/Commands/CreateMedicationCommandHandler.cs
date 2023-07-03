namespace Medication.Api.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Medication.Domain;
    using Medication.Infrastructure.Services;

    internal sealed class CreateMedicationCommandHandler : IRequestHandler<CreateMedicationCommand, CreateMedicationResult>
    {
        private readonly IMedicationRepository _medicationRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateMedicationCommandHandler(IMedicationRepository medicationRepository, IDateTimeProvider dateTimeProvider)
        {
            _medicationRepository = medicationRepository ?? throw new ArgumentNullException(nameof(medicationRepository));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<CreateMedicationResult> Handle(CreateMedicationCommand request, CancellationToken cancellationToken)
        {
            var medication = new Medication(
                Guid.NewGuid(),
                request.Name,
                request.Quantity,
                _dateTimeProvider.UtcNow);

            medication = await _medicationRepository.AddAsync(medication, cancellationToken);

            _ = await _medicationRepository.SaveChangesAsync(cancellationToken);

            return new CreateMedicationResult(
                medication.Id,
                medication.Name,
                medication.Quantity,
                medication.CreatedAt);
        }
    }
}
