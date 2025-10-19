using System.Linq;
using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class ColorPanel : MonoBehaviour
    {
        private ColorButton[] _colorButtons;

        private ColorType _startColor;
        private ColorButton _currentButton;
        private PlayerColor _playerColor;

        public void SetupPanel()
        {
            ServiceLocator.Locator
                .Get(out PlayerSettings playerColor)
                .Get(out _playerColor);

            _startColor = playerColor.playerInfo.startColor;
            SetupButtons();
            RestartPanel();
        }

        public void CollorButtonDown(ColorButton colorButton)
        {
            _currentButton.ActiveButton();
            _currentButton = colorButton;
            _currentButton.DeactiveButton();
            _playerColor.ChangeeColor(_currentButton.playerColor);
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