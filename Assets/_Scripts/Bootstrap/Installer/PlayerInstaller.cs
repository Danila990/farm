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
            PlayerColor playerColor = playerView.GetComponent<PlayerColor>();

            //SETUP
            mover.SetupMover(gridMap, settings.playerInfo.stats);
            playerArrow.SetupArrow(playerView.transform, settings.arrowInfo.stats);
            playerColor.SetupStartColor(settings.playerInfo.startColor);

            //CONTROLLER
            PlayerController playerController = new GameObject(nameof(PlayerController)).GetOrAddComponent<PlayerController>();
            playerController.SetupController(mover, playerArrow, playerColor);

            //REGISTER
            ServiceLocator
                .Set(playerController)
                .Set(mover)
                .Set(playerArrow)
                .Set(playerColor);
        }
    }
}