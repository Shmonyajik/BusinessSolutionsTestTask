using Microsoft.AspNetCore.Mvc.Rendering;
using SolutionsForBuisnesTestTask.DAL;
using SolutionsForBuisnesTestTask.DAL.Repositories;
using SolutionsForBuisnesTestTask.Domain;
using SolutionsForBuisnesTestTask.Domain.Models;
using SolutionsForBuisnesTestTask.Domain.Validators;
using SolutionsForBuisnesTestTask.Domain.ViewModels;
using SolutionsForBuisnesTestTask.Services.Interfaces;

namespace SolutionsForBuisnesTestTask.Services.Implementations
{
    public class OrderCRUDService : IOrderCRUDService
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<OrderItem> _itemRepository;
        private readonly IBaseRepository<Provider> _providerRepository;
        
        public OrderCRUDService(IBaseRepository<Order> orderRepository,
            IBaseRepository<OrderItem> itemRepository,
            IBaseRepository<Provider> providerRepository)
        {
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
            _providerRepository = providerRepository;
            
        }
        public async Task<BaseResponse<HomeVM>> GetOrders()
        {
            var baseResponse = new BaseResponse<HomeVM>();
            try
            {
                var ordersNumbers = _orderRepository.GetBy().Select(o => new SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = o.Number

                }).Distinct().ToList();

                var itemsNames = _itemRepository.GetBy().Select(i => i.Name).Distinct().ToList();

                var itemsUnits = _itemRepository.GetBy().Select(i => i.Unit).Distinct().ToList();

                var providers = await _providerRepository.GetAll();

                var orders = await _orderRepository.GetAll();

                var homeVM = new HomeVM
                {
                    OrdersNumbers = ordersNumbers,
                    ItemsNames = new SelectList(itemsNames),
                    ItemsUnits = new SelectList(itemsUnits),
                    Providers = providers.Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name
                    }).ToList(),
                    Orders = orders

                };

                baseResponse.Data = homeVM;
                baseResponse.StatusCode = 200;


            }
            catch (Exception ex)
            {
                baseResponse.StatusCode = 500;
                baseResponse.Description = ex.Message;

            }
            return baseResponse;


        }

        public async Task<BaseResponse<Order>> GetOrderDetails(int? id)
        {
            var baseResponse = new BaseResponse<Order>();
            try
            {
                if (id == null)
                {
                    baseResponse.StatusCode = 404;
                    baseResponse.Description = "Incorrect order ID";
                    return baseResponse;
                }

                var order = await _orderRepository.GetFullInfoById((int)id);
                if (order == null)
                {
                    baseResponse.StatusCode = 404;
                    baseResponse.Description = "Incorrect order ID";
                    return baseResponse;
                }

                baseResponse.Data = order;
                baseResponse.StatusCode = 200;
            }
            catch (Exception ex)
            {

                baseResponse.StatusCode = 500;
                baseResponse.Description = ex.Message;
            }
            return baseResponse;
        }

        public async Task<BaseResponse<bool>> DeleteOrder(int? id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                if (id == null)
                {
                    baseResponse.StatusCode = 404;
                    baseResponse.Description = "Incorrect order ID";
                    return baseResponse;
                }

                var order = await _orderRepository.GetById((int)id);
                if (order == null)
                {
                    baseResponse.StatusCode = 404;
                    baseResponse.Description = "Incorrect order ID";
                    return baseResponse;
                }
                await _orderRepository.Delete(order);

                baseResponse.Data = true;
                baseResponse.StatusCode = 200;
            }
            catch (Exception ex)
            {

                baseResponse.StatusCode = 500;
                baseResponse.Description = ex.Message;
            }
            return baseResponse;
        }

        public async Task<BaseResponse<OrderVM>> CreateEditOrderGet(int? id)
        {
            var baseResponse = new BaseResponse<OrderVM>();
            try
            {
                var providers = await _providerRepository.GetAll();
                var orderVM = new OrderVM
                {
                    Order = new Order { Date = DateTime.Now},
                    Providers = new SelectList(providers, nameof(Provider.Id), nameof(Provider.Name)),
                };
                if (id == null)
                {
                    baseResponse.Data = orderVM;
                    baseResponse.StatusCode = 200;
                    return baseResponse;
                }
                var order = await _orderRepository.GetFullInfoById((int)id);
                if (order == null)
                {
                    baseResponse.StatusCode = 404;
                    baseResponse.Description = "Incorrect order ID";
                    return baseResponse;
                }

                baseResponse.Data = new OrderVM
                {
                    Order = order,
                    Providers = new SelectList(providers, nameof(Provider.Id), nameof(Provider.Name)),
                    SelectedProviderId = order.ProviderId

                };
                baseResponse.StatusCode = 200;
            }
            catch (Exception ex)
            {
                baseResponse.StatusCode = 500;
                baseResponse.Description = ex.Message;
            }
            return baseResponse;
        }

        public async Task<BaseResponse<bool>> CreateEditOrderPost(Order order)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                order.Date = order.Date.ToLocalTime();/*TimeZoneInfo.ConvertTimeFromUtc(order.Date, TimeZoneInfo.GetSystemTimeZones().First());*/
                var validator = new OrderValidator(_orderRepository);
                var validationResult = validator.Validate(order);
                if(!validationResult.IsValid)
                {
                    baseResponse.StatusCode = 400;
                    baseResponse.Data = false;
                    baseResponse.Description = validationResult.ToString();
                    return baseResponse;
                }
                if(order.Id==0)
                {
                    foreach (var item in order.Items)
                    {
                        item.Id = 0;
                    }
                    await _orderRepository.Create(order); 
                }
                else
                {
                    var originalOrder = await _orderRepository.GetById(order.Id);

                    if(originalOrder==null)
                    {
                        baseResponse.StatusCode = 404;
                        baseResponse.Data = false;
                        baseResponse.Description = "The order with the specified Id was not found";
                        return baseResponse;
                    }

                    await _orderRepository.UpdateWithLinkedData(order); 
                    #region old
                    ////var newItems = order.Items.Where(o=>o.Id<0).ToList();
                    ////order.Items = order.Items.Where(o => o.Id > 0).ToList();

                    ////await _orderRepository.Update(order);
                    ////await _context.Items.AddRangeAsync(newItems);
                    //var existingOrder = await _orderRepository.GetFullInfoById(order.Id);

                    //existingOrder.Number = order.Number;
                    //existingOrder.Date = order.Date;
                    //existingOrder.Provider = order.Provider;
                    //existingOrder.Items = order.Items;

                    ////if (existingOrder.Items != null && order.Items != null)
                    ////{
                    ////    _context.Entry(existingOrder.Items).CurrentValues.SetValues(order.Items);
                    ////}
                    //await _orderRepository.Update(existingOrder);
                    #endregion

                }
                
                baseResponse.StatusCode = 201;
                baseResponse.Data = true;
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