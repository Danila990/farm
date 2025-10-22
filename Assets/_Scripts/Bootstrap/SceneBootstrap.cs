using UnityEngine;

namespace _Project
{
    public class SceneBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            var installers = GetComponents<IInstaller>();
            foreach (var installer in installers)
                installer.Install();
        }
    }
}