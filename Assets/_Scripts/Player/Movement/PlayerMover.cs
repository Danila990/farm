using System.Collections;
using UnityEngine;
    
namespace _Project
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight = 1;
        [SerializeField] private float _jumpDuration = 0.5f;
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
            Vector3 start = transform.position;
            float time = 0f;
            while (time < _jumpDuration)
            {
                time += Time.deltaTime;
                float t = time / _jumpDuration;

                Vector3 horizontalPos = Vector3.Lerp(start, targetPosition, t);
                float height = _jumpHeight * 4 * t * (1 - t);
                transform.position = new Vector3(horizontalPos.x, start.y + height, horizontalPos.z);

                yield return null;
            }

            _playerAnimator.MoveState(false);
            transform.position = targetPosition;
            IsMoved = false;
        }
    }
}