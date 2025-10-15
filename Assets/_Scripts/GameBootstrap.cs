using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerSo _playerSo;
        [SerializeField] private GridMap _gridMap;
        [SerializeField] private UserInputController _userInputController;


        private void OnValidate()
        {
            _gridMap ??= FindFirstObjectByType<GridMap>();
            _userInputController ??= FindFirstObjectByType<UserInputController>();
        }

        private void Awake()
        {
            MainServiceRegister();
            CreatePlayer();
        }

        private void MainServiceRegister()
        {
            ServiceLocator.Locator
                //GridMap
                .Register<IGridMap>(_gridMap)

                //User input
                .Register(_userInputController)
                .Register<IUserInput>(_userInputController.CreateUserInput())

                //Player stats
                .Register(_playerSo.playerData.stats)
                .Register(_playerSo.arrowData.stats);
        }

        private void CreatePlayer()
        {
            Player player = Instantiate(_playerSo.playerData.prefab);
            PlayerArrow playerArrow = Instantiate(_playerSo.arrowData.prefab);

            ServiceLocator.Locator
                .Register(player)
                .Register(player.GetComponent<PlayerMover>())
                .Register(playerArrow);
        }
    }
}