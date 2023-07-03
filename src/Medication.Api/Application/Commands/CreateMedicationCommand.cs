namespace Medication.Api.Application.Commands
{
    using FluentValidation;
    using MediatR;

    public sealed record CreateMedicationCommand(string Name, int Quantity)
       : IRequest<CreateMedicationResult>;

    public sealed record CreateMedicationResult(Guid Id, string Name, int Quantity, DateTime CreationDate);

    public sealed class CreateMedicationCommandValidator
        : AbstractValidator<CreateMedicationCommand>
    {
        public CreateMedicationCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(c => c.Quantity).NotEmpty().WithMessage("Quantity must be greater than 0");
        }
    }
}
