using Catalog.API.Interfaces;
using Catalog.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [ResponseCache(Duration = 30)]
    public async Task<IActionResult> Products()
    {
        return Ok(await _productService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product is null)
            return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        await _productService.CreateAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Product newProduct)
    {
        var product = await _productService.GetByIdAsync(newProduct.Id);
        if (product is null)
            return NotFound();
        await _productService.UpdateAsync(newProduct);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product is null)
            return NotFound();
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}