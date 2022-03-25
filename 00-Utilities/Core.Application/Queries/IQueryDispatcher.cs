﻿using System.Threading.Tasks;
using Core.Application.Queries;

namespace Core.Application.Queries
{


    /// <summary>
    /// تعریف ساختار الگوی Mediator جهت اتصال ساده کوئری‌ها به هندلر‌ها
    /// </summary>
    public interface IQueryDispatcher
    {
        Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query) where TQuery : class, IQuery<TData>;
    }
}
