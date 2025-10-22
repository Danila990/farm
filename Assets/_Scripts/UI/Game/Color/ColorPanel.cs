using System;
using System.Linq;
using UnityEngine;

namespace _Project
{
    public class ColorPanel : MonoBehaviour
    {
        public event Action<ColorType> OnColorChanged;

        private ColorButton[] _colorButtons;

        private ColorType _startColor;
        private ColorButton _currentButton;

        public void SetupPanel(ColorType startColor)
        {
            _startColor = startColor;
            SetupButtons();
            RestartPanel();
        }

        public void CollorButtonDown(ColorButton colorButton)
        {
            _currentButton.ActiveButton();
            _currentButton = colorButton;
            _currentButton.DeactiveButton();
            OnColorChanged?.Invoke(_currentButton.playerColor);
        }

        public void RestartPanel()
        {
            foreach (var button in _colorButtons)
            {
                button.ActiveButton();
            }

            _currentButton = _colorButtons.FirstOrDefault(color => color.playerColor == _startColor);
            _currentButton.DeactiveButton();
        }

        private void SetupButtons()
        {
            _colorButtons = GetComponentsInChildren<ColorButton>();
            foreach (var button in _colorButtons)
            {
                button.SetupButton(this);
            }
        }
    }
}