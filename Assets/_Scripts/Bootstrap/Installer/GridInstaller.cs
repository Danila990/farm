using Unity.VisualScripting;
using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GridInstaller : MonoBehaviour, IInstaller
    {
        public void Install()
        {
            //GET
            GridMap gridMap = FindFirstObjectByType<GridMap>();
            PlatformEvents platformEvents = gridMap.AddComponent<PlatformEvents>();
            CoinCounter coinCounter = gridMap.AddComponent<CoinCounter>();

            //SETUP
            coinCounter.SetupCounter(gridMap);

            //CONTROLLER
            GridController gridController = new GameObject(nameof(GridController)).AddComponent<GridController>();
            gridController.SetupController(coinCounter, gridMap);

            //REGISTER
            ServiceLocator
                .Set(gridController)
                .Set<IGridMap>(gridMap)
                .Set<IPlatformEvent>(platformEvents)
                .Set(coinCounter);
        }
    }
}