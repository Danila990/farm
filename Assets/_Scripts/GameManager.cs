using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GameManager : MonoBehaviour, IInstaller
    {
        private UserInputController _userInputController;
        private PlayerController _playerController;
        private GridController _gridController;
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
                .Get(out _gridController)
                .Get(out _uiController);
        }

        public void StartGame()
        {
            RestartGame();
            _userInputController.Active();
        }

        public void RestartGame()
        {
            _gridController.ResetGrid();
            _playerController.ResetPlayer();
            _uiController.ResetUI();
        }

        public void FinishGame()
        {
            _userInputController.Deactive();
            _playerController.StopPlayer();
        }
    }
}