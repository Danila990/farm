using System.Collections;
using UnityEngine;

namespace _Project
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private MoverStats _stats;
        [SerializeField] private Transform _rotateModel;

        private RotateComponent _rotate;
        private PlayerMoveComponent _move;
        private PlayerAnimator _animator;

        private DirectionType _currentDirection;
        private IGridMap _map;

        public Vector2Int playerIndex { get; private set; }

        public bool IsActived => _move.IsMoved || _rotate.IsRotated;

        public void SetupMover(IGridMap gridMap)
        {
            _map = gridMap;
            _animator = GetComponent<PlayerAnimator>();
            _move = new PlayerMoveComponent(transform, _stats.jumpDuration, _stats.jumpHeigh, _animator);
            _rotate = new RotateComponent(_stats.rotateDuration, _rotateModel, _currentDirection);
        }

        public void SetStartPosition()
        {
            StopAllCoroutines();
            _animator.StopAnimation();
            _rotate.ResetRotate();

            Platform playerPlatform = _map.FindPlatform(PlatformType.StartPlayer);
            playerIndex = playerPlatform.platformIndex;
            transform.position = playerPlatform.transform.position;
        }

        public void SetInputDirection(DirectionType directionType)
        {
            _currentDirection = directionType;
            if (!IsActived)
                StartCoroutine(Moved(_currentDirection));
        }

        private IEnumerator Moved(DirectionType direction)
        {
            if (direction == DirectionType.None || IsActived) yield break;

            yield return _rotate.Rotate(direction);

            Vector2Int nextIndex = playerIndex + direction.ToVector();
            if (_map.CheckPlatform(nextIndex))
            {
                Platform platform = _map.GetPlatform(nextIndex);
                if (platform.canMove)
                {
                    yield return _move.Move(platform.transform.position);
                    playerIndex = nextIndex;
                    platform.Event();
                }
            }

            yield return Moved(_currentDirection);
        }
    }
}