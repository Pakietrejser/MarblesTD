using System;
using Cysharp.Threading.Tasks;
using MarblesTD.Core.Common.Requests;
using UnityEngine;

namespace MarblesTD.UnityCore.Common.RequestHandlers
{
    public abstract class MonoRequestHandler<TRequest, TResponse> : MonoBehaviour, IRequestHandler<TResponse> where TRequest : IRequest<TResponse>
    {
        public UniTask<TResponse> ImplicitExecute(IRequest<TResponse> request)
        {
            if (!(request is TRequest actualRequest)) throw new Exception($"Couldn't cast request to IRequest<{typeof(TResponse)}>");
            return Execute(actualRequest);
        }
        
        protected abstract UniTask<TResponse> Execute(TRequest request);
    }
}