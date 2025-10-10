using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerFactory _factory;

        private void Start()
        {
            Player player = _factory.CreatePlayer();

        }
    }

    [Serializable]
    public class PlayerFactory
    {
        [SerializeField] private Player _prefab;
        [SerializeField] private PlayerStats _stats;

        public Player CreatePlayer()
        {
            Player player = Object.Instantiate(_prefab);

            return player;
        }
    }
}