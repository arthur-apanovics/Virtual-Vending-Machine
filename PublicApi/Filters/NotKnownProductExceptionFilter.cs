using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VirtualVendingMachine.Exceptions;

namespace VirtualVendingMachine.Filters;

public class NotKnownProductExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is NotKnownProductException exception)
        {
            context.Result = new ContentResult
            {
                Content =
                    "Sorry, this machine does not sell " +
                    $"{exception.UnknownProduct.Name} products",
                ContentType = MediaTypeNames.Text.Plain,
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
    }
}