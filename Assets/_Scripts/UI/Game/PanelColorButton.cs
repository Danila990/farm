using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

namespace _Project
{
    public class PanelColorButton : MonoBehaviour
    {
        [SerializeField] private ColorType _playerCollor;

        private Button _button;
        private PlayerColorPanel _colorPanel;

        private void Start()
        {
            _colorPanel = ServiceLocator.Locator.Get<PlayerColorPanel>();
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