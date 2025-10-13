using Unity.VisualScripting;
using UnityEngine;

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

        public IUserInput CreateUserInput()
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

            _userInput.AddComponent<PlayerUserInputHandler>();

            return _userInput;
        }
    }
}