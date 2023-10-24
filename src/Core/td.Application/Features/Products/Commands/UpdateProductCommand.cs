using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Application.Dto;
using td.Application.Interfaces;
using td.Application.Wrappers;
using td.Domain.Entities;

namespace td.Application.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<ServiceResponse<ProductDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Quantity { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ServiceResponse<ProductDto>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;
            public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }
            public async Task<ServiceResponse<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var productObj = new Product
                {
                    Id = request.Id,
                    Name = request.Name,
                    Value = request.Value,
                    Quantity = request.Quantity,
                };
                var product = await _productRepository.UpdateProduct(productObj);
                var productDto = _mapper.Map<ProductDto>(product);
                return new ServiceResponse<ProductDto>(productDto, "Product Updated", true);
            }
        }
    }
}
