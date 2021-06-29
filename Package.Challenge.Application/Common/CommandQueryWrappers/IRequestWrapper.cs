using MediatR;

namespace Package.Challenge.Application.Common.CommandQueryWrappers
{
    public interface IRequestWrapper<T> : IRequest<T>
    {
    }
}