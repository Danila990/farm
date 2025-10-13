using System.Collections;
using UnityEngine;
using UnityObjectResolver;

namespace _Project
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Transform _rotateModel;

        private RotateComponent _rotateComponent;
        private PlayerMove _playerMove;
        private PlayerAnimator _playerAnimator;

        public bool IsActived => _playerMove.IsMoved || _rotateComponent.IsRotated;

        private void Start()
        {
            ObjectResolver.Scene.Resolve(out PlayerInfo playerInfo);

            _playerAnimator = GetComponent<PlayerAnimator>();
            _playerMove = new PlayerMove(transform, playerInfo.jumpDuration, playerInfo.jumpHeigh, _playerAnimator);
            _rotateComponent = new RotateComponent(playerInfo.rotateDuration, _rotateModel);
        }

        public IEnumerator Rotate(DirectionType direction)
        {
            yield return _rotateComponent.Rotate(direction);
        }

        public IEnumerator Move(Vector3 targetPosition)
        {
            yield return _playerMove.Move(targetPosition);
        }
    }
}