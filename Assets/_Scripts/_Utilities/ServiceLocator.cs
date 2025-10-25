using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityServiceLocator
{
    public static class ServiceLocator 
    {
        private static IServiceLocator _container;

        private static IServiceLocator _serviceLocator
        {
            get 
            {
                if (_container == null)
                    _container = new ServiceContainer();

                return _container; 
            }
        }

        public static IServiceLocator Get<T>(out T service) where T : class
        {
            return _serviceLocator.Get(out service);
        }

        public static T Get<T>() where T : class
        {
            return _serviceLocator.Get<T>();
        }

        public static IServiceLocator Set<T>(T service) where T : class
        {
            return _serviceLocator.Set(service);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            _container = null;
        }
    }

    public interface IServiceLocator
    {
        public IServiceLocator Get<T>(out T service) where T : class;
        public T Get<T>() where T : class;
        public IServiceLocator Set<T>(T service) where T : class;
    }

    public class ServiceContainer : IServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public IEnumerable<object> RegisteredServices => _services.Values;

        public IServiceLocator Get<T>(out T service) where T : class
        {
            service = Get<T>();
            return this;
        }

        public T Get<T>() where T : class
        {
            Type type = typeof(T);
            if (_services.TryGetValue(type, out object obj))
                return obj as T;

            throw new ArgumentException($"ServiceContainer.Get: Service of type {type.FullName} not registered");
        }

        public IServiceLocator Set<T>(T service) where T : class
        {
            Type type = typeof(T);
            if (!_services.TryAdd(type, service))
                Debug.LogError($"ServiceContainer.Register: Service of type {type.FullName} already registered");

            return this;
        }
    }
}