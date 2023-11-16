using SolutionsForBuisnesTestTask.Domain.Models;

namespace SolutionsForBuisnesTestTask.DAL
{
    
        public interface IBaseRepository<T>
        {
            Task Create(T model);

            IQueryable<T> GetBy();
            
            Task<IEnumerable<T>> GetAll();
            
            Task<T> GetById(int id);
            Task Delete(T model);

            Task Update(T model);
            Task<T> GetFullInfoById(int id);

            Task UpdateWithLinkedData(T model);



    }
    
}
