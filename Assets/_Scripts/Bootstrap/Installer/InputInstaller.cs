
using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class InputInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private InputType _userType;

        public void Install()
        {
            //CREATE
            InputService inputService = CreateInputService();
            
            //CONTROLLER
            InputController inputController = new GameObject(nameof(InputController)).AddComponent<InputController>();
            inputController.SetupController(inputService);

            //REGISTER
            ServiceLocator.Set(inputController)
                .Set<IInputService>(inputService);
        }

        private InputService CreateInputService()
        {
            return _userType switch
            {
                InputType.Pc => new GameObject(nameof(PCInputService)).AddComponent<PCInputService>(),
                InputType.Mobile => new GameObject(nameof(MobileInputService)).AddComponent<MobileInputService>(),
                _ => new GameObject(nameof(PCInputService)).AddComponent<PCInputService>()
            };
        }
    }
}