using System;
using System.Threading.Tasks;

namespace MarblesTD.Core.Common.Requests
{
    public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TResponse> where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> ImplicitExecute(IRequest<TResponse> request)
        {
            if (!(request is TRequest actualRequest)) throw new Exception($"Cannot cast request to IRequest<{typeof(TResponse)}>");
            return Execute(actualRequest);
        }
        
        protected abstract Task<TResponse> Execute(TRequest request);
    }
}