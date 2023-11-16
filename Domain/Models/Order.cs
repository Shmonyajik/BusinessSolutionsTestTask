using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace SolutionsForBuisnesTestTask.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        [DisplayName("Number")]
        [NotNull]
        public string Number { get; set; }
        [Column(TypeName = "datetime2(7)")]
        [DisplayName("Date")]
        [NotNull]
        public DateTime Date { get; set; }
        [NotNull]
        public int ProviderId { get; set; }

        public Provider Provider { get; set; }

        public ICollection<OrderItem> Items { get; set; }


    }
}
