using System;
using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private PlayerFollowArrow _arrow;

        private PlayerMover _mover;

        public void Configure()
        {
            CreatePlayer();
            SetupPlayer();
            Register();
        }

        public void StartPlayer()
        {
            _mover.SetStartPosition();
        }

        public void ResetPlayer()
        {
            _mover.SetStartPosition();
            _arrow.ResetArrow();
        }

        private void Register()
        {
            ServiceLocator.Locator
                            .Register(this)
                            .Register(_playerModel)
                            .Register(_mover)
                            .Register(_arrow);
        }

        private void SetupPlayer()
        {
            IGridMap gridMap = ServiceLocator.Locator.Get<IGridMap>();
            Transform playerTranform =_playerModel.GetComponent<Transform>();

            _mover.SetupMover(gridMap);
            _arrow.SetupArrow(playerTranform);
        }

        private void CreatePlayer()
        {
            _playerModel = Instantiate(_playerModel);
            _arrow = Instantiate(_arrow);
            _mover = _playerModel.GetComponent<PlayerMover>();
        }
    }
}