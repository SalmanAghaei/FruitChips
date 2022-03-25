using Core.Application.Commands;
using FluentValidation;

namespace FruitChips.Core.Contracts.Categories.Commands
{

    public class CategoryCreateValidator : AbstractValidator<CategoryCreateCommand>
    {
        public CategoryCreateValidator()
        {
            RuleFor(x=>x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(256);
            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }

    public class CategoryCreateCommand : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }





 
}
