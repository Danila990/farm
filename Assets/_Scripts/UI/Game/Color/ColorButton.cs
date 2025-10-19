using UnityEngine;
using UnityEngine.UI;

namespace _Project
{
    public class ColorButton : MonoBehaviour
    {
        [field: SerializeField] public ColorType playerColor { get; private set; }

        private Button _button;
        private ColorPanel _colorPanel;

        public void SetupButton(ColorPanel colorPanel)
        {
            _colorPanel = colorPanel;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ButtonClick);
            ActiveButton();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonClick);
        }

        public void ActiveButton()
        {
            _button.interactable = true;
        }

        public void DeactiveButton()
        {
            _button.interactable = false;
        }

        private void ButtonClick()
        {
            _colorPanel.CollorButtonDown(this);
        }
    }
}