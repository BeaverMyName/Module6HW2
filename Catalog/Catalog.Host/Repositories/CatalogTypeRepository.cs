using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<IEnumerable<CatalogType>> GetTypesAsync()
        {
            var items = await _dbContext.CatalogTypes.ToListAsync();
            return items;
        }

        public async Task<int> Add(string type)
        {
            var item = _dbContext.CatalogTypes.Add(new CatalogType
            {
                Type = type
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<int> Delete(int id)
        {
            _dbContext.CatalogTypes.Remove(await _dbContext.CatalogTypes.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException());

            await _dbContext.SaveChangesAsync();

            return id;
        }

        public async Task<int> Update(int id, string type)
        {
            var item = _dbContext.CatalogTypes.FirstOrDefault(i => i.Id == id) ?? throw new NullReferenceException();

            item.Type = type;

            _dbContext.CatalogTypes.Update(item);
            await _dbContext.SaveChangesAsync();

            return id;
        }
    }
}
