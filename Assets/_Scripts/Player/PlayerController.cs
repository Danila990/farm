using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerFactory _factory;

        private Player _player;

        private void Start()
        {
            //_player = _factory.CreatePlayer();
        }
    }

    [Serializable]
    public class PlayerFactory
    {
        [SerializeField] private PlayerSo _data;

        public Player CreatePlayer()
        {
            Player player = Object.Instantiate(_data.playerInfo.prefab);

            return player;
        }
    }
}