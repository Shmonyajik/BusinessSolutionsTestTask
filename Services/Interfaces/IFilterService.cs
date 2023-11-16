using SolutionsForBuisnesTestTask.Domain;
using SolutionsForBuisnesTestTask.Domain.Models;
using SolutionsForBuisnesTestTask.Domain.ViewModels;

namespace SolutionsForBuisnesTestTask.Services.Interfaces
{
    public interface IFilterService
    {
        

        Task<BaseResponse<IEnumerable<Order>>> GetOrdersByFilters(FilterVM filterVM);
    }
}
