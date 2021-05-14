using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cayent.Core.CQRS.Commands
{
    public sealed class DefaultCommandHandlerDispatcher : ICommandHandlerDispatcher
    {
        private readonly ICommandHandlerFactory _factory;

        public DefaultCommandHandlerDispatcher(ICommandHandlerFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException("factory");
        }

        async Task ICommandHandlerDispatcher.HandleAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
        {
            var handler = _factory.Create<TCommand>();

            await handler.HandleAsync(command, cancellationToken);
        }

    }
}
