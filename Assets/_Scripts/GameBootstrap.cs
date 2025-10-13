using Unity.VisualScripting;
using UnityEngine;
using UnityObjectResolver;

namespace _Project
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerSo _playerSo;
        [SerializeField] private GridMap _gridMap;
        [SerializeField] private Player _player;
        [SerializeField] private PlayerArrow _playerArrow;
        [SerializeField] private UserInputController _userInputController;

        private void OnValidate()
        {
            _gridMap ??= FindFirstObjectByType<GridMap>();
            _player ??= FindFirstObjectByType<Player>();
            _playerArrow ??= FindFirstObjectByType<PlayerArrow>();
            _userInputController ??= FindFirstObjectByType<UserInputController>();
        }

        private void Awake()
        {
            Regist();
            _userInputController.Active();
        }

        private void Regist()
        {
            ObjectResolver.Scene
                .Register(_playerSo.playerInfo)
                .Register(_playerSo.arrowInfo)
                .Register<IGridMap>(_gridMap)
                .Register(_player)
                .Register(_player.GetComponent<PlayerMover>())
                .Register(_playerArrow)
                .Register(_userInputController)
                .Register<IUserInput>(_userInputController.CreateUserInput());
        }
    }
}