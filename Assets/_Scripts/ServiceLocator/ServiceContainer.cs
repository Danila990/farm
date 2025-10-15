using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityServiceLocator
{
    public class ServiceContainer
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public IEnumerable<object> RegisteredServices => _services.Values;

        public bool TryGet<T>(out T service) where T : class
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

        public T Get<T>() where T : class
        {
            Type type = typeof(T);
            if (_services.TryGetValue(type, out object obj))
                return obj as T;

            throw new ArgumentException($"ServiceContainer.Get: Service of type {type.FullName} not registered");
        }

        public ServiceContainer Register<T>(T service)
        {
            Type type = typeof(T);
            if (!_services.TryAdd(type, service))
                Debug.LogError($"ServiceContainer.Register: Service of type {type.FullName} already registered");

            return this;
        }
    }
}