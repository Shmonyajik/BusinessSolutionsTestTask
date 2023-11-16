using FluentValidation;
using SolutionsForBuisnesTestTask.Domain.ViewModels;

namespace SolutionsForBuisnesTestTask.Domain.Validators
{
    public class FilterVMValidator: AbstractValidator<FilterVM>
    {
        public FilterVMValidator() { 
            RuleFor(r=>r.StartDate).NotEmpty().LessThanOrEqualTo(r=>r.EndDate);
        }
    }
}
