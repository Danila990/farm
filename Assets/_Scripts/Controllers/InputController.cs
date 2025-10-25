using UnityEngine;

namespace _Project
{
    public class InputController : MonoBehaviour
    {
        private bool _isActive = false;
        private InputService _inputService;

        public void SetupController(InputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            if (!_isActive) return;

            _inputService.Tick();
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