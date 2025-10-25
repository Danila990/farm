using System;
using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class PlatformEvents : MonoBehaviour
    {
        private GameManager _gameManager;
        private PlayerColorChanger _colorChanger;
        private CoinCounter _coinCounter;

        private void Start()
        {
            ServiceLocator
                .Get(out _gameManager)
                .Get(out _colorChanger)
                .Get(out _coinCounter);
        }

        public void Event(Platform platform)
        {
            switch (platform.platformType)
            {
                case PlatformType.Finish:
                    break;

                case PlatformType.Empty:
                    _gameManager.RestartGame();
                    break;

                case PlatformType.Coin:
                    Coin(platform as CoinPlatform);
                    break;

                case PlatformType.Rock:
                    _gameManager.RestartGame();
                    break;
            }
        }

        private void Coin(CoinPlatform platform)
        {
            if (_colorChanger.currentColor != platform.color)
                _gameManager.RestartGame();

            _coinCounter.AddCoint();
        }
    }
}