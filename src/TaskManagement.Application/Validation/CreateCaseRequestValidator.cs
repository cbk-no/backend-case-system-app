using FluentValidation;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Validation;

public class CreateCaseValidator : AbstractValidator<CreateCaseRequest>
{
    public CreateCaseValidator()
    {
        RuleFor(x => x.ComplaintDescription).NotEmpty();
        RuleFor(x => x.Priority).NotEmpty();
        RuleFor(x => x.Status).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.EmailComplainer).NotEmpty().EmailAddress();
        RuleFor(x => x.UserInfoComplainer).NotEmpty();
        RuleFor(x => x.CaseOwnerId).NotEmpty();
    }
}