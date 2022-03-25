using Mapster;
using Core.Application.Commands;
using I6.Utilities.Services.Commands;
using FruitChips.Core.Contracts.Categories.Commands;
using FruitChips.Core.Contracts.Features.Repositories;

namespace FruitChips.Core.Application.Categories.CommandHandlers
{
    internal class CategoryEditCommandHandler : CommandHandler<CategoryEditCommand>
    {
        private readonly ICategoryCommandRepository _categoryCommandRepository;

        public CategoryEditCommandHandler(ICategoryCommandRepository categoryCommandRepository)
        {
            _categoryCommandRepository = categoryCommandRepository;
        }

        public override async Task<CommandResult> Handle(CategoryEditCommand request)
        {
            var category = await _categoryCommandRepository.GetAsync(request.Id);
            if (category is null)
                return Result(System.Net.HttpStatusCode.NotFound);
            request.Adapt(category);
            _categoryCommandRepository.Commit();
            return Ok();
        }
    }
}
