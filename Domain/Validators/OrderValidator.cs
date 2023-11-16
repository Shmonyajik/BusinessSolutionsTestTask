using FluentValidation;
using SolutionsForBuisnesTestTask.DAL;
using SolutionsForBuisnesTestTask.Domain.Models;

namespace SolutionsForBuisnesTestTask.Domain.Validators
{
    public class OrderValidator :AbstractValidator<Order>
    {
        private readonly IBaseRepository<Order> _orderRepository;
        public OrderValidator(IBaseRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Number is required.");

            RuleFor(x => x.ProviderId)
                .NotNull().WithMessage("Provider is required.");

            RuleFor(x => x)
                .Must(BeUniqueCombination)
                .WithMessage("Combination of Number and Provider must be unique");

            RuleFor(x => x)
               .Must(ItemsNamesNotEquealOrderNumber)
               .When(o => o.Items != null)
               .WithMessage("The order item name cannot be the same as the order number");
        }

        private bool BeUniqueCombination(Order order)
        {
            return !_orderRepository.GetBy()
                .Any(o => o.ProviderId == order.ProviderId && o.Number == order.Number && o.Id!=order.Id);
                    
        }
        private bool ItemsNamesNotEquealOrderNumber(Order order)
        {
            foreach (var item in order.Items)
            {
                if(item.Name==order.Number)
                    return false;
            }
            return true;
        }
    }
}
