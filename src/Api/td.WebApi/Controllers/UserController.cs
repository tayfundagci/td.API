using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using td.Application.Dto;
using td.Application.Features.Products.Queries;
using td.Application.Features.Users.Commands;
using td.Application.Features.Users.Queries;
using td.Application.Wrappers;
using td.Application.Wrappers.Users;

namespace td.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Register(UserRegisterCommand command)
        {
            return await Mediator.Send(command);
        }
        
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<UserLoginResponse>>> Login(UserLoginQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
