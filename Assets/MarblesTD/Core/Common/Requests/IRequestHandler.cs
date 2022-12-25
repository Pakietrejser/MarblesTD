using Cysharp.Threading.Tasks;

namespace MarblesTD.Core.Common.Requests
{
    public interface IRequestHandler<TResponse>
    {
        UniTask<TResponse> ImplicitExecute(IRequest<TResponse> request);
    }
}