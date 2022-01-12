using System.Net;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;
    private readonly ICatalogItemService _catalogItemService;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService,
        ICatalogItemService catalogItemService)
    {
        _logger = logger;
        _catalogService = catalogService;
        _catalogItemService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _catalogItemService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByBrand(string brand)
    {
        var result = await _catalogItemService.GetByBrandAsync(brand);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByType(string type)
    {
        var result = await _catalogItemService.GetByTypeAsync(type);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<CatalogBrandDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBrands()
    {
        var result = await _catalogService.GetBrandsAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<CatalogTypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTypes()
    {
        var result = await _catalogService.GetTypesAsync();
        return Ok(result);
    }
}