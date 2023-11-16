using SolutionsForBuisnesTestTask.Domain;
using SolutionsForBuisnesTestTask.Domain.Models;
using SolutionsForBuisnesTestTask.Domain.ViewModels;

namespace SolutionsForBuisnesTestTask.Services.Interfaces
{
    public interface IOrderCRUDService
    {
        Task<BaseResponse<HomeVM>> GetOrders();
        
        Task<BaseResponse<Order>> GetOrderDetails(int? id);
        Task<BaseResponse<bool>> DeleteOrder(int? id);
        Task<BaseResponse<OrderVM>> CreateEditOrderGet(int? id);

        Task<BaseResponse<bool>> CreateEditOrderPost(Order order);

    }
}
