using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private Canvas _mainCanvas;

        private GameUISettings _settings;
        private ColorPanel _colorPanel;

        private void OnValidate()
        {
            _mainCanvas ??= FindFirstObjectByType<Canvas>();
        }

        public void Configure()
        {
            _settings = ServiceLocator.Locator.Get<GameUISettings>();
            CreateUI();
            SetupUI();
            Register();
        }

        public void ResetUI()
        {
            _colorPanel.RestartPanel();
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