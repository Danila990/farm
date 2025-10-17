using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GameManager : MonoBehaviour
    {
        private UserInputController _userInputController;
        private GridControoler _gridController;
        private PlayerController _playerController;

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
                .Get(out _gridController);

        }

        public void StartGame()
        {
            _playerController.StartPlayer();
            _userInputController.Active();
        }

        public void Restart()
        {
            _playerController.ResetPlayer();
        }
    }
}