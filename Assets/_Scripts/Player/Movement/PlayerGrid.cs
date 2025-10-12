using System.Collections;
using UnityEngine;
using UnityObjectResolver;

namespace _Project
{
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerGrid : MonoBehaviour
    {
        private PlayerMover _playerMover;
        private IGridMap _map;
        private DirectionType _currentDirection;
        private IUserInput _userInput;

        public Vector2Int PlayerIndex { get; private set; }

        private void Start()
        {
            _playerMover = GetComponent<PlayerMover>();
            ObjectResolver.Scene
                .Resolve(out _map)
                .Resolve(out _userInput);

            Platform playerPlatform = _map.FindPlatform(PlatformType.StartPlayer);
            PlayerIndex = playerPlatform.Index;
            transform.position = playerPlatform.transform.position;

            _userInput.OnDirectionInput += DirectionIput;
        }

        private void OnDestroy()
        {
            _userInput.OnDirectionInput -= DirectionIput;
        }

        private void DirectionIput(DirectionType direction)
        {
            _currentDirection = direction;
            if(!_playerMover.IsActived)
                StartCoroutine(Moved(direction));
        }

        private IEnumerator Moved(DirectionType direction)
        {
            if(direction == DirectionType.None || _playerMover.IsActived) yield break;

            yield return _playerMover.Rotate(direction);

            Vector2Int nextIndex = PlayerIndex + direction.ToVector();
            if (_map.CheckPlatform(nextIndex))
            {
                Platform platform = _map.GetPlatform(nextIndex);
                if (platform.CanMove)
                {
                    yield return _playerMover.Move(platform.transform.position);
                    PlayerIndex = nextIndex;
                }
            }

            yield return Moved(_currentDirection);
        }
    }
}