using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VirtualVendingMachine.Exceptions;
using VirtualVendingMachine.Helpers;

namespace VirtualVendingMachine.Filters;

public class InsufficientFundsExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not InsufficientFundsException fundsException)
            return;

        var requiredTopUp = CurrencyFormatter.CentsAsCurrency(
            fundsException.ExpectedFunds - fundsException.ReceivedFunds
        );
        var totalProductCost =
            CurrencyFormatter.CentsAsCurrency(fundsException.ExpectedFunds);

        var message =
            $"Please insert {requiredTopUp} to purchase {fundsException.ProductName} " +
            $"with total cost of {totalProductCost}";

        context.Result = new ContentResult
        {
            Content = message,
            ContentType = MediaTypeNames.Text.Plain,
            StatusCode = (int)HttpStatusCode.BadRequest
        };
    }
}