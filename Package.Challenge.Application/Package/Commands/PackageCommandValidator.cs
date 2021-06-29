using FluentValidation;
using FluentValidation.Validators;

namespace Package.Challenge.Application.Package.Commands
{
    public class PackageCommandValidator : AbstractValidator<PackageCommand>
    {
        public PackageCommandValidator()
        {
            RuleFor(t => t.FilePath).NotEmpty().NotNull();
        }
    }
}