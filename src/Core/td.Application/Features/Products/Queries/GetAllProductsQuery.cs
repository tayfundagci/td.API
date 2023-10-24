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

namespace td.Application.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<ServiceResponse<List<ProductDto>>>
    {
        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, ServiceResponse<List<ProductDto>>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public GetAllProductQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<List<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var productList = await _productRepository.GetProducts();
                var productDtoList = _mapper.Map<List<ProductDto>>(productList);
                return new ServiceResponse<List<ProductDto>>(productDtoList, "Success", true);
            }
        }
    }
}
