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

namespace td.Application.Features.Products.Queries
{
    public class GetProductQuery : IRequest<ServiceResponse<ProductDto>>
    {
        public Guid Id { get; set; }
        public class GetProductHandler : IRequestHandler<GetProductQuery, ServiceResponse<ProductDto>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public GetProductHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetProduct(request.Id);
                var productDto = _mapper.Map<ProductDto>(product);
                return new ServiceResponse<ProductDto>(productDto, "Success", true);
            }
        }


    }
}
