using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityObjectResolver
{
    public class ObjectContainer
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public IEnumerable<object> RegisteredServices => _services.Values;

        public bool TryResolve<T>(out T service) where T : class
        {
            Type type = typeof(T);
            if (_services.TryGetValue(type, out object obj))
            {
                service = obj as T;
                return true;
            }

            service = null;
            return false;
        }

        public bool TryResolve<T>(Type type, out T service) where T : class
        {
            if (_services.TryGetValue(type, out object obj))
            {
                service = obj as T;
                return true;
            }

            service = null;
            return false;
        }

        public T Resolve<T>() where T : class
        {
            Type type = typeof(T);
            if (_services.TryGetValue(type, out object obj))
                return obj as T;

            throw new ArgumentException($"ServiceManager.Resolve: Service of type {type.FullName} not registered");
        }

        public ObjectContainer Register<T>(T service)
        {
            Type type = typeof(T);
            if (!_services.TryAdd(type, service))
                Debug.LogError($"ServiceManager.Register: Service of type {type.FullName} already registered");

            return this;
        }

        public ObjectContainer Register(Type type, object service)
        {
            if (!type.IsInstanceOfType(service))
                throw new ArgumentException("platformType of service does not match type of service interface", nameof(service));

            if (!_services.TryAdd(type, service))
                Debug.LogError($"ServiceManager.Register: Service of type {type.FullName} already registered");

            return this;
        }
    }
}