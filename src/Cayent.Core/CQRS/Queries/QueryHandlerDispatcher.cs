namespace Cayent.Core.CQRS.Queries
{
    public sealed class QueryHandlerDispatcher : IQueryHandlerDispatcher
    {
        private readonly IQueryHandlerFactory _factory;

        public QueryHandlerDispatcher(IQueryHandlerFactory factory)
        {
            _factory = factory;
        }

        async Task<TResult> IQueryHandlerDispatcher.HandleAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
        {
            var handler = _factory.Create<TQuery, TResult>();

            var result = await handler.HandleAsync(query, cancellationToken);

            return result;
        }
    }
}
