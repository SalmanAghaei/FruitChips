using System.Net;
using System.Threading.Tasks;
using Core.Application.Queries;

namespace Core.Application.Queries
{
    public abstract class QueryHandler<TQuery, TData> : IQueryHandler<TQuery, TData>
        where TQuery : class, IQuery<TData>
    {

        protected readonly QueryResult<TData> result = new QueryResult<TData>();

        protected virtual Task<QueryResult<TData>> ResultAsync(TData data, HttpStatusCode status)
        {
            result._data = data;
            result.Status = status;
            return Task.FromResult(result);
        }

        protected virtual QueryResult<TData> Result(TData data, HttpStatusCode status)
        {
            result._data = data;
            result.Status = status;
            return result;
        }


        protected virtual Task<QueryResult<TData>> ResultAsync(TData data)
        {
            var status = data != null ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            return ResultAsync(data, status);
        }

        protected virtual QueryResult<TData> Result(TData data)
        {
            var status = data != null ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            return Result(data, status);
        }

        public QueryHandler()
        {

        }

        public abstract Task<QueryResult<TData>> Handle(TQuery request);
    }
}
