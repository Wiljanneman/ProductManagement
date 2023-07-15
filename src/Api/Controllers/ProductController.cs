﻿using Application.Products.Commands;
using Application.Products.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductController : BaseController
{
    //[HttpGet]
    //public async Task<IActionResult> GetProducts()
    //{
    //    var query = new GetProductsQuery();
    //    var result = await Mediator.Send(query);
    //    return Ok(result);
    //}

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetProductById(int id)
    //{
    //    var query = new GetProductByIdQuery { Id = id };
    //    var result = await _mediator.Send(query);
    //    if (result == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(result);
    //}

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
            return BadRequest(ex.Message);
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductVM product)
    {
        var result = await Mediator.Send(new UpdateProductCommand { Product = product });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromBody] ProductVM product)
    {
        var result = await Mediator.Send(new DeleteProductCommand { Product = product });
        return Ok(result);
    }
}