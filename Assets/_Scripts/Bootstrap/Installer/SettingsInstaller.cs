using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class SettingsInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private GameUISettings _gameUISettings;

        public void Install()
        {
            //REGISTER
            ServiceLocator
                .Set(_playerSettings)
                .Set(_gameUISettings);
        }
    }
}