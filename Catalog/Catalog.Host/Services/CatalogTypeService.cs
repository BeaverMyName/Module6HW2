using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;
        private readonly IMapper _mapper;

        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogTypeRepository catalogTypeRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogTypeRepository = catalogTypeRepository;
            _mapper = mapper;
        }

        public Task<int> CreateTypeAsync(string name)
        {
            return ExecuteSafe(() => _catalogTypeRepository.Add(name));
        }

        public Task<int> DeleteTypeAsync(int id)
        {
            return ExecuteSafe(() => _catalogTypeRepository.Delete(id));
        }

        public Task<int> UpdateTypeAsync(int id, string name)
        {
            return ExecuteSafe(() => _catalogTypeRepository.Update(id, name));
        }
    }
}
