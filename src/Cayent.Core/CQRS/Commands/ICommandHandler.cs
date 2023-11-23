namespace Cayent.Core.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        Task HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}
