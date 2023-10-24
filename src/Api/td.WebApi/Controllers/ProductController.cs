﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using td.Application.Dto;
using td.Application.Features.Products.Commands;
using td.Application.Features.Products.Queries;
using td.Application.Wrappers;
using td.Domain.Entities;
using td.Shared.Enums;
using td.WebApi.Attributes;

namespace td.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiControllerBase
    {

        [AllowAnonymous]
        [Route("list")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> List(GetAllProductsQuery query)
        {
            return await Mediator.Send(query);
        }

        [Route("detail")]
        [HttpPost]

        public async Task<ActionResult<ServiceResponse<ProductDto>>> Detail(GetProductQuery query)
        {
            return await Mediator.Send(query);
        }

        [Route("create")]
        [HttpPost]
        public async Task <ActionResult<ServiceResponse<Guid>>> Create(CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [Route("delete")]
        [HttpPost]
        public async Task <ActionResult<ServiceResponse<Guid>>> Delete(DeleteProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [Route("update")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> Update(UpdateProductCommand command)
        {
            return await Mediator.Send(command);
        }

    }
}
