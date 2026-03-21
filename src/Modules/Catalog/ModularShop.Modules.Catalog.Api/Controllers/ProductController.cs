using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModularShop.Modules.Catalog.Api.Security;
using ModularShop.Modules.Catalog.Core.Dtos;
using ModularShop.Modules.Catalog.Core.Services;

namespace ModularShop.Modules.Catalog.Api.Controllers;

[Authorize(Policy = Policies.Catalog.Read)]
[ApiController]
[Route("api/catalog/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var product = await productService.GetAsync(id, cancellationToken);
        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }
    
    [HttpGet]
    public async Task<IActionResult> BrowseAsync(CancellationToken cancellationToken) 
        => Ok(await productService.BrowseAsync(cancellationToken));

    [HttpPost]
    [Authorize(Policy = Policies.Catalog.Write)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
    {
        var id = await productService.CreateAsync(dto, cancellationToken);
        
        return CreatedAtAction(nameof(Get), new { Id = id }, dto);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy = Policies.Catalog.Write)]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateProductDto dto, CancellationToken cancellationToken)
    {
        await productService.UpdateAsync(dto with { Id = id }, cancellationToken);
        
        return NoContent();
    }

    [HttpPut("{id:guid}/deactivate")]
    [Authorize(Policy = Policies.Catalog.Write)]
    public async Task<IActionResult> DeactivateAsync(Guid id, CancellationToken cancellationToken)
    {
        await productService.DeactivateAsync(id, cancellationToken);
        
        return NoContent();
    }
}