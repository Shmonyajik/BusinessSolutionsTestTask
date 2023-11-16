using Microsoft.AspNetCore.Mvc.Rendering;
using SolutionsForBuisnesTestTask.Domain.Models;

namespace SolutionsForBuisnesTestTask.Domain.ViewModels
{
    public class OrderVM
    {
        public Order Order { get; set; }

        public SelectList Providers { get; set; }

        public int SelectedProviderId{ get; set; }
    }
}
