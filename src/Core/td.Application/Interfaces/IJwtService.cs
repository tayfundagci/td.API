using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Domain.Entities;

namespace td.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        Guid ValidateToken(string token);
    }
}
