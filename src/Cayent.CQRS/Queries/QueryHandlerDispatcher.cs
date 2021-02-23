using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.CQRS.Queries
{
    public sealed class QueryHandlerDispatcher : IQueryHandlerDispatcher
    {
        readonly IQueryHandlerFactory _factory;

        public QueryHandlerDispatcher(IQueryHandlerFactory factory)
        {
            _factory = factory;
        }

        public Task<TResult> HandleAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
            where TResult : class// IResponse
        {
            var handler = _factory.Create<TQuery, TResult>();

            var result = handler.HandleAsync(query);

            return result;
        }
    }
}
