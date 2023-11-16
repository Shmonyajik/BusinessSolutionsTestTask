
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

using SolutionsForBuisnesTestTask.Domain.Models;
using SolutionsForBuisnesTestTask.Domain.ViewModels;
using SolutionsForBuisnesTestTask.Services.Interfaces;

namespace SolutionsForBuisnesTestTask.Controllers
{
    [Consumes("application/json")]
    public class OrderController : Controller
    {
        private readonly IOrderCRUDService _orderService;

        public OrderController(IOrderCRUDService orderService)
        {
            _orderService = orderService;
        }

        
        public async Task<IActionResult> Details(int? id)
        {

            var response = await _orderService.GetOrderDetails(id);
            if (response.StatusCode != 200)
            {
                return View("Error", new ErrorVM { StatusCode = response.StatusCode, Description = response.Description });
            }
            return View(response.Data);
        }
        [HttpPost]
        public async Task<JsonResult> Delete([FromBody]int? id)
        {
            var response = await _orderService.DeleteOrder(id);
            if (response.StatusCode != 200)
            {
                return Json(new { success = false, statuscode = response.StatusCode, description = response.Description });
            }
            return Json(new {success = true, statuscode = response.StatusCode});
        }

        [HttpGet]
        public async Task<IActionResult> CreateEdit(int? id)
        {
            var response = await _orderService.CreateEditOrderGet(id);
            if (response.StatusCode != 200)
            {
                return View("Error", new ErrorVM { StatusCode = response.StatusCode, Description = response.Description });
            }
            return View(response.Data);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreteEditPost([FromBody] Order order)
        {
            
            var response = await _orderService.CreateEditOrderPost(order);
            if (response.StatusCode != 201)
            {
                return Json(new { success = false, statuscode = response.StatusCode, description = response.Description });
            }
            return Json(new { success = true, statuscode = response.StatusCode });
        }
        [HttpPost]
        public  IActionResult GetItemRow([FromBody] NewRowDataVM newRowData)
        {
            return PartialView("_NewRow", new OrderItem { Id = newRowData.rowCount, OrderId = newRowData.orderId});
        }

        

        //.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Number,Date,ProviderId")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name", order.ProviderId);
        //    return View(order);
        //}


        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Orders == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name", order.ProviderId);
        //    return View(order);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Date,ProviderId")] Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(order);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderExists(order.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name", order.ProviderId);
        //    return View(order);
        //}





        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Orders == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
        //    }
        //    var order = await _context.Orders.FindAsync(id);
        //    if (order != null)
        //    {
        //        _context.Orders.Remove(order);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OrderExists(int id)
        //{
        //  return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
