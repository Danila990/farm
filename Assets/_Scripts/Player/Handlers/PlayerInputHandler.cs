using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerMover _playerMover;
        private PlayerArrow _playerArrow;
        private IUserInput _userInput;

        private void Start()
        {
            ServiceLocator
                .Get(out _userInput)
                .Get(out _playerMover)
                .Get(out _playerArrow);

            _userInput.OnDirectionInput += OnUserInput;
        }

        private void OnDestroy()
        {
            _userInput.OnDirectionInput -= OnUserInput;
        }

        private void OnUserInput(DirectionType direction)
        {
            _playerMover.SetInputDirection(direction);
            _playerArrow.SetInputDirection(direction);
        }
    }
}