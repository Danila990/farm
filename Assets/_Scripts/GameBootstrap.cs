using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private GridControoler _gridControoler;
        [SerializeField] private UserInputController _userInputController;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private GameUIController _uiController;
        [SerializeField] private GameManager _gameManager;

        private void OnValidate()
        {
            _gridControoler ??= FindFirstObjectByType<GridControoler>();
            _userInputController ??= FindFirstObjectByType<UserInputController>();
            _playerController ??= FindFirstObjectByType<PlayerController>();
            _uiController ??= FindFirstObjectByType<GameUIController>();
            _gameManager ??= FindFirstObjectByType<GameManager>();
        }

        private void Awake()
        {
            _gridControoler.Configure();
            _userInputController.Configure();
            _playerController.Configure();
            _uiController.Configure();
            _gameManager.Configure();
        }
    }
}