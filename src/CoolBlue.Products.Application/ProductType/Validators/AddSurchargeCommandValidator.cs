using CoolBlue.Products.Application.Common.Interfaces;
using CoolBlue.Products.Application.ProductType.Commands;
using FluentValidation;

namespace CoolBlue.Products.Application.Product.Validators
{
    class AddSurchargeCommandValidator : AbstractValidator<AddSurchargeCommand>
    {
        public AddSurchargeCommandValidator(IInsuranceDbContext context)
        {
            RuleFor(v => v.SurchargeRate)
                .NotEmpty().WithMessage("Surcharge rate is required.")
                .GreaterThanOrEqualTo(0.1).WithMessage("Surcharge rate cannot be lower than 0.1.");
            RuleFor(v => v.ProductTypeId)
            .NotEmpty().WithMessage("Product Type Id is required.");
        }
    }
}
