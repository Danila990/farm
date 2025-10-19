using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GridControoler : MonoBehaviour, IController
    {
        [SerializeField] private GridMap _gridMap;

        private void OnValidate()
        {
            _gridMap ??= FindFirstObjectByType<GridMap>();
        }

        public void Configure()
        {
            GridEvents gridEvents = _gridMap.GetComponent<GridEvents>();

            ServiceLocator.Locator
                .Register(this)
                .Register<IGridMap>(_gridMap)
                .Register(gridEvents);
        }
    }
}