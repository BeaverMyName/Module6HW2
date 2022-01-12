using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly IMapper _mapper;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _mapper = mapper;
    }

    public async Task<CatalogItemDto> GetByIdAsync(int id)
    {
        return await ExecuteSafe(async () =>
        {
            var result = await _catalogItemRepository.GetByIdAsync(id);
            return _mapper.Map<CatalogItemDto>(result);
        });
    }

    public async Task<IEnumerable<CatalogItemDto>> GetByBrandAsync(string brand)
    {
        return await ExecuteSafe(async () =>
        {
            var result = await _catalogItemRepository.GetByBrandAsync(brand);
            return _mapper.Map<IEnumerable<CatalogItemDto>>(result);
        });
    }

    public async Task<IEnumerable<CatalogItemDto>> GetByTypeAsync(string type)
    {
        return await ExecuteSafe(async () =>
        {
            var result = await _catalogItemRepository.GetByTypeAsync(type);
            return _mapper.Map<IEnumerable<CatalogItemDto>>(result);
        });
    }

    public Task<int> CreateProductAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return ExecuteSafe(() => _catalogItemRepository.Add(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
    }

    public Task<int> DeleteProductAsync(int id)
    {
        return ExecuteSafe(() => _catalogItemRepository.Delete(id));
    }

    public Task<int> UpdateProductAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return ExecuteSafe(() => _catalogItemRepository.Update(id, name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
    }
}