using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using td.Domain.Entities;
using td.Shared.Enums;
using td.Application.Wrappers;

namespace td.WebApi.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<enmRole> _userRoles;

        public AuthorizeAttribute(params enmRole[] userRoles)
        {
            _userRoles = userRoles ?? new enmRole[] { };
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;
            if (context != null && context.HttpContext != null)
            {
                var user = context.HttpContext.Items["User"] as User;
                if (user == null || _userRoles.Any() && !_userRoles.Contains(user.Role))
                {
                    context.Result = new JsonResult(new BaseResponse() { Message = "Unauthorized!" , Success = false}) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}
