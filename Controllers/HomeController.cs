using Microsoft.AspNetCore.Mvc;
using SolutionsForBuisnesTestTask.DAL;
using SolutionsForBuisnesTestTask.Domain.Models;
using SolutionsForBuisnesTestTask.Domain.ViewModels;
using SolutionsForBuisnesTestTask.Services.Interfaces;
using System.Diagnostics;

namespace SolutionsForBuisnesTestTask.Controllers
{
    [Consumes("application/json")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFilterService _filterService;
        private readonly IOrderCRUDService _orderService;

        public HomeController(ILogger<HomeController> logger, IFilterService filterService, IOrderCRUDService orderService )
        {
            _filterService = filterService;
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var response = await _orderService.GetOrders();
            if(response.StatusCode != 200)
            {
                return View("Error", new ErrorVM { StatusCode = response.StatusCode, Description = response.Description});
            }
            return View(response.Data);
        }
        [HttpPost]
        public async Task<IActionResult> OrdersByFilters([FromBody]FilterVM filterVM)
        {
            var response = await _filterService.GetOrdersByFilters(filterVM);

            if (response.StatusCode != 200)
            {
                return PartialView("_Error", new ErrorVM { StatusCode = response.StatusCode, Description = response.Description });
            }
            return PartialView("_OrdersTable", response.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}