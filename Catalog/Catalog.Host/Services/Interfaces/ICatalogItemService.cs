using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int> CreateProductAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<int> DeleteProductAsync(int id);
    Task<int> UpdateProductAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItemDto> GetByIdAsync(int id);
    Task<IEnumerable<CatalogItemDto>> GetByBrandAsync(string brand);
    Task<IEnumerable<CatalogItemDto>> GetByTypeAsync(string type);
}