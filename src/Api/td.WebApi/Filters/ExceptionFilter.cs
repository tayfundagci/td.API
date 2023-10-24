using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.Common;
using System.Net;
using System.Security.Authentication;
using td.Application.Exceptions;
using td.Application.Messages;
using td.Application.Wrappers;

namespace td.WebApi.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        public ExceptionFilter()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(ModelValidationException), HandleModelValidationException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }
        }

        private void HandleModelValidationException(ExceptionContext context)
        {
            var exception = (ModelValidationException)context.Exception;

            var response = new ValidationResponse(exception.Errors, "Validation Error");

            context.Result = new ObjectResult(response);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            context.ExceptionHandled = true;
        }
    }
}
