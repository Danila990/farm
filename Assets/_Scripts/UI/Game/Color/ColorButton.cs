using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

namespace _Project
{
    public class ColorButton : MonoBehaviour
    {
        [SerializeField] private ColorType _playerCollor;

        private Button _button;
        private ColorPanel _colorPanel;

        private void Start()
        {
            _colorPanel = ServiceLocator.Locator.Get<ColorPanel>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonClick);
        }

        private void ButtonClick()
        {
            _colorPanel.CollorButtonDown(_playerCollor);
        }
    }
}