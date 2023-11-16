using Microsoft.EntityFrameworkCore;
using SolutionsForBuisnesTestTask.Domain.Models;

namespace SolutionsForBuisnesTestTask.DAL.Repositories
{
    public class ProviderRepository : IBaseRepository<Provider>
    {
        private readonly ApplicationDbContext _context;
        public ProviderRepository(ApplicationDbContext context)
        {
                _context = context;
        }
        public Task Create(Provider model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Provider model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Provider>> GetAll()
        {
            return await _context.Providers.ToListAsync();
        }

        public IQueryable<Provider> GetBy()
        {
            throw new NotImplementedException();
        }

        public Task<Provider> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Provider> GetFullInfoById(int id)
        {
            throw new NotImplementedException();
        }


        public Task Update(Provider model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWithLinkedData(Provider model)
        {
            throw new NotImplementedException();
        }
    }
}
