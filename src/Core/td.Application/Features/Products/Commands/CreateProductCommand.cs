using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Application.Dto;
using td.Application.Interfaces;
using td.Application.Messages;
using td.Domain.Entities;

namespace td.Application.Features.Products.Commands
{
    public class CreateProductCommand : IRequest<ServiceResponse<Guid>>
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int Quantity { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResponse<Guid>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;
            public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }
            public async Task<ServiceResponse<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var productObj = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Value = request.Value,
                    Quantity = request.Quantity,
                    CreateDate = DateTime.Now
                };
                var product = await _productRepository.CreateProduct(productObj);
                return new ServiceResponse<Guid>(product, "Success", true);
            }
        }
    }
}
