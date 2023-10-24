using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Application.Features.Products.Commands;

namespace td.Application.Validators.Products
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product {PropertyName} is required.")
                .NotNull().WithMessage("Product {PropertyName} is required.");

            RuleFor(p => p.Value)
                .NotEmpty().WithMessage("Product {PropertyName} is required.")
                .NotNull().WithMessage("Product {PropertyName} is required.")
                .GreaterThan(0).WithMessage("Product {PropertyName} must be greater than 0.");

            RuleFor(p => p.Quantity)
                .NotEmpty().WithMessage("Product {PropertyName} is required.")
                .NotNull().WithMessage("Product {PropertyName} is required.")
                .GreaterThan(0).WithMessage("Product {PropertyName} must be greater than 0.");


        }
    }
}
