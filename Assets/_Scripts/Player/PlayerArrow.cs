using UnityEngine;
using UnityObjectResolver;

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
            ObjectResolver.Scene
                .Resolve(out ArrowInfo info)
                .Resolve(out Player player);

            _arrowTransform = GetComponent<Transform>();
            _rotate = new RotateComponent(info.rotateDuration, transform, DirectionType.Up);
            _offsetY = info.modelOffsetY;
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