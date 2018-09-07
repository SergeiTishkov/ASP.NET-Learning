using FluentValidation;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace WebApplication3.Controllers
{
    public class MyValidationFilterAttribute:IExceptionFilter
    {
        public bool AllowMultiple => true;

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Exception.GetType() == typeof(ValidationException))
            {
                actionExecutedContext.Response = new System.Net.Http.HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest };
                //actionExecutedContext.Response.StatusCode = HttpStatusCode.BadRequest;
                //actionExecutedContext.Response.ReasonPhrase = actionExecutedContext.Exception.Message;
            }

            return Task.CompletedTask;
        }
    }
}