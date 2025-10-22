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
            ServiceLocator
                .Get(out GameUISettings gameUISettings)
                .Get(out PlayerSettings playerSettings);

            //CREATE
            ColorPanel colorPanel = Instantiate(gameUISettings.colorPanel, canvas.transform);

            //SETUP
            colorPanel.SetupPanel(playerSettings.playerInfo.startColor);

            //CONTROLLER
            GameUIController gameUIController = new GameObject(nameof(GameUIController)).GetOrAddComponent<GameUIController>();
            gameUIController.SetupController(colorPanel);

            //REGISTER
            ServiceLocator
                .Set(gameUIController)
                .Set(colorPanel);
        }
    }
}