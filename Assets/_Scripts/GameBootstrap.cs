using UnityEngine;

namespace _Project
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private GameSettingController _gameSettingsController;
        [SerializeField] private GridControoler _gridController;
        [SerializeField] private UserInputController _userInputController;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private GameUIController _uiController;
        [SerializeField] private GameManager _gameManager;

        private void OnValidate()
        {
            _gameSettingsController ??= FindAnyObjectByType<GameSettingController>();
            _gridController ??= FindFirstObjectByType<GridControoler>();
            _userInputController ??= FindFirstObjectByType<UserInputController>();
            _playerController ??= FindFirstObjectByType<PlayerController>();
            _uiController ??= FindFirstObjectByType<GameUIController>();
            _gameManager ??= FindFirstObjectByType<GameManager>();
        }

        private void Awake()
        {
            _gameSettingsController.Configure();
            _gridController.Configure();
            _userInputController.Configure();
            _playerController.Configure();
            _uiController.Configure();
            _gameManager.Configure();
        }
    }
}