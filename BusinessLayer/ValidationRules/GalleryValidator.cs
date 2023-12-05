using EntityLayer.Entities;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class GalleryValidator : AbstractValidator<Gallery>
    {
        public GalleryValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Image Title Can not be null.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Image Description Can not be null.");

            RuleFor(x => x.Title).MaximumLength(20).WithMessage("Title can not be more than 15 char.");
            RuleFor(x => x.Title).MinimumLength(7).WithMessage("Title can not be less than 7 char.");

            RuleFor(x => x.Description).MaximumLength(50).WithMessage("Description can not be more than 40 char.");
            RuleFor(x => x.Description).MinimumLength(10).WithMessage("Description can not be less than 10 char.");
        }
    }
}