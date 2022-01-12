using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
    {
        private readonly ICatalogBrandRepository _catalogBrandRepository;
        private readonly IMapper _mapper;

        public CatalogBrandService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogBrandRepository catalogBrandRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogBrandRepository = catalogBrandRepository;
            _mapper = mapper;
        }

        public Task<int> CreateBrandAsync(string name)
        {
            return ExecuteSafe(() => _catalogBrandRepository.Add(name));
        }

        public Task<int> DeleteBrandAsync(int id)
        {
            return ExecuteSafe(() => _catalogBrandRepository.Delete(id));
        }

        public Task<int> UpdateBrandAsync(int id, string name)
        {
            return ExecuteSafe(() => _catalogBrandRepository.Update(id, name));
        }
    }
}
