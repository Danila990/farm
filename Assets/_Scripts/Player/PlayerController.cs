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
        private PlayerColor _playerColor;

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
            _playerColor.ResetColor();
        }

        private void Register()
        {
            ServiceLocator.Locator
                            .Register(this)
                            .Register(_playerModel)
                            .Register(_mover)
                            .Register(_arrow)
                            .Register(_playerColor);
        }

        private void SetupPlayer()
        {
            IGridMap gridMap = ServiceLocator.Locator.Get<IGridMap>();
            Transform playerTranform =_playerModel.GetComponent<Transform>();

            _mover.SetupMover(gridMap, _settings.playerInfo.stats);
            _arrow.SetupArrow(playerTranform, _settings.arrowInfo.stats);
            _playerColor.SetupStartColor(_settings.playerInfo.startColor);
        }

        private void CreatePlayer()
        {
            _playerModel = Instantiate(_settings.playerInfo.prefab);
            _arrow = Instantiate(_settings.arrowInfo.prefab);
            _mover = _playerModel.GetComponent<PlayerMover>();
            _playerColor = _playerModel.GetComponent<PlayerColor>();
        }
    }
}