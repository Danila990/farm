using UnityEngine;

namespace _Project
{
    public class CoinPlatform  : Platform
    {
        [SerializeField] private GameObject _coin;

        [field:SerializeField] public ColorType color { get; private set; }

        private void Start()
        {
            Color newColor = color switch
            {
                ColorType.While => Color.white,
                ColorType.Red => Color.red,
                ColorType.Green => Color.green,
                ColorType.Blue => Color.blue,

                _ => Color.white,
            };

            _coin.GetComponent<MeshRenderer>().material.color = newColor;
        }

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