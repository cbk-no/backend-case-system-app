using FluentValidation;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Validation;

public class CreateProjectRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
        RuleFor(x => x.CaseId).NotEmpty();
        RuleFor(x => x.Status).NotEmpty();
        RuleFor(x => x.AssignedUserId).NotEmpty();
    }
}
