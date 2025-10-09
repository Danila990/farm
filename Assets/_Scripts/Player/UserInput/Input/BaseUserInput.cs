using System;
using UnityEngine;

namespace _Project
{
    public abstract class BaseUserInput : MonoBehaviour, IUserInput
    {
        public event Action<DirectionType> OnDirectionInput;

        protected DirectionType _currentDirection;

        protected abstract void UpdateDirection();

        public void Tick()
        {
            UpdateDirection();
            if (_currentDirection == DirectionType.None) return;

            OnDirectionInput?.Invoke(_currentDirection);
            _currentDirection = DirectionType.None;
        }
    }
}