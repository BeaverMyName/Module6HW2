using System.Net;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(
        ILogger<CatalogTypeController> logger,
        ICatalogTypeService catalogTypeService)
    {
        _logger = logger;
        _catalogTypeService = catalogTypeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddTypeResponse<int>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Create(CreateTypeRequest request)
    {
        var result = await _catalogTypeService.CreateTypeAsync(request.Type);
        return Ok(new AddTypeResponse<int>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeleteTypeResponse<int>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Delete(int id)
    {
        await _catalogTypeService.DeleteTypeAsync(id);
        return Ok(new DeleteTypeResponse<int>() { Id = id });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateTypeResponse<int>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Update(UpdateTypeRequest request)
    {
        await _catalogTypeService.UpdateTypeAsync(request.Id, request.Type);
        return Ok(new UpdateTypeResponse<int>() { Id = request.Id });
    }
}
