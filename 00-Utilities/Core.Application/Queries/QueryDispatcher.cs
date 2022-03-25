using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.Application.Queries
{

    public class QueryDispatcher : IQueryDispatcher
    {
        #region Fields
        private readonly IServiceProvider _serviceProvider;
        
        private readonly Stopwatch _stopwatch;
        #endregion

        #region Constructors
        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _stopwatch = new Stopwatch();
        }
        #endregion

        #region Query Dispatcher

        public Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query) where TQuery : class, IQuery<TData>
        {

            _stopwatch.Start();
            try
            {
                
                var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TData>>();
                return handler.Handle(query);

            }
            catch (InvalidOperationException ex)
            {
                
                throw;
            }
            finally
            {
                _stopwatch.Stop();
                
            }


        }
        #endregion


    }
}
