using Core.Application.Commands;
using FluentValidation;

namespace FruitChips.Core.Contracts.Categories.Commands
{
    public class CategoryEditValidator : AbstractValidator<CategoryEditCommand>
    {
        public CategoryEditValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(256);
            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
    public class CategoryEditCommand : ICommand
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
