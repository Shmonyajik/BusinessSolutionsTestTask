using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SolutionsForBuisnesTestTask.Domain.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        [NotNull]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        [NotNull]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 7)")]
        [NotNull]
        public decimal Quantity { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        [NotNull]
        public string Unit {get; set; }


    }
}
