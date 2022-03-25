using Core.Application.Commands;
using FluentValidation;

namespace FruitChips.Core.Contracts.Categories.Commands
{

    public class CategoryDeleteValidator : AbstractValidator<CategoryDeleteCommand>
    {
        public CategoryDeleteValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
        }
    }
    public class CategoryDeleteCommand : ICommand
    {
        public int Id { get; set; }

    }
}
