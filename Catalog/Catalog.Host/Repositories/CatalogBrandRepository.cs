using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CatalogBrandRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<IEnumerable<CatalogBrand>> GetBrandsAsync()
        {
            var items = await _dbContext.CatalogBrands.ToListAsync();
            return items;
        }

        public async Task<int> Add(string brand)
        {
            var item = _dbContext.CatalogBrands.Add(new CatalogBrand
            {
                Brand = brand
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<int> Delete(int id)
        {
            _dbContext.CatalogBrands.Remove(await _dbContext.CatalogBrands.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException());

            await _dbContext.SaveChangesAsync();

            return id;
        }

        public async Task<int> Update(int id, string brand)
        {
            var item = _dbContext.CatalogBrands.FirstOrDefault(i => i.Id == id) ?? throw new NullReferenceException();

            item.Brand = brand;

            _dbContext.CatalogBrands.Update(item);
            await _dbContext.SaveChangesAsync();

            return id;
        }
    }
}
