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
    public async Task<IActionResult> Get()
    {
        var response =
            await _mediator.Send(new ListAvailableProductsAndStockRequest());

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> InsertCoin(
        [FromBody] InsertCoinRequest request
    )
    {
        var response = await _mediator.Send(request);

        return Ok(response);
    }
}