using UnityEngine;

namespace UnityObjectResolver
{
    [DisallowMultipleComponent] 
    [RequireComponent(typeof(ObjectResolver))]
    public abstract class Bootstrapper : MonoBehaviour 
    {
        private ObjectResolver _container;

        internal ObjectResolver Container => _container.OrNull() ?? (_container = GetComponent<ObjectResolver>());
        
        private bool _hasBeenBootstrapped;

        private void Awake() => BootstrapOnDemand();
        
        public void BootstrapOnDemand() 
        {
            if (_hasBeenBootstrapped) return;
            _hasBeenBootstrapped = true;
            Bootstrap();
        }
        
        protected abstract void Bootstrap();
    }
}
