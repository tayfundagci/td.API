using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Application.Dto;
using td.Application.Features.Products.Commands;

namespace td.Application.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Domain.Entities.Product, ProductDto>().ReverseMap();
            CreateMap<Domain.Entities.Product, CreateProductCommand>().ReverseMap();
            CreateMap<Domain.Entities.User, UserDto>().ReverseMap();
        }
    }
}
