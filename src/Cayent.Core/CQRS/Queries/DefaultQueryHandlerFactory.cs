using Cayent.Core.CQRS.Services;

namespace Cayent.Core.CQRS.Queries
{
    public sealed class DefaultQueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IContainer _container;

        public DefaultQueryHandlerFactory(IContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        IQueryHandler<TQuery, TResult> IQueryHandlerFactory.Create<TQuery, TResult>()
        {
            var handler = _container.Resolve<IQueryHandler<TQuery, TResult>>();

            return handler;
        }
    }
}
