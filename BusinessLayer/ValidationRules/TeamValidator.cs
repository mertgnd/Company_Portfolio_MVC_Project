using EntityLayer.Entities;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class TeamValidator : AbstractValidator<Team>
    {
        public TeamValidator()
        {
            RuleFor(x => x.PersonName).NotEmpty().WithMessage("Team Member Name can not be null.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Team Member Title can not be null.");
            RuleFor(x => x.PersonName).MaximumLength(50).WithMessage("Team Member Name can not be more than 50 char.");
            RuleFor(x => x.PersonName).MinimumLength(6).WithMessage("Team Member Name can not be less than 6 char.");
            RuleFor(x => x.Title).MaximumLength(30).WithMessage("Team Member Title can not be more than 30 char.");
            RuleFor(x => x.Title).MinimumLength(3).WithMessage("Team Member Title can not be less than 3 char.");
        }
    }
}