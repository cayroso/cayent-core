namespace Cayent.Core.CQRS.Services
{
    public interface IContainer
    {
        T Resolve<T>();
    }
}
