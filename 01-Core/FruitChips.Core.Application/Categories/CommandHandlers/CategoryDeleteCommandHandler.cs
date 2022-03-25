using Core.Application.Commands;
using I6.Utilities.Services.Commands;
using FruitChips.Core.Contracts.Categories.Commands;
using FruitChips.Core.Contracts.Features.Repositories;

namespace FruitChips.Core.Application.Categories.CommandHandlers
{
    internal class CategoryDeleteCommandHandler : CommandHandler<CategoryDeleteCommand>
    {
        private readonly ICategoryCommandRepository _categoryCommandRepository;

        public CategoryDeleteCommandHandler(ICategoryCommandRepository categoryCommandRepository)
        {
            _categoryCommandRepository = categoryCommandRepository;
        }

        public override async Task<CommandResult> Handle(CategoryDeleteCommand request)
        {

            var category = await _categoryCommandRepository.GetAsync(request.Id);
            if (category is null)
                return Result(System.Net.HttpStatusCode.NotFound);
            _categoryCommandRepository.Delete(category);
            _categoryCommandRepository.Commit();
            return Ok();
        }
    }
}
