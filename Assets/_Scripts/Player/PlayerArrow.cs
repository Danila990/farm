using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class PlayerArrow : MonoBehaviour
    {
        private RotateComponent _rotate;
        private float _offsetY;
        private Transform _playerTarget;

        private void Start()
        {
            Configure();
        }

        private void Configure()
        {
            ServiceLocator.Locator
                            .Get(out ArrowStats stats)
                            .Get(out Player player);

            _rotate = new RotateComponent(stats.rotateDuration, transform, DirectionType.Up);
            _offsetY = stats.modelOffsetY;
            _playerTarget = player.transform;
        }

        private void Update()
        {
            MovedToPlayer();
        }

        public void SetInputDirection(DirectionType direction)
        {
            if (!_rotate.IsRotated)
                StartCoroutine(_rotate.Rotate(direction));
        }

        private void MovedToPlayer()
        {
            Vector3 position = _playerTarget.position;
            position.y = _offsetY;
            transform.position = position;
        }
    }
}