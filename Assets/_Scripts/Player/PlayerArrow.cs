using UnityEngine;
using UnityObjectResolver;

namespace _Project
{
    public class PlayerArrow : MonoBehaviour
    {
        [SerializeField] private Transform _arrow;

        private IUserInput _userInput;
        private RotateComponent _rotate;

        public bool IsRotated { get; private set; } = false;

        private void Start()
        {
            ObjectResolver.Scene
                .Resolve(out _userInput)
                .Resolve(out ArrowInfo arrowInfo);

            _rotate = new RotateComponent(arrowInfo.rotateDuration, _arrow);
            _userInput.OnDirectionInput += RotateToDirection;
        }

        private void OnDestroy()
        {
            _userInput.OnDirectionInput -= RotateToDirection;
        }

        public void RotateToDirection(DirectionType direction)
        {
            if (!IsRotated)
                StartCoroutine(_rotate.Rotate(direction));
        }
    }
}