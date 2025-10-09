using UnityEngine;

namespace UnityObjectResolver
{
    [AddComponentMenu("ObjectResolver/ObjectResolver Scene")]
    public class ObjectResolverScene : Bootstrapper 
    {
        protected override void Bootstrap() 
        {
            Container.ConfigureForScene();            
        }
    }
}