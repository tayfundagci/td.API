using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Application.Interfaces;
using td.Domain.Entities;
using td.Persistence.Context;

namespace td.Persistence.Repositories
{
    public class UserRepository : DapperRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Guid> AddAsync(User entity)
        {
            string query = @"INSERT INTO Users (Id,Email,Password,Role,CreateDate) VALUES (@Id,@Email,@Password,@Role,@CreateDate)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id);
            parameters.Add("Email", entity.Email);
            parameters.Add("Password", entity.Password);
            parameters.Add("Role", entity.Role);
            parameters.Add("CreateDate", entity.CreateDate);
            await base.ExecuteScalarAsync<Guid>(query, entity);
            return entity.Id;
        }

        public Task<User> GetByIdAsync(Guid Id)
        {
            string query = @"Select * from Users where Id=@Id ";
            return base.QueryFirstOrDefaultAsync<User>(query, new { Id = Id });
        }

        public async Task<User> GetByMail(string email)
        {
            string query = @"Select * from Users where email=@email ";
            return await base.QueryFirstOrDefaultAsync<User>(query, new { email = email });
        }
    }
}
