using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.CQRS.Commands
{
    public sealed class DefaultCommandHandlerDispatcher : ICommandHandlerDispatcher
    {
        private readonly ICommandHandlerFactory _factory;

        public DefaultCommandHandlerDispatcher(ICommandHandlerFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException("factory");
        }

        //public void Handle<TCommand>(TCommand command) where TCommand : ICommand
        //{
        //    var handler = _factory.Create<TCommand>();

        //    handler.Handle(command);
        //}

        Task ICommandHandlerDispatcher.HandleAsync<TCommand>(TCommand command)
        {
            var handler = _factory.Create<TCommand>();

            var result = handler.HandleAsync(command);

            return result;
        }

    }
}
