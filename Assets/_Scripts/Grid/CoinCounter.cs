using System;
using UnityEngine;

namespace _Project
{
    public class CoinCounter : MonoBehaviour
    {
        public event Action<int> OnUpdateCount;

        public int maxCount { get; private set; }
        public int currentCount { get; private set; }

        public void SetupCounter(IGridMap gridMap)
        {
            maxCount = gridMap.FindPlatforms<CoinPlatform>(PlatformType.Coin).Length;
            ResetCounter();
        }

        public void AddCoint()
        {
            currentCount++;
            OnUpdateCount?.Invoke(currentCount);
        }

        public void ResetCounter()
        {
            currentCount = 0;
            OnUpdateCount?.Invoke(currentCount);
        }
    }
}