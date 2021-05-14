namespace Cayent.Core.CQRS.Commands
{
    public interface ICommandHandlerFactory
    {
        /// <summary>
        /// Creates a command handler for the command
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <returns></returns>
        ICommandHandler<TCommand> Create<TCommand>() where TCommand : ICommand;
    }
}
