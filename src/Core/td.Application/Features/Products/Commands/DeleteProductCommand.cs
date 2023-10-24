using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Application.Interfaces;
using td.Application.Messages;

namespace td.Application.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest<ServiceResponse<Guid>>
    {
        public Guid Id { get; set; }
        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ServiceResponse<Guid>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;
            public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }
            public async Task<ServiceResponse<Guid>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetProduct(request.Id);
                if (product == null)
                {
                    return new ServiceResponse<Guid>(Guid.Empty, "Product not found", false);
                }

                await _productRepository.DeleteProduct(product);
                return new ServiceResponse<Guid>(product.Id, "Product Deleted", true);
            }
        }

    }
}
