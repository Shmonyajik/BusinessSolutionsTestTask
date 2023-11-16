using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SolutionsForBuisnesTestTask.Domain.Models
{
    public class Provider
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        [NotNull]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
