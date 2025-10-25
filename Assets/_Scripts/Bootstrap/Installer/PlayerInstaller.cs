using Unity.VisualScripting;
using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class PlayerInstaller : MonoBehaviour, IInstaller
    {
        public void Install()
        {
            //GET
            PlayerSettings settings = ServiceLocator.Get<PlayerSettings>();
            IGridMap gridMap = ServiceLocator.Get<IGridMap>();

            //CREATE
            PlayerView playerView = Instantiate(settings.playerInfo.prefab);
            PlayerArrow playerArrow = Instantiate(settings.arrowInfo.prefab);
            PlayerMover mover = playerView.GetComponent<PlayerMover>();
            PlayerColorChanger colorChanger = playerView.GetComponent<PlayerColorChanger>();
            PlayerColorHandler colorHandler = playerView.AddComponent<PlayerColorHandler>();
            PlayerInputHandler inputHandler = playerView.AddComponent<PlayerInputHandler>();

            //SETUP
            mover.SetupMover(gridMap, settings.playerInfo.stats);
            playerArrow.SetupArrow(playerView.transform, settings.arrowInfo.stats);
            colorChanger.SetupChanger(settings.playerInfo.startColor);

            //CONTROLLER
            PlayerController playerController = new GameObject(nameof(PlayerController)).AddComponent<PlayerController>();
            playerController.SetupController(mover, playerArrow, colorChanger);

            //REGISTER
            ServiceLocator
                .Set(playerController)
                .Set(mover)
                .Set(playerArrow)
                .Set(colorChanger);
        }
    }
}