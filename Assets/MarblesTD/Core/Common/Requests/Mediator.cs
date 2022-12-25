using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace MarblesTD.Core.Common.Requests
{
    public class Mediator
    {
        readonly Dictionary<Type, object> _handlers = new Dictionary<Type, object>();

        public void AddHandler<TRequest, TResponse>(IRequestHandler<TResponse> requestHandler) where TRequest : IRequest<TResponse>
        {
            if (requestHandler == null) throw new Exception("Can't add a null handler.");
            _handlers.Add(typeof(TRequest), requestHandler);
        }
        
        public async UniTask<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            if (request == null) throw new Exception("Can't execute a null request");
            if (!_handlers.TryGetValue(request.GetType(), out object handler)) throw new Exception($"No handler for a request of type: {request.GetType().Name}");
            if (!(handler is IRequestHandler<TResponse> actualHandler)) throw new Exception($"Cant cast handler to IHandler<{typeof(TResponse)}>");

            return await actualHandler.ImplicitExecute(request);
        }
    }
}