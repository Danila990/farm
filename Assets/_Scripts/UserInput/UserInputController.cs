using Unity.VisualScripting;
using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class UserInputController : MonoBehaviour
    {
        [SerializeField] private UserInputType _userType;

        private BaseUserInput _userInput;
        private bool _isActive = false;

        public IUserInput UserInput => _userInput;

        private void Update()
        {
            if(!_isActive || _userInput == null) return;

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

        public void Configure()
        {
            CreateUserInput();
            _userInput.AddComponent<PlayerUserInputHandler>();

            ServiceLocator.Locator
                .Register(this)
                .Register(UserInput);
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