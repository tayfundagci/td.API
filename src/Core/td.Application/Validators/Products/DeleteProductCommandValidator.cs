﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Application.Features.Products.Commands;

namespace td.Application.Validators.Products
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Product {PropertyName} is required.")
                .NotNull().WithMessage("Product {PropertyName} is required.");
        }
    }
}
