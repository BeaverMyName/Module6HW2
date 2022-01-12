namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<int> CreateBrandAsync(string name);
        Task<int> DeleteBrandAsync(int id);
        Task<int> UpdateBrandAsync(int id, string name);
    }
}
