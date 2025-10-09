using UnityEngine;

namespace UnityObjectResolver
{
    [AddComponentMenu("ObjectResolver/ObjectResolver Global")]
    public class ObjectResolverGlobal : Bootstrapper 
    {
        [SerializeField] private bool _dontDestroyOnLoad = true;
        
        protected override void Bootstrap() 
        {
            Container.ConfigureAsGlobal(_dontDestroyOnLoad);
        }
    }
}