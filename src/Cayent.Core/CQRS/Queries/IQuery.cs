namespace Cayent.Core.CQRS.Queries
{
    public interface IQuery<out TResponse> where TResponse : class
    {
        string CorrelationId { get; }
        string TenantId { get; }
        string UserId { get; }
    }

}
