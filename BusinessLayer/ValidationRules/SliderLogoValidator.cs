using EntityLayer.Entities;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class SliderLogoValidator : AbstractValidator<SliderLogo>
    {
        public SliderLogoValidator()
        {
            RuleFor(x => x.PopUpTitle).NotEmpty().WithMessage("Team Member Name can not be null.");
            RuleFor(x => x.PopUpSubTitle).NotEmpty().WithMessage("Team Member Title can not be null.");
            RuleFor(x => x.PopUpDescription).NotEmpty().WithMessage("Team Member Title can not be null.");
            RuleFor(x => x.SliderTitle).NotEmpty().WithMessage("Team Member Title can not be null.");
            RuleFor(x => x.SliderDescription).NotEmpty().WithMessage("Team Member Title can not be null.");

            RuleFor(x => x.SliderTitle).MinimumLength(6).WithMessage("Team Member Name can not be less than 6 char.");
            RuleFor(x => x.SliderTitle).MaximumLength(15).WithMessage("Team Member Title can not be more than 30 char.");

            RuleFor(x => x.SliderDescription).MinimumLength(6).WithMessage("Team Member Name can not be less than 6 char.");
            RuleFor(x => x.SliderDescription).MaximumLength(25).WithMessage("Team Member Title can not be more than 30 char.");

            RuleFor(x => x.PopUpTitle).MinimumLength(6).WithMessage("Team Member Name can not be less than 6 char.");
            RuleFor(x => x.PopUpTitle).MaximumLength(20).WithMessage("Team Member Title can not be more than 30 char.");

            RuleFor(x => x.PopUpSubTitle).MinimumLength(6).WithMessage("Team Member Name can not be less than 6 char.");
            RuleFor(x => x.PopUpSubTitle).MaximumLength(30).WithMessage("Team Member Title can not be more than 30 char.");

            RuleFor(x => x.PopUpDescription).MinimumLength(6).WithMessage("Team Member Name can not be less than 6 char.");
            RuleFor(x => x.PopUpDescription).MaximumLength(30).WithMessage("Team Member Title can not be more than 30 char.");
        }
    }
}
