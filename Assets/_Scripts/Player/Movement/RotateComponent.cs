using System.Collections;
using UnityEngine;

namespace _Project
{
    public class RotateComponent
    {
        private readonly float _rotateDuration = 0.2f;
        private readonly Transform _rotate;

        public DirectionType currentDirection { get; private set; }
        public bool IsRotated { get; private set; } = false;

        public RotateComponent(float rotateDuration, Transform rotate, DirectionType startDirection = DirectionType.Up)
        {
            _rotateDuration = rotateDuration;
            _rotate = rotate;
            currentDirection = startDirection;
        }

        public virtual IEnumerator Rotate(DirectionType direction)
        {
            if (IsRotated || direction == DirectionType.None || currentDirection == direction) yield break;

            currentDirection = direction;
            IsRotated = true;

            Quaternion startRotation = _rotate.rotation;
            Quaternion targetRotation = direction.ToQuaternionY();

            float elapsed = 0f;

            while (elapsed < _rotateDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _rotateDuration);
                _rotate.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
                yield return null;
            }

            _rotate.rotation = targetRotation;
            IsRotated = false;
        }
    }
}