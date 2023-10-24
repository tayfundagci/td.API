using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Domain.Entities;
using td.Shared.Enums;

namespace td.Application.Dto
{
    public class UserDto : BaseDto<UserDto, User>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public enmRole Role { get; set; }
    }
}
