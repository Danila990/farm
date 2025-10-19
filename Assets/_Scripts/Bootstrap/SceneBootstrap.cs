using UnityEngine;

namespace _Project
{
    public class SceneBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            var installers = GetComponents<IController>();
            foreach (var installer in installers)
                installer.Configure();
        }
    }
}