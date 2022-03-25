using Core.Domain.Entities;
using System.Linq.Expressions;

namespace Core.Contracts.Data.Commands
{

    /// <summary>
    /// در صورتی که داده‌ها به صورت عادی ذخیره سازی شوند از این Interface جهت تعیین اعمال اصلی موجود در بخش ذخیره سازی داده‌ها استفاده می‌شود.
    /// </summary>
    /// <typeparam name="TEntity">کلاسی که جهت ذخیره سازی انتخاب می‌شود</typeparam>
    public interface ICommandRepository<TEntity,Tkey> : IUnitOfWork,IScopeLifeTime
    where TEntity : BaseEntity<Tkey>

    {
        /// <summary>
        /// یک شی را با شناسه حذف می کند
        /// </summary>
        /// <param name="id">شناسه</param>
        void Delete(Tkey id);

        /// <summary>
        /// حذف یک شی به همراه تمامی فرزندان آن را انجام می دهد
        /// </summary>
        /// <param name="id">شناسه</param>
        void DeleteGraph(Tkey id);

        /// <summary>
        /// یک شی را دریافت کرده و از دیتابیس حذف می‌کند
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);


        void DeleteRange(Tkey[] ids);

        /// <summary>
        /// داده جدید را به دیتابیس اضافه می‌کند
        /// </summary>
        /// <param name="entity">نمونه داده‌ای که باید به دیتابیس اضافه شود.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// داده‌های جدید را به دیتابیس اضافه می‌کند
        /// </summary>
        /// <param name="entity">نمونه داده‌ای که باید به دیتابیس اضافه شود.</param>

        void InsertRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// داده‌ جدید را به دیتابیس اضافه می‌کند
        /// </summary>
        /// <param name="entity">نمونه داده‌ای که باید به دیتابیس اضافه شود.</param>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// داده‌های جدید را به دیتابیس اضافه می‌کند
        /// </summary>
        /// <param name="entity">نمونه داده‌ای که باید به دیتابیس اضافه شود.</param>

        Task InsertRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// یک شی را با شناسه از دیتابیس یافته و بازگشت می‌دهد.
        /// </summary>
        /// <param name="id">شناسه شی مورد نیاز</param>
        /// <returns>نمونه ساخته شده از شی</returns>
        TEntity Get(Tkey id);

        Task<TEntity> GetAsync(Tkey id);

        TEntity GetGraph(Tkey id);

        Task<TEntity> GetGraphAsync(Tkey id);

        bool Exists(Expression<Func<TEntity, bool>> expression);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    }
}