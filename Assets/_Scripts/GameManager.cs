using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GameManager : MonoBehaviour
    {
        private UserInputController _userInputController;
        private Player _player;

        private void Start()
        {
            ServiceLocator.Locator
                .Get(out  _userInputController)
                .Get(out _player);

            _player.StartPlayer();
            _userInputController.Active();
        }
    }
}