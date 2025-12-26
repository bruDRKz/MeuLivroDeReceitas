using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System.Net;

namespace MyRecipeBook.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)//detalhar mais depois -> explicar o conceito para fixar 
        {
            if (context.Exception is MyRecipeBookException)
                HandleProjectException(context);

            else
                ThrowUnknowException(context);
        }
        private void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException)
            {
                var exception = context.Exception as ErrorOnValidationException;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception!.ErrorMessages));
            }
        }
        private void ThrowUnknowException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesExceptions.NAME_EMPTY)); //mudar a mensagem depois -> é UNKNOWNERROR, preciso add no resource
            }
        }
    }
}
