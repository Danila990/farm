using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    [RequireComponent(typeof(PlayerMover))]
    public class Player : MonoBehaviour
    {
        private PlayerMover _playerMover;

        private void Awake()
        {
            Configure();
        }

        private void Configure()
        {
            _playerMover = GetComponent<PlayerMover>();
            ServiceLocator.Locator
                .Get(out PlayerStats playerStats)
                .Get(out IGridMap gridMap);

            _playerMover.SetupMover(playerStats, gridMap);
        }

        public void StartPlayer()
        {
            _playerMover.SetStartPosition();
        }
    }
}