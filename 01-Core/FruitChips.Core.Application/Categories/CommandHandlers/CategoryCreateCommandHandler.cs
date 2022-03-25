using Mapster;
using Core.Application.Commands;
using I6.Utilities.Services.Commands;
using FruitChips.Core.Domain.Categories.Entities;
using FruitChips.Core.Contracts.Categories.Commands;
using FruitChips.Core.Contracts.Features.Repositories;

namespace FruitChips.Core.Application.Categories.CommandHandlers
{
    internal class CategoryCreateCommandHandler : CommandHandler<CategoryCreateCommand>
    {
        private readonly ICategoryCommandRepository _categoryCommandRepository;

        public CategoryCreateCommandHandler(ICategoryCommandRepository categoryCommandRepository)
        {
            _categoryCommandRepository = categoryCommandRepository;
        }

        public override async Task<CommandResult> Handle(CategoryCreateCommand request)
        {
            await _categoryCommandRepository.InsertAsync(request.Adapt<Category>());
            _categoryCommandRepository.Commit();
            return Ok();
        }
    }
}
