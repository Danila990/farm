using System.Collections;
using UnityEngine;

namespace _Project
{
    public class PlayerMoveComponent : MoveComponent
    {
        private readonly PlayerAnimator _playerAnimator;

        public PlayerMoveComponent(Transform target, float jumpDuration, float jumpHeight, PlayerAnimator playerAnimator)
            : base(target, jumpDuration, jumpHeight)
        {
            _playerAnimator = playerAnimator;
        }

        public override IEnumerator Move(Vector3 targetPosition)
        {
            if (IsMoved) yield break;

            _playerAnimator.MoveState(true);
            yield return base.Move(targetPosition);
            _playerAnimator.MoveState(false);
        }
    }
}