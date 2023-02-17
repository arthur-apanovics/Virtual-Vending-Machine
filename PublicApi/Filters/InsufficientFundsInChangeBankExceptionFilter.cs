using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VirtualVendingMachine.Exceptions;

namespace VirtualVendingMachine.Filters;

public class InsufficientFundsInChangeBankExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is InsufficientFundsInChangeBankException)
        {
            context.Result = new ContentResult
            {
                Content =
                    "Sorry, there we are unable to provide change for your request. " +
                    "Please request a refund or try purchasing a different product",
                ContentType = MediaTypeNames.Text.Plain,
                StatusCode = (int)HttpStatusCode.NoContent
            };
        }
    }
}