using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Queries
{
    public interface IQueryHandlerFactory
    {
        /// <summary>
        /// Creates a query handler for the given query and result
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        IQueryHandler<TQuery, TResult> Create<TQuery, TResult>() where TQuery : IQuery<TResult> where TResult : class;
    }
}
