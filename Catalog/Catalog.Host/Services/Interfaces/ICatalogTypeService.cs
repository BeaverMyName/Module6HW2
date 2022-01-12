namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int> CreateTypeAsync(string name);
        Task<int> DeleteTypeAsync(int id);
        Task<int> UpdateTypeAsync(int id, string name);
    }
}
