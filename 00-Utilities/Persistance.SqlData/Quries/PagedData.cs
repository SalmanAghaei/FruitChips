using Core.Application.Queries;
using Core.Contracts.Data.Queries;
namespace Persistance.SqlData.Quries
{

    /// <summary>
    /// ساختار پایه جهت بازگشت داده‌ها هنگام کوئری گرفتن وقتی که Paging دارد
    /// </summary>
    /// <typeparam name="T">نوع داده‌ای که از کوئری دریافت می‌شود را تعیین می‌کند!</typeparam>
    public class PagedData<T>
    {
        public PagedData()
        {

        }
        public PagedData(IPageQuery page)
        {
            PageNumber=page.PageNumber;
            PageSize=page.PageSize;
        }
        public List<T>? QueryResult { get; set; }
        private int PageNumber { get; set; } = 1;
        private int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }

    }
}
