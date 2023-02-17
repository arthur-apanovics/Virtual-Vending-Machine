using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualVendingMachine.Requests;

namespace VirtualVendingMachine.Controllers;

[ApiController]
[Route("[controller]")]
public class VendingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<VendingController> _logger;

    public VendingController(
        IMediator mediator,
        ILogger<VendingController> logger
    )
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response =
            await _mediator.Send(new ListAvailableProductsAndStockRequest());

        return Ok(response);
    }

    [HttpGet("accepted-coins")]
    public async Task<IActionResult> GetAcceptedCoins()
    {
        var response = await _mediator.Send(new ListAcceptedCoinsRequest());

        return Ok(response);
    }

    [HttpGet("inserted-funds")]
    public async Task<IActionResult> GetInsertedFundsAmount()
    {
        var response = await _mediator.Send(new GetInsertedFundsRequest());

        return Ok(response);
    }

    [HttpPut("insert-coin")]
    public async Task<IActionResult> InsertCoin(
        [FromBody] InsertCoinRequest request
    )
    {
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("purchase")]
    public async Task<IActionResult> MakePurchase(
        [FromBody] PurchaseProductRequest request
    )
    {
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("cancel-request")]
    public async Task<IActionResult> CancelAndRefund()
    {
        var response = await _mediator.Send(new CancelAndRefundRequest());

        return Ok(response);
    }
}