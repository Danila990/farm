using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class PlatformEvents : MonoBehaviour, IPlatformEvent
    {
        public void Event(Platform platform)
        {
            switch (platform.platformType)
            {
                case PlatformType.Finish:
                    break;
                case PlatformType.Empty:
                    ServiceLocator.Get<GameManager>().RestartGame();
                    break;
                case PlatformType.Coin:
                    ServiceLocator.Get<CoinCounter>().AddCoint();
                    break;
                case PlatformType.Rock:
                    ServiceLocator.Get<GameManager>().RestartGame();
                    break;
            }
        }
    }
}