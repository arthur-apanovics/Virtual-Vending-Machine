using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VirtualVendingMachine.Exceptions;

namespace VirtualVendingMachine.Filters;

public class ProductOutOfStockExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ProductOutOfStockException stockException)
        {
            context.Result = new ContentResult
            {
                Content =
                    "Sorry, this machine is out of stock " +
                    $"for {stockException.ProductOutOfStock.Name}",
                ContentType = MediaTypeNames.Text.Plain,
                StatusCode = (int)HttpStatusCode.ServiceUnavailable
            };
        }
    }
}