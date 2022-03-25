using FluentValidation;

namespace Core.Application.Queries
{
    public class PageQueryValidator<T> : AbstractValidator<T>
    where T : IPageQuery
    {
        public PageQueryValidator()
        {
            RuleFor(x => x.PageNumber).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
        }
    }
}
