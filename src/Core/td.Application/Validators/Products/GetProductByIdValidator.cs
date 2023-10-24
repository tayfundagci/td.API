using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Application.Features.Products.Queries;
using td.Application.Features.Users.Queries;

namespace td.Application.Validators.Products
{
    public class GetProductByIdValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductByIdValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Product {PropertyName} is required.")
                .NotNull();
        }
    }
}
