using Unity.VisualScripting;
using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class UserInputInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private UserInputType _userType;

        public void Install()
        {
            //CREATE
            BaseUserInput userInput = CreateUserInput();
            PlayerUserInputHandler inputHandler = userInput.AddComponent<PlayerUserInputHandler>();

            //CONTROLLER
            UserInputController userInputController = new GameObject(nameof(UserInputController)).GetOrAddComponent<UserInputController>();
            userInputController.SetupController(userInput);

            //REGISTER
            ServiceLocator.Set(userInputController)
                .Set<IUserInput>(userInput);
        }

        private BaseUserInput CreateUserInput()
        {
            return _userType switch
            {
                UserInputType.Pc => new GameObject(nameof(PcInput)).AddComponent<PcInput>(),
                UserInputType.Mobile => new GameObject(nameof(MobileInput)).AddComponent<MobileInput>(),
                _ => new GameObject(nameof(PcInput)).AddComponent<PcInput>(),
            };
        }
    }
}