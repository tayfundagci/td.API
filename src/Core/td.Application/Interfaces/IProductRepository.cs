using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Domain.Entities;

namespace td.Application.Interfaces
{
    public interface IProductRepository 
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProduct(Guid id);
        public Task<Guid> CreateProduct(Product product);
        public Task<Guid> DeleteProduct(Product product);
        public Task<Product> UpdateProduct(Product product);
    }
}
