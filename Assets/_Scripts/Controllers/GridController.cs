using UnityEngine;

namespace _Project
{
    public class GridController : MonoBehaviour
    {
        private CoinCounter _coinCounter;
        private IGridMap _gridMap;

        public void SetupController(CoinCounter coinCounter, IGridMap gridMap)
        {
            _coinCounter = coinCounter;
            _gridMap = gridMap;
        }

        public void ResetGrid()
        {
            _coinCounter.ResetCounter();
            var coins = _gridMap.FindPlatforms<CoinPlatform>(PlatformType.Coin);
            foreach (CoinPlatform coinPlatform in coins)
                coinPlatform.ResetPlatform();
        }
    }
}