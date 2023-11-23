﻿using Cayent.Core.CQRS.Services;

namespace Cayent.Core.CQRS.Commands
{
    public sealed class DefaultCommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IContainer _container;

        public DefaultCommandHandlerFactory(IContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        ICommandHandler<TCommand> ICommandHandlerFactory.Create<TCommand>()
        {
            var handler = _container.Resolve<ICommandHandler<TCommand>>();

            return handler;
        }
    }
}
