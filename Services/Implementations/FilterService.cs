using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SolutionsForBuisnesTestTask.DAL;
using SolutionsForBuisnesTestTask.Domain;
using SolutionsForBuisnesTestTask.Domain.Models;
using SolutionsForBuisnesTestTask.Domain.ViewModels;
using SolutionsForBuisnesTestTask.Services.Interfaces;
using System.Globalization;
using System;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace SolutionsForBuisnesTestTask.Services.Implementations
{
    public class FilterService : IFilterService
    {
        private readonly IBaseRepository<Order> _orderRepository;
        public FilterService(IBaseRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;       
        }
        
        
        public async Task<BaseResponse<IEnumerable<Order>>> GetOrdersByFilters(FilterVM filterVM)
        {
            var baseResponse = new BaseResponse<IEnumerable<Order>>();
            try
            {
               
                if (filterVM==null)
                {
                    baseResponse.Data = await _orderRepository.GetAll();
                    baseResponse.StatusCode = 200;
                    return baseResponse;
                }
                List<Order>? ordersFound = null;
                DateTime startDate;
                DateTime endDate;
                DateTime.TryParse(filterVM.StartDate, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out startDate);
                DateTime.TryParse(filterVM.EndDate, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out endDate);
                
                ordersFound = _orderRepository.GetBy()
                    .Where(o => filterVM.OrderIds.Count > 0 ? filterVM.OrderIds.Contains(o.Id) : true)
                    .Where(o => filterVM.ProviderIds.Count > 0 ? filterVM.ProviderIds.Contains(o.ProviderId) : true)
                    .Where(o => o.Date<= endDate && o.Date >= startDate)
                    .Include(o => o.Items).Include(o => o.Provider).ToList();

                if (ordersFound == null || ordersFound.Count > 0)
                {
                    if(filterVM.ItemUnits.Count>0) 
                    {
                        ordersFound = ordersFound == null
                        ? _orderRepository.GetBy()
                            .Where(o => o.Items != null && o.Items
                            .Any(i =>i != null && filterVM.ItemUnits.Contains(i.Unit) && (filterVM.ItemNames.Count>0?filterVM.ItemNames.Contains(i.Name):true)))
                            .Include(o => o.Items).Include(o => o.Provider).ToList()
                        : ordersFound
                            .Where(o => o.Items != null && o.Items
                            .Any(i =>i != null && filterVM.ItemUnits.Contains(i.Unit) && (filterVM.ItemNames.Count > 0 ? filterVM.ItemNames.Contains(i.Name) : true))).ToList();
                    }
                    else if(filterVM.ItemNames.Count>0)
                    {
                        ordersFound = ordersFound == null
                        ? _orderRepository.GetBy()
                            .Where(o => o.Items != null && o.Items
                            .Any(i => i != null &&  filterVM.ItemNames.Contains(i.Name)))
                            .Include(o => o.Items).Include(o => o.Provider).ToList()
                        : ordersFound
                            .Where(o => o.Items != null && o.Items
                            .Any(i => i != null &&  filterVM.ItemNames.Contains(i.Name))).ToList();
                    }

                }
                    #region Old
                    //if (filterVM.OrderIds.Count>0)
                    //{
                    //    ordersFound = _orderRepository.GetBy().Where(o => filterVM.OrderIds.Contains(o.Id)).Include(o=>o.Items).Include(o=>o.Provider).ToList();  
                    //}

                    //if(ordersFound == null || ordersFound.Count>0)
                    //{
                    //    if(filterVM.ProviderIds.Count>0)
                    //    {
                    //        ordersFound = ordersFound == null
                    //        ? _orderRepository.GetBy().Where(o => filterVM.ProviderIds.Contains(o.ProviderId)).Include(o => o.Items).Include(o => o.Provider).ToList()
                    //        : ordersFound.Where(o => filterVM.ProviderIds.Contains(o.ProviderId)).ToList();
                    //    }

                    //}
                    //if(ordersFound == null || ordersFound.Count > 0)
                    //{
                    //    if(filterVM.ItemNames.Count > 0)
                    //    {
                    //        ordersFound = ordersFound == null
                    //        ? _orderRepository.GetBy()
                    //        .Where(o => o.Items != null && o.Items.Any(i => i != null && filterVM.ItemNames.Contains(i.Name))).Include(o => o.Items).Include(o => o.Provider).ToList()
                    //        : ordersFound
                    //        .Where(o => o.Items != null && o.Items.Any(i => i != null && filterVM.ItemNames.Contains(i.Name))).ToList();
                    //    }

                    //}
                    //if (ordersFound == null || ordersFound.Count > 0)
                    //{
                    //    if(filterVM.ItemUnits.Count>0) 
                    //    {
                    //        ordersFound = ordersFound == null
                    //        ? _orderRepository.GetBy()
                    //        .Where(o => o.Items != null && o.Items.Any(i =>i != null &&/**/ filterVM.ItemUnits.Contains(i.Unit))).Include(o => o.Items).Include(o => o.Provider).ToList()
                    //        : ordersFound
                    //        .Where(o => o.Items != null && o.Items.Any(i =>i != null &&/**/ filterVM.ItemUnits.Contains(i.Unit))).ToList();
                    //    }

                    //}
                    //if (ordersFound == null || ordersFound.Count > 0)
                    //{
                    //    ordersFound = ordersFound == null
                    //    ? _orderRepository.GetBy()
                    //    .Where(o => o.Date >= startDate && o.Date <= endDate).ToList()
                    //    : ordersFound
                    //    .Where(o => o.Date >= startDate && o.Date <= endDate).ToList();
                    //}
                    #endregion
                baseResponse.Data = ordersFound==null?new List<Order>():ordersFound;
                baseResponse.StatusCode = 200;
                

            }
            catch (Exception ex)
            {
                baseResponse.StatusCode = 500;
                baseResponse.Description = ex.Message;
            }
            return baseResponse;
        }

        
    }
}
