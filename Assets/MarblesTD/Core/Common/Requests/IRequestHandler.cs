using System.Threading.Tasks;

namespace MarblesTD.Core.Common.Requests
{
    public interface IRequestHandler<TResponse>
    {
        Task<TResponse> ImplicitExecute(IRequest<TResponse> request);
    }
}