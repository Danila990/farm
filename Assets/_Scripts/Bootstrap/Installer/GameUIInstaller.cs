using Unity.VisualScripting;
using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GameUIInstaller : MonoBehaviour, IInstaller
    {
        public void Install()
        {
            //GET
            Canvas canvas = FindFirstObjectByType<Canvas>();
            GameUISettings settings = ServiceLocator.Get<GameUISettings>();

            //CREATE
            ColorPanel colorPanel = Instantiate(settings.colorPanel, canvas.transform);

            //SETUP
            colorPanel.SetupPanel();

            //CONTROLLER
            GameUIController gameUIController = new GameObject(nameof(GameUIController)).GetOrAddComponent<GameUIController>();
            gameUIController.SetupController(colorPanel);
            ServiceLocator.Set(gameUIController);
        }
    }
}