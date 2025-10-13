using UnityEngine;
using UnityObjectResolver;

namespace _Project
{
    public class PlayerUserInputHandler : MonoBehaviour
    {
        private PlayerMover _playerMover;
        private PlayerArrow _playerArrow;
        private IUserInput _userInput;

        private void Start()
        {
            ObjectResolver.Scene
                .Resolve(out _userInput)
                .Resolve(out _playerMover)
                .Resolve(out _playerArrow);

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