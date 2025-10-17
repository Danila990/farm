using System;
using UnityEditor;
using UnityEngine;

namespace UnityServiceLocator
{
    public class ServiceLocator : MonoBehaviour
    {
        private const string SERVICE_LOCATION_NAME = "ServiceLocator";

        private static ServiceLocator _serviceLocator;

        public static ServiceLocator Locator
        {
            get
            {
                if (_serviceLocator != null)
                    return _serviceLocator;

                if(FindFirstObjectByType<ServiceLocator>() is { } found)
                {
                    found.Configure();
                    return _serviceLocator;
                }

                var gameobjectLocator = new GameObject(SERVICE_LOCATION_NAME, typeof(ServiceLocator));
                gameobjectLocator.GetComponent<ServiceLocator>().Configure();
                return _serviceLocator;
            }
        }

        private ServiceContainer _container = new ServiceContainer();

        private void Configure()
        {
            if (_serviceLocator == this)
                Debug.LogWarning("ServiceLocator.Configure: Already configured", this);
            else if (_serviceLocator != null)
                Debug.LogError("ServiceLocator.Configure: Another ServiceLocator is already configured", this);
            else
                _serviceLocator = this;
        }

        public ServiceLocator Register<T>(T service) where T : class
        {
            _container.Register(service);
            return this;
        }

        public ServiceLocator Get<T>(out T service) where T : class
        {
            if (_container.TryGet(out service))
                return this;

            throw new ArgumentException($"ServiceLocator.Get: Service of type {typeof(T).FullName} not registered");
        }

        public T Get<T>() where T : class
        {
            return _container.Get<T>();
        }

        private void OnDestroy()
        {
            if (this == _serviceLocator)
                _serviceLocator = null;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            _serviceLocator = null;
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/ServiceLocator/Add Locator")]
        private static void AddGlobal()
        {
            var go = new GameObject(SERVICE_LOCATION_NAME, typeof(ServiceLocator));
        }
#endif
    }
}