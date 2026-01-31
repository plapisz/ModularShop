using Microsoft.AspNetCore.Mvc;
using ModularShop.Modules.Catalog.Core.Dtos;
using ModularShop.Modules.Catalog.Core.Services;

namespace ModularShop.Modules.Catalog.Api.Controllers;

[ApiController]
[Route("api/catalog/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var product = await productService.GetAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }
    
    [HttpGet]
    public async Task<IActionResult> BrowseAsync() 
        => Ok(await productService.BrowseAsync());

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductDto dto)
    {
        var id = await productService.CreateAsync(dto);
        
        return CreatedAtAction(nameof(Get), new { Id = id }, dto);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateProductDto dto)
    {
        await productService.UpdateAsync(dto with { Id = id });
        
        return NoContent();
    }

    [HttpPut("{id:guid}/deactivate")]
    public async Task<IActionResult> DeactivateAsync(Guid id)
    {
        await productService.DeactivateAsync(id);
        
        return NoContent();
    }
}