using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogItems
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<CatalogItem> GetByIdAsync(int id)
    {
        var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(x => x.Id == id);
        return item ?? throw new NullReferenceException("Can't find item by id!");
    }

    public async Task<IEnumerable<CatalogItem>> GetByBrandAsync(string brand)
    {
        var items = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Where(i => i.CatalogBrand.Brand == brand)
            .ToListAsync();

        return items;
    }

    public async Task<IEnumerable<CatalogItem>> GetByTypeAsync(string type)
    {
        var items = await _dbContext.CatalogItems
            .Include(i => i.CatalogType)
            .Where(i => i.CatalogType.Type == type)
            .ToListAsync();

        return items;
    }

    public async Task<int> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = _dbContext.CatalogItems.Add(new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<int> Delete(int id)
    {
        _dbContext.CatalogItems.Remove(await _dbContext.CatalogItems.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException());

        await _dbContext.SaveChangesAsync();

        return id;
    }

    public async Task<int> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = _dbContext.CatalogItems.FirstOrDefault(i => i.Id == id) ?? throw new NullReferenceException();

        item.Name = name;
        item.Description = description;
        item.Price = price;
        item.AvailableStock = availableStock;
        item.CatalogBrandId = catalogBrandId;
        item.CatalogTypeId = catalogTypeId;
        item.PictureFileName = pictureFileName;

        _dbContext.CatalogItems.Update(item);
        await _dbContext.SaveChangesAsync();

        return id;
    }
}