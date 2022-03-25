using Core.Domain;

namespace Core.Application.Queries
{


    /// <summary>
    /// نتیجه یک کوئری را بازگشت می‌دهد
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public sealed class QueryResult<TData> : ApiResult
    {
        public TData? _data;
        public TData? Data
        {
            get
            {
                return _data;
            }
        }
    }
}
