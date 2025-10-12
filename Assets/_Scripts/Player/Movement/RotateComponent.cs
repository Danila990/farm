using System;
using System.Collections;
using UnityEngine;

namespace _Project
{
    [Serializable]
    public class RotateComponent
    {
        private float _rotateDuration = 0.2f;
        private Transform _rotate;

        public DirectionType CurrentDirection { get; private set; } = DirectionType.Up;
        public bool IsRotated { get; private set; } = false;

        public RotateComponent(float rotateDuration, Transform rotate)
        {
            _rotateDuration = rotateDuration;
            _rotate = rotate;
        }

        public IEnumerator Rotate(DirectionType direction)
        {
            if (IsRotated || direction == DirectionType.None || CurrentDirection == direction) yield break;

            CurrentDirection = direction;
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