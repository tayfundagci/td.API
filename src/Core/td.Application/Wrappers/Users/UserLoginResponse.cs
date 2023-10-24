using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Application.Dto;

namespace td.Application.Wrappers.Users
{
    public class UserLoginResponse
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
