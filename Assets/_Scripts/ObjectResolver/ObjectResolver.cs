using System;
using UnityEditor;
using UnityEngine;

namespace UnityObjectResolver
{
    public class ObjectResolver : MonoBehaviour
    {
        private const string k_globalServiceLocatorName = "ObjectResolver [Global]";
        private const string k_sceneServiceLocatorName = "ServiceLocator [Scene]";

        private static ObjectResolver _global;
        private static ObjectResolver _scene;

        private ObjectContainer _container = new ObjectContainer();

        internal void ConfigureAsGlobal(bool dontDestroyOnLoad)
        {
            if (_global == this)
                Debug.LogWarning("ObjectResolver.ConfigureAsGlobal: Already configured as _global", this);
            else if (_global != null)
                Debug.LogError("ObjectResolver.ConfigureAsGlobal: Another ObjectResolver is already configured as _global", this);
            else
            {
                _global = this;
                if (dontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);
            }
        }

        internal void ConfigureForScene()
        {
            if (_scene == this)
                Debug.LogWarning("ObjectResolver.ConfigureAsGlobal: Already configured as _scene", this);
            else if (_scene != null)
                Debug.LogError("ObjectResolver.ConfigureAsGlobal: Another ObjectResolver is already configured as _scene", this);
            else
                _scene = this;
        }

        public static ObjectResolver Global
        {
            get
            {
                if (_global != null)
                    return _global;

                if (FindFirstObjectByType<ObjectResolverGlobal>() is { } found)
                {
                    found.BootstrapOnDemand();
                    return _global;
                }

                var container = new GameObject(k_globalServiceLocatorName, typeof(ObjectResolver));
                container.AddComponent<ObjectResolverGlobal>().BootstrapOnDemand();
                return _global;
            }
        }

        public static ObjectResolver Scene
        {
            get
            {
                if (_scene != null)
                    return _scene;

                if (FindFirstObjectByType<ObjectResolverScene>() is { } found)
                {
                    found.BootstrapOnDemand();
                    return _scene;
                }

                var container = new GameObject(k_sceneServiceLocatorName, typeof(ObjectResolver));
                container.AddComponent<ObjectResolverScene>().BootstrapOnDemand();
                return _scene;
            }
        }

        public ObjectResolver Register<T>(T service)
        {
            _container.Register(service);
            return this;
        }

        public ObjectResolver Register(Type type, object service)
        {
            _container.Register(type, service);
            return this;
        }

        public ObjectResolver Resolve<T>(out T service) where T : class
        {
            if (TryGetService(out service))
                return this;

            if (TryGetNextInHierarchy(out ObjectResolver container))
            {
                container.Resolve(out service);
                return this;
            }

            throw new ArgumentException($"ObjectResolver.Resolve: Service of type {typeof(T).FullName} not registered");
        }

        public T Resolve<T>() where T : class
        {
            Type type = typeof(T);
            T service = null;

            if (TryGetService(type, out service))
                return service;

            if (TryGetNextInHierarchy(out ObjectResolver container))
                return container.Resolve<T>();

            throw new ArgumentException($"Could not resolve type '{typeof(T).FullName}'.");
        }

        public bool TryResolve<T>(out T service) where T : class
        {
            Type type = typeof(T);
            service = null;
            if (TryGetService(type, out service))
                return true;

            return TryGetNextInHierarchy(out ObjectResolver container) && container.TryResolve(out service);
        }

        private bool TryGetService<T>(out T service) where T : class
        {
            return _container.TryResolve(out service);
        }

        private bool TryGetService<T>(Type type, out T service) where T : class
        {
            return _container.TryResolve(type, out service);
        }

        private bool TryGetNextInHierarchy(out ObjectResolver container)
        {
            if (this == _global)
            {
                container = null;
                return false;
            }

            container = transform.parent.OrNull()?.GetComponentInParent<ObjectResolver>().OrNull() ?? Scene;
            return container != null;
        }

        private void OnDestroy()
        {
            if (this == _global)
                _global = null;

            if(this == _scene)
                _scene = null;
        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            _global = null;
            _scene = null;
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/ObjectResolver/Add Global")]
        private static void AddGlobal()
        {
            var go = new GameObject(k_globalServiceLocatorName, typeof(ObjectResolverGlobal));
        }

        [MenuItem("GameObject/ObjectResolver/Add Scene")]
        private static void AddScene()
        {
            var go = new GameObject(k_sceneServiceLocatorName, typeof(ObjectResolverScene));
        }
#endif
    }
}