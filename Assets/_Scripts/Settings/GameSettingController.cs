using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GameSettingController : MonoBehaviour, IController
    {
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private GameUISettings _gameUISettings;

        public void Configure()
        {
            ServiceLocator.Locator
                .Register(_playerSettings)
                .Register(_gameUISettings);
        }
    }
}