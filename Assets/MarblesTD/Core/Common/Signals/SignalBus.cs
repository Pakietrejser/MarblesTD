using System;
using System.Collections.Generic;

namespace MarblesTD.Core.Common.Signals
{
    public class SignalBus
    {
        // TODO: remove singleton
        static SignalBus Instance;
        public SignalBus() => Instance = this;
        public static void FireStatic<TSignal>(TSignal signal) where TSignal : ISignal => Instance?.Fire(signal);
        
        public bool DebugMode { get; set; } = false;
        public event Action<Type, int> DebugCallbacksInvoked;
        
        readonly Dictionary<BindingId, List<Callback>> _callbackMap = new Dictionary<BindingId, List<Callback>>();

        public void Fire<TSignal>(TSignal signal) where TSignal : ISignal => FireId(null, signal);
        public void FireId<TSignal>(object identifier, TSignal signal) where TSignal : ISignal => FireInternal(typeof(TSignal), signal, identifier);
        
        public void Subscribe<TSignal>(Action callback) where TSignal : ISignal => SubscribeId<TSignal>(null, callback);
        public void SubscribeId<TSignal>(object identifier, Action callback) where TSignal : ISignal
        {
            void WrappedCallback(object args) => callback();
            SubscribeInternal(typeof(TSignal), identifier, callback, WrappedCallback);
        }
        
        public void Subscribe<TSignal>(Action<TSignal> callback) where TSignal : ISignal => SubscribeId(null, callback);
        public void SubscribeId<TSignal>(object identifier, Action<TSignal> callback) where TSignal : ISignal
        {
            void WrappedCallback(object args) => callback((TSignal) args);
            SubscribeInternal(typeof(TSignal), identifier, callback, WrappedCallback);
        }
        
        public void Unsubscribe<TSignal>(Action callback) where TSignal : ISignal => UnsubscribeId<TSignal>(null, callback);
        public void UnsubscribeId<TSignal>(object identifier, Action callback) where TSignal : ISignal
        {
            void WrappedCallback(object args) => callback();
            UnsubscribeInternal(typeof(TSignal), identifier, callback, WrappedCallback);
        }
        
        public void Unsubscribe<TSignal>(Action<TSignal> callback) where TSignal : ISignal => UnsubscribeId(null, callback);
        public void UnsubscribeId<TSignal>(object identifier, Action<TSignal> callback) where TSignal : ISignal
        {
            void WrappedCallback(object args) => callback((TSignal) args);
            UnsubscribeInternal(typeof(TSignal), identifier, callback, WrappedCallback);
        }

        void FireInternal(Type signalType, object signal, object identifier)
        {
            var bindingId = new BindingId(signalType, identifier);
            
            if (!_callbackMap.TryGetValue(bindingId, out var callbacks)) return;
            if (DebugMode) DebugCallbacksInvoked?.Invoke(signalType, callbacks.Count);

            for (int i = callbacks.Count - 1; i >= 0; i--)
            {
                callbacks[i].Invoke(signal);
            }
        }

        void SubscribeInternal(Type signalType, object identifier, object token, Action<object> callback)
        {
            var bindingId = new BindingId(signalType, identifier);
            var newCallback = new Callback(token, callback);

            if (_callbackMap.TryGetValue(bindingId, out var callbacks))
            {
                if (callbacks.Contains(newCallback)) return;
                
                callbacks.Add(new Callback(token, callback));
            }
            else
            {
                _callbackMap.Add(bindingId, new List<Callback>{new Callback(token, callback)});
            }
        }

        void UnsubscribeInternal(Type signalType, object identifier, object token, Action<object> callback)
        {
            var bindingId = new BindingId(signalType, identifier);
            var newCallback = new Callback(token, callback);

            if (!_callbackMap.TryGetValue(bindingId, out var callbacks)) return;
            
            for (int i = callbacks.Count - 1; i >= 0; i--)
            {
                if (newCallback != callbacks[i]) continue;
                callbacks.RemoveAt(i);
            }
            
            if (callbacks.Count == 0) 
                _callbackMap.Remove(bindingId);
        }

        readonly struct BindingId : IEquatable<BindingId>
        {
            readonly Type _type;
            readonly object _identifier;

            public BindingId(Type type, object identifier)
            {
                _type = type;
                _identifier = identifier;
            }

            public override bool Equals(object other)
            {
                if (other is BindingId otherId)
                    return otherId == this;
                return false;
            }

            public override int GetHashCode()
            {
                unchecked // Overflow is fine, just wrap
                {
                    var hash = 17;
                    hash = hash * 29 + _type.GetHashCode();
                    hash = hash * 29 + (_identifier == null ? 0 : _identifier.GetHashCode());
                    return hash;
                }
            }
            
            public bool Equals(BindingId that) => this == that;
            public static bool operator ==(BindingId left, BindingId right) => left._type == right._type && Equals(left._identifier, right._identifier);
            public static bool operator !=(BindingId left, BindingId right) => !left.Equals(right);
        }

        readonly struct Callback : IEquatable<Callback>
        {
            readonly object _token;
            readonly Action<object> _action;

            public Callback(object token, Action<object> action)
            {
                _token = token;
                _action = action;
            }

            public void Invoke(object signal)
            {
                _action?.Invoke(signal);
            }

            public override int GetHashCode() => _token.GetHashCode();
            public bool Equals(Callback other) => Equals(_token, other._token);
            public override bool Equals(object obj) => obj is Callback other && Equals(other);
            public static bool operator ==(Callback left, Callback right) => left.Equals(right);
            public static bool operator !=(Callback left, Callback right) => !left.Equals(right);
        }
    }
}