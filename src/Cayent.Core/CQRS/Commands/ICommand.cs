namespace Cayent.Core.CQRS.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// Business transaction id
        /// </summary>
        string CorrelationId { get; }
        /// <summary>
        /// The tenant Id
        /// </summary>
        string TenantId { get; }
        /// <summary>
        /// The current user id
        /// </summary>
        string UserId { get; }

    }
}
