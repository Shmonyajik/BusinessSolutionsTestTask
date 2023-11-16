using Microsoft.EntityFrameworkCore;
using SolutionsForBuisnesTestTask.Domain.Models;

namespace SolutionsForBuisnesTestTask.DAL.Repositories
{
    public class OrderItemRepository : IBaseRepository<OrderItem>
    {
        private readonly ApplicationDbContext _context;
        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(OrderItem model)
        {
            _context.Items.Add(model);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(OrderItem model)
        {
            _context.Items.Remove(model);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteById(int id)
        {
            var orderItem = await GetById(id);
            _context.Items.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
        public async Task<OrderItem> GetById(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<IEnumerable<OrderItem>> GetAll()
        {
            return await _context.Items.ToListAsync();
        }
        public IQueryable<OrderItem> GetBy()
        {
            return _context.Items;
        }


        public async Task Update(OrderItem model)
        {
            _context.Items.Update(model);
            await _context.SaveChangesAsync();
        }

        public Task<OrderItem> GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> GetFullInfoById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWithLinkedData(OrderItem model)
        {
            throw new NotImplementedException();
        }
    }
}
