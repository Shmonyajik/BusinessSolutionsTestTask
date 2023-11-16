using SolutionsForBuisnesTestTask.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SolutionsForBuisnesTestTask.Domain.ViewModels
{
    public class FilterVM
    {
        public List<int> OrderIds { get; set; }
        public List<string> ItemNames { get; set; }

        public List<string> ItemUnits { get; set; }
        public List<int> ProviderIds { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        
    }
}
