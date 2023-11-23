namespace Cayent.Core.CQRS.Queries
{
    public interface IQueryHandlerDispatcher
    {
        /// <summary>
        /// Creates the query handler for the given query, then executes it, then returns TResult
        /// </summary>
        /// <typeparam name="TQuery">iquery</typeparam>
        /// <typeparam name="TResult">object</typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TResult> HandleAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
            where TQuery : IQuery<TResult>
            where TResult : class;
    }
}
