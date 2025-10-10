using System.Collections;
using UnityEngine;
using UnityObjectResolver;

namespace _Project
{
    public class PlayerArrow : MonoBehaviour
    {
        [SerializeField] private GameObject _arrow;
        [SerializeField] private float _rotateDuration = 0.2f;

        [SerializeField] private RotateComponent _rotate;

        private IUserInput _userInput;

        public bool IsRotated { get; private set; } = false;

        private void Start()
        {
            _userInput = ObjectResolver.Scene.Resolve<IUserInput>();
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