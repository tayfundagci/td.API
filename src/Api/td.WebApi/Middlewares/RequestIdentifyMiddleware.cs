

using Microsoft.IdentityModel.Logging;
using td.Application.Interfaces;

namespace Matchermania.Api.Middlewares
{
    public class RequestIdentifyMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestIdentifyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserRepository userRepository, IJwtService jwtService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var userId = jwtService.ValidateToken(token);
                if (userId != Guid.Empty)
                    context.Items["User"] = await userRepository.GetByIdAsync(userId);
                await _next(context);
            }
            else
            {
                try
                {
                    await _next(context);
                }
                catch (Exception e)
                {
                    LogHelper.LogExceptionMessage(e);
                }
            }
        }
    }
}
