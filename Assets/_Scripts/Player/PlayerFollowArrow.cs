using UnityEngine;

namespace _Project
{
    public class PlayerFollowArrow : MonoBehaviour
    {
        private RotateComponent _rotate;
        private float _offsetY;
        private Transform _target;

        public void SetupArrow(Transform target, ArrowStats stats)
        {
            _target = target;
            _rotate = new RotateComponent(stats.rotateDuration, transform);
            _offsetY = stats.modelOffsetY;
        }

        private void Update()
        {
            MovedToPlayer();
        }

        public void ResetArrow()
        {
            _rotate.ResetRotate();
        }

        public void SetInputDirection(DirectionType direction)
        {
            if (!_rotate.IsRotated)
                StartCoroutine(_rotate.Rotate(direction));
        }

        private void MovedToPlayer()
        {
            Vector3 position = _target.position;
            position.y = _offsetY;
            transform.position = position;
        }
    }
}