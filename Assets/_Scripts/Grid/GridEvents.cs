using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GridEvents : MonoBehaviour
    {
        public void Event(PlatformType platformType)
        {
            switch (platformType)
            {
                case PlatformType.Finish:
                    break;
                case PlatformType.Empty:
                    ServiceLocator.Get<GameManager>().RestartGame();
                    break;
                case PlatformType.Fruit:
                    break;
                case PlatformType.Rock:
                    ServiceLocator.Get<GameManager>().RestartGame();
                    break;
            }
        }
    }
}