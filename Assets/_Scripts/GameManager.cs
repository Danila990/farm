using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GameManager : MonoBehaviour, IController
    {
        private UserInputController _userInputController;
        private GridControoler _gridController;
        private PlayerController _playerController;
        private GameUIController _uiController;

        private void Start()
        {
            StartGame();
        }

        public void Configure()
        {
            ServiceLocator.Locator
                .Register(this)
                .Get(out _userInputController)
                .Get(out _playerController)
                .Get(out _gridController)
                .Get(out _uiController);
        }

        public void StartGame()
        {
            _playerController.StartPlayer();
            _userInputController.Active();
        }

        public void RestartGame()
        {
            _playerController.ResetPlayer();
            _uiController.ResetUI();
        }
    }
}