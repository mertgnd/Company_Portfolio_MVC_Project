using EntityLayer.Entities;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Mapinfo).NotEmpty().WithMessage("Map info can not be null.");
            RuleFor(x => x.Adress).NotEmpty().WithMessage("Adress can not be null.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City can not be null.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country can not be null.");

            RuleFor(x => x.Adress).MaximumLength(50).WithMessage("Adress can not be more than 50 char.");
            RuleFor(x => x.City).MaximumLength(15).WithMessage("City can not be more than 20 char.");
            RuleFor(x => x.Country).MaximumLength(15).WithMessage("Country can not be more than 20 char.");
        }
    }
}
