using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace _Project
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _moveDuration = 1f;
        [SerializeField] private float _offsetPosY;
        [SerializeField] private RotateComponent _rotateComponent;

        private PlayerAnimator _playerAnimator;

        public bool IsMoved { get; private set; } = false;
        public bool IsActived => IsMoved || _rotateComponent.IsRotated;

        private void Start()
        {
            _playerAnimator = GetComponent<PlayerAnimator>();
        }

        public IEnumerator Rotate(DirectionType direction)
        {
            yield return _rotateComponent.Rotate(direction);
        }

        public IEnumerator Move(Vector3 targetPosition)
        {
            if (IsMoved) yield break;

            IsMoved = true;
            _playerAnimator.MoveState(true);
            Vector3 startPosition = transform.position;
            float elapsed = 0f;

            while (elapsed < _moveDuration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / _moveDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            _playerAnimator.MoveState(false);
            transform.position = targetPosition;
            IsMoved = false;
        }
    }
}