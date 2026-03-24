using FluentValidation;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Validation;

public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.OwnerId).NotEmpty();
    }
}
