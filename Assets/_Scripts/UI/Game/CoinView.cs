using TMPro;
using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class CoinView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private CoinCounter _coinCounter;

        private void Start()
        {
            _coinCounter = ServiceLocator.Get<CoinCounter>();
            OnUpdateCount(_coinCounter.currentCount);
            _coinCounter.OnUpdateCount += OnUpdateCount;
        }

        private void OnDestroy()
        {
            _coinCounter.OnUpdateCount -= OnUpdateCount;
        }

        private void OnUpdateCount(int count)
        {
            _text.text = $"Coin: {count}/{_coinCounter.maxCount}";
        }
    }
}