using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VirtualVendingMachine.Exceptions;

namespace VirtualVendingMachine.Filters;

public class NotSupportedCoinExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is NotSupportedCoinException coinException)
        {
            context.Result = new ContentResult
            {
                Content =
                    $"Sorry, this machine does not accept {coinException.UnsupportedCoin} coins",
                ContentType = MediaTypeNames.Text.Plain,
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
    }
}