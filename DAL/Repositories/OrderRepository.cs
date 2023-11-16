using Microsoft.EntityFrameworkCore;
using SolutionsForBuisnesTestTask.Domain.Models;
using SolutionsForBuisnesTestTask.Domain.ViewModels;

namespace SolutionsForBuisnesTestTask.DAL.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(Order model)
        {
            _context.Orders.Add(model);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Order model)
        {
            _context.Orders.Remove(model);
            await _context.SaveChangesAsync();
        }
        public async Task<Order> GetById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>>? GetAll()
        {

            var orders = await _context.Orders.ToListAsync();
            return orders;
        }
        public IQueryable<Order> GetBy()
        {
            return _context.Orders;
        }


        public async Task Update(Order model)
        {
            _context.Orders.Update(model);
            await _context.SaveChangesAsync();
        }


        public async Task<Order> GetFullInfoById(int id)
        {
            return await _context.Orders.Include(o=>o.Provider).Include(o=>o.Items).FirstOrDefaultAsync(o=>o.Id== id);
        }

        public async Task UpdateWithLinkedData(Order model)
        {
            var originalOrder = await GetFullInfoById(model.Id);

            UpdateOrderItems(originalOrder.Items, model.Items);
            originalOrder.Number = model.Number;
            originalOrder.Date = model.Date;
            originalOrder.ProviderId = model.ProviderId;

            _context.Orders.Update(originalOrder);

            await _context.SaveChangesAsync();
        }
        private void UpdateOrderItems(ICollection<OrderItem> originalItems, ICollection<OrderItem> updatedItems)
        {

            foreach (var originalItem in originalItems.ToList())
            {
                if (!updatedItems.Any(i => i.Id == originalItem.Id))
                {
                    _context.Items.Remove(originalItem);
                }
            }

            foreach (var updatedItem in updatedItems)
            {
                if (updatedItem.Id < 0)
                {
                    updatedItem.Id = 0;
                    originalItems.Add(updatedItem);
                }
                else
                {
                    var originalItem = originalItems.FirstOrDefault(i => i.Id == updatedItem.Id);

                    if (originalItem != null)
                    {

                        originalItem.Name = updatedItem.Name;
                        originalItem.Quantity = updatedItem.Quantity;
                        originalItem.Unit = updatedItem.Unit;
                    }
                }
            }

            
        }
    }
}
