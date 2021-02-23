using Cayent.CQRS.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Queries
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
