using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerMover _playerMover;
        private PlayerArrow _playerArrow;
        private IInputService _inputService;

        private void Start()
        {
            ServiceLocator
                .Get(out _inputService)
                .Get(out _playerMover)
                .Get(out _playerArrow);

            _inputService.DirectionChanged += OnDirectionChanged;
        }

        private void OnDestroy()
        {
            _inputService.DirectionChanged -= OnDirectionChanged;
        }

        private void OnDirectionChanged(DirectionType direction)
        {
            _playerMover.SetInputDirection(direction);
            _playerArrow.SetInputDirection(direction);
        }
    }
}