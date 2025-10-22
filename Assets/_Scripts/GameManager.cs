using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GameManager : MonoBehaviour, IInstaller
    {
        private UserInputController _userInputController;
        private PlayerController _playerController;
        private GameUIController _uiController;

        private void Start()
        {
            StartGame();
        }

        public void Install()
        {
            ServiceLocator
                .Set(this)
                .Get(out _userInputController)
                .Get(out _playerController)
                .Get(out _uiController);
        }

        public void StartGame()
        {
            RestartGame();
            _userInputController.Active();
        }

        public void RestartGame()
        {
            _playerController.ResetPlayer();
            _uiController.ResetUI();
        }
    }
}