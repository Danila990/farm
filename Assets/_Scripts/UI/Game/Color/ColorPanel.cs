using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class ColorPanel : MonoBehaviour
    {
        public void SetupPanel()
        {

        }

        public void CollorButtonDown(ColorType color)
        {
            var playerColor = ServiceLocator.Locator.Get<PlayerColor>();
            playerColor.ChangeeColor(color);
        }
    }
}