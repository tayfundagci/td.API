using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Application.Interfaces;
using td.Domain.Entities;
using td.Persistence.Context;
using static Dapper.SqlMapper;

namespace td.Persistence.Repositories
{
    public class ProductRepository : DapperRepository, IProductRepository
    {
        private readonly DapperContext _context;

        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Product> GetProduct(Guid id)
        {
            string query = @"select * from products where id=@id";
            var product = await base.QueryFirstOrDefaultAsync<Product>(query, new { id = id });
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var query = "SELECT * FROM Products";
            var products = await base.QueryAsync<Product>(query);
            return products;
        }

        public async Task<Guid> CreateProduct(Product product)
        {
            string query = @"insert into products (id, name, value, quantity, createdate) values (@id, @name, @value, @quantity, @createdate)";

            var parameters = new DynamicParameters();
            parameters.Add("id", product.Id, DbType.Guid);
            parameters.Add("name", product.Name, DbType.String);
            parameters.Add("value", product.Value, DbType.Int16);
            parameters.Add("quantity", product.Quantity, DbType.Int16);
            parameters.Add("createdate", product.CreateDate, DbType.DateTime);

            await base.ExecuteAsync(query, parameters);
            return product.Id;
        }

        public async Task<Guid> DeleteProduct(Product product)
        {
            string query = @"delete from products where id=@id";
            var parameters = new DynamicParameters();
            parameters.Add("id", product.Id, DbType.Guid);

            await base.ExecuteAsync(query, parameters);
            return product.Id;
        }  
        
        public async Task<Product> UpdateProduct(Product product)
        {
            string query = @"update products set name=@name, value=@value, quantity=@quantity where id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("name", product.Name, DbType.String);         
            parameters.Add("value", product.Value, DbType.Int16);         
            parameters.Add("quantity", product.Quantity, DbType.Int16);         
            parameters.Add("id", product.Id, DbType.Guid);

            await base.ExecuteAsync(query, parameters);
            return product;
        }
        
    }
}
