using System.Net;
using Application.Products.Commands;
using Application.Products.Common;
using Application.Products.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductController : BaseController
{
    private readonly ILogger<ProductController> _logger;
    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            var query = new GetProductsQuery();
            var result = await Mediator.Send(query);
            
            return Ok(result);

        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error on products");
            return StatusCode(ex.HResult, new { error = ex.Message });
        }

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        try
        {
            var query = new GetProductByIdQuery { Id = id };
            var result = await Mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error on products");
            return StatusCode(ex.HResult, new { error = ex.Message });
        }

    }

    [HttpGet("VAT")]
    public IActionResult GetVAT()
    {
        try
        {
            return Ok(Application.Commons.Helpers.VATHelper.VATPercentage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on products");
            return StatusCode(ex.HResult, new { error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductVM product)
    {
        try
        {
            var result = await Mediator.Send(new CreateProductCommand { Product = product });
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error on products");
            return StatusCode(ex.HResult, new { error = ex.Message });
        }

    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductVM product)
    {
        try
        {
            var result = await Mediator.Send(new UpdateProductCommand { Product = product });
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error on products");
            return StatusCode(ex.HResult, new { error = ex.Message });
        }

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteProductCommand { Id = id });
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error on products");
            return StatusCode(ex.HResult, new { error = ex.Message });
        }

    }
}
