namespace Medication.Api.Application.Commands
{
    using FluentValidation;
    using MediatR;

    public sealed record DeleteMedicationCommand(Guid Id) : IRequest<bool>;

    public sealed class DeleteMedicationCommandValidator
        : AbstractValidator<DeleteMedicationCommand>
    {
        public DeleteMedicationCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id cannot be empty");
        }
    }
}
