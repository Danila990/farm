using UnityEngine;
using UnityServiceLocator;

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
            _player = Instantiate(_playerSo.playerData.prefab);
            _playerArrow = Instantiate(_playerSo.arrowData.prefab);
        }

        private void Regist()
        {
            IServiceLocator.Locator
                .Register<IGridMap>(_gridMap)

                .Register(_userInputController)
                .Register<IUserInput>(_userInput)

                .Register(_playerSo.playerData.stats)
                .Register(_playerSo.arrowData.stats)
                .Register(_player)
                .Register(_player.GetComponent<PlayerMover>())
                .Register(_playerArrow);
        }
    }
}