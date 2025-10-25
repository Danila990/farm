using UnityEngine;

namespace _Project
{
    public class CoinPlatform  : Platform
    {
        [SerializeField] private GameObject _coin;

        public override void Event()
        {
            _coin.SetActive(false);
            base.Event();
        }

        public void ResetPlatform()
        {
            _coin.SetActive(true);
        }
    }
}