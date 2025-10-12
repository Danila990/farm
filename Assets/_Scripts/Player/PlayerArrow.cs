using UnityEngine;
using UnityObjectResolver;

namespace _Project
{
    public class PlayerArrow : MonoBehaviour
    {
        [SerializeField] private Transform _arrow;

        private IUserInput _userInput;
        private RotateComponent _rotate;
        private float _offsetY;
        private Transform _playerTarget;

        public bool IsRotated { get; private set; } = false;

        private void Start()
        {
            ObjectResolver.Scene
                .Resolve(out _userInput)
                .Resolve(out ArrowInfo arrowInfo)
                .Resolve(out Player player);

            _rotate = new RotateComponent(arrowInfo.rotateDuration, transform);
            _offsetY = arrowInfo.modelOffsetY;
            _playerTarget = player.transform;
            _userInput.OnDirectionInput += RotateToDirection;
        }

        private void OnDestroy()
        {
            _userInput.OnDirectionInput -= RotateToDirection;
        }

        private void Update()
        {
            ArrowMover();
        }

        public void RotateToDirection(DirectionType direction)
        {
            if (!IsRotated)
                StartCoroutine(_rotate.Rotate(direction));
        }

        private void ArrowMover()
        {
            Vector3 position = _playerTarget.position;
            position.y = _offsetY;
            transform.position = position;
        }
    }
}