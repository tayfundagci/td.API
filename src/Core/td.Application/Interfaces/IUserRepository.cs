using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Domain.Entities;

namespace td.Application.Interfaces
{
    public interface IUserRepository : IGenericRepositoryAsync<User>
    {
        Task<User> GetByMail(string mail);

    }
}
