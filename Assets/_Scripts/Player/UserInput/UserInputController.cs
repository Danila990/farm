using UnityEngine;
using UnityObjectResolver;

namespace _Project
{
    public class UserInputController : MonoBehaviour
    {
        [SerializeField] private UserInputType _userType;

        private BaseUserInput _userInput;
        private bool _isActive = false;

        private void Awake()
        {
            CreateUserInput();
            Active();

            ObjectResolver.Scene
                .Register(this)
                .Register<IUserInput>(_userInput);
        }

        private void Update()
        {
            if(!_isActive) return;

            _userInput.Tick();
        }

        public void Active()
        {
            _isActive = true;
        }

        public void Deactive()
        {
            _isActive = false;
        }

        private void CreateUserInput()
        {
            switch (_userType)
            {
                case UserInputType.Pc:
                    _userInput = new GameObject(nameof(PcInput)).AddComponent<PcInput>();
                    break;

                case UserInputType.Mobile:
                    _userInput = new GameObject(nameof(MobileInput)).AddComponent<MobileInput>();
                    break;

                default:
                    break;
            }
        }
    }
}