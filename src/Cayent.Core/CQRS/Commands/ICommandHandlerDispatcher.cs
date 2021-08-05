using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cayent.Core.CQRS.Commands
{
    public interface ICommandHandlerDispatcher
    {

        /// <summary>
        /// Calls the handle method of the command handler for the given command
        /// </summary>
        /// <typeparam name="TCommand">subclass of ICommand</typeparam>
        /// <param name="command">command to execute</param>
        Task HandleAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand;
    }
}
