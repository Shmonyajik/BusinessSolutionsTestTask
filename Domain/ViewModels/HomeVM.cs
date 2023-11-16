using Microsoft.AspNetCore.Mvc.Rendering;
using SolutionsForBuisnesTestTask.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SolutionsForBuisnesTestTask.Domain.ViewModels
{
    public class HomeVM
    {
        public HomeVM()
        {
            StartDate = DateTime.Now.AddMonths(-1);
            EndDate = DateTime.Now;
        }

        public SelectList ItemsNames { get; set; }

        public string SelectedItemName { get; set; }

        public SelectList ItemsUnits { get; set; }
        public string SelectedItemUnit { get; set; }
        public List<SelectListItem> OrdersNumbers { get; set; }
        public int SelectedOrderId { get; set; }

        public List<SelectListItem> Providers { get; set; }

        public int SelectedProviderId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public IEnumerable<Order> Orders{get;set;}

    }
}
