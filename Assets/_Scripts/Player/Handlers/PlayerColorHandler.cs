using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class PlayerColorHandler : MonoBehaviour
    {
        private PlayerColorChanger _colorChanger;
        private ColorPanel _colorPane;

        private void Start()
        {
            ServiceLocator
                .Get(out _colorChanger)
                .Get(out _colorPane);

            _colorPane.OnColorChanged += OnChangeColor;
        }

        private void OnDestroy()
        {
            _colorPane.OnColorChanged -= OnChangeColor;
        }

        private void OnChangeColor(ColorType colorType)
        {
            _colorChanger.ChangeeColor(colorType);
        }
    }
}