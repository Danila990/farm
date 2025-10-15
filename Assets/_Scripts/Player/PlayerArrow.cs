using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class PlayerArrow : MonoBehaviour
    {
        private RotateComponent _rotate;
        private float _offsetY;
        private Transform _playerTarget;
        private Transform _arrowTransform;

        public bool IsRotated { get; private set; } = false;

        private void Start()
        {
            IServiceLocator.Locator
                .Get(out ArrowStats stats)
                .Get(out Player player);

            _arrowTransform = GetComponent<Transform>();
            _rotate = new RotateComponent(stats.rotateDuration, transform, DirectionType.Up);
            _offsetY = stats.modelOffsetY;
            _playerTarget = player.GetComponent<Transform>();
        }

        private void Update()
        {
            ArrowMoved();
        }

        public void SetInputDirection(DirectionType direction)
        {
            if (!IsRotated)
                StartCoroutine(_rotate.Rotate(direction));
        }

        private void ArrowMoved()
        {
            Vector3 position = _playerTarget.position;
            position.y = _offsetY;
            _arrowTransform.position = position;
        }
    }
}