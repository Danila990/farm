using UnityEngine;

namespace _Project
{
    public class UserInputController : MonoBehaviour
    {
        private bool _isActive = false;
        private IUserInput _userInput;

        public void SetupController(IUserInput userInput)
        {
            _userInput = userInput;
        }

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
    }
}