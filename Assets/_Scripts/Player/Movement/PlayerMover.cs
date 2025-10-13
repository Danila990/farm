using System.Collections;
using UnityEngine;
using UnityObjectResolver;

namespace _Project
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Transform _rotateModel;

        private RotateComponent _rotate;
        private PlayerMoveComponent _move;
        private PlayerAnimator _animator;

        private DirectionType _currentDirection = DirectionType.None;
        private IGridMap _map;

        public Vector2Int playerIndex { get; private set; }

        public bool IsActived => _move.IsMoved || _rotate.IsRotated;

        private void Start()
        {
            ObjectResolver.Scene
                .Resolve(out PlayerInfo info)
                .Resolve(out _map);

            StarPosition();
            _animator = GetComponent<PlayerAnimator>();
            _move = new PlayerMoveComponent(transform, info.jumpDuration, info.jumpHeigh, _animator);
            _rotate = new RotateComponent(info.rotateDuration, _rotateModel, _currentDirection);
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
                if (platform.CanMove)
                {
                    yield return _move.Move(platform.transform.position);
                    playerIndex = nextIndex;
                }
            }

            yield return Moved(_currentDirection);
        }

        private void StarPosition()
        {
            Platform playerPlatform = _map.FindPlatform(PlatformType.StartPlayer);
            playerIndex = playerPlatform.Index;
            transform.position = playerPlatform.transform.position;
        }
    }
}