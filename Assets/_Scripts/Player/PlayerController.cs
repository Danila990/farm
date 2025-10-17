using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerSettings _settings;

        private PlayerModel _playerModel;
        private PlayerFollowArrow _arrow;
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

            _mover.SetupMover(gridMap, _settings.playerInfo.stats);
            _arrow.SetupArrow(playerTranform, _settings.arrowInfo.stats);
        }

        private void CreatePlayer()
        {
            _playerModel = Instantiate(_settings.playerInfo.prefab);
            _arrow = Instantiate(_settings.arrowInfo.prefab);
            _mover = _playerModel.GetComponent<PlayerMover>();
        }
    }
}