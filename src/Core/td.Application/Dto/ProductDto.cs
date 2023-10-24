using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Domain.Entities;

namespace td.Application.Dto
{
    public class ProductDto : BaseDto<ProductDto, Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Quantity { get; set; }
    }
}
