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
            GridEvents gridEvents = gridMap.GetComponent<GridEvents>();

            //REGISTER
            ServiceLocator
                .Set<IGridMap>(gridMap)
                .Set(gridEvents);
        }
    }
}