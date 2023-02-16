using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine.Controllers;

[ApiController]
[Route("[controller]")]
public class VendingController : ControllerBase
{
    private readonly ILogger _logger;

    public VendingController(

        ILogger logger
    )
    {
        _logger = logger;
    }

    [HttpGet]
    public Product Get()
    {
        return Product.Coke;
    }
}