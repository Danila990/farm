using System.Collections;
using UnityEngine;

namespace _Project
{
    public class MoveComponent
    {
        private readonly Transform _target;
        private readonly float _jumpDuration;
        private readonly float _jumpHeight;

        public bool IsMoved { get; private set; } = false;

        public MoveComponent(Transform target, float jumpDuration, float jumpHeight)
        {
            _target = target;
            _jumpDuration = jumpDuration;
            _jumpHeight = jumpHeight;
        }

        public virtual IEnumerator Move(Vector3 targetPosition)
        {
            if (IsMoved) yield break;

            IsMoved = true;
            Vector3 start = _target.position;
            float time = 0f;
            while (time < _jumpDuration)
            {
                time += Time.deltaTime;
                float t = time / _jumpDuration;

                Vector3 horizontalPos = Vector3.Lerp(start, targetPosition, t);
                float height = _jumpHeight * 4 * t * (1 - t);
                _target.position = new Vector3(horizontalPos.x, start.y + height, horizontalPos.z);

                yield return null;
            }

            _target.position = targetPosition;
            IsMoved = false;
        }
    }
}