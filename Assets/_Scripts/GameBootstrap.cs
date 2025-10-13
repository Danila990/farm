using UnityEngine;
using UnityObjectResolver;

namespace _Project
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerSo _playerSo;
        [SerializeField] private GridMap _gridMap;
        [SerializeField] private UserInputController _userInputController;

        private IUserInput _userInput;
        private PlayerArrow _playerArrow;
        private Player _player;

        private void OnValidate()
        {
            _gridMap ??= FindFirstObjectByType<GridMap>();
            _userInputController ??= FindFirstObjectByType<UserInputController>();
        }

        private void Awake()
        {
            Create();
            Regist();
        }

        private void Start()
        {
            _userInputController.Active();
        }

        private void Create()
        {
            _userInput = _userInputController.CreateUserInput();
            _player = Instantiate(_playerSo.playerPrefab);
            _playerArrow = Instantiate(_playerSo.arrowPrefab);
        }

        private void Regist()
        {
            ObjectResolver.Scene
                .Register<IGridMap>(_gridMap)

                .Register(_userInputController)
                .Register<IUserInput>(_userInput)

                .Register(_playerSo.playerInfo)
                .Register(_playerSo.arrowInfo)
                .Register(_player)
                .Register(_player.GetComponent<PlayerMover>())
                .Register(_playerArrow);
        }
    }
}