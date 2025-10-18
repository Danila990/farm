using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private Canvas _mainCanvas;
        [SerializeField] private GameUISettings _settings;

        private PlayerColorPanel _colorPanel;

        private void OnValidate()
        {
            _mainCanvas ??= FindFirstObjectByType<Canvas>();
        }

        public void Configure()
        {
            CreateUI();
            SetupUI();
            Register();
        }

        private void Register()
        {
            ServiceLocator.Locator
                            .Register(this)
                            .Register(_colorPanel);
        }

        private void SetupUI()
        {
            _colorPanel.SetupPanel();
        }

        private void CreateUI()
        {
            _colorPanel = Instantiate(_settings.colorPanel, _mainCanvas.transform);
        }
    }
}