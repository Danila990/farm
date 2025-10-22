using UnityEngine;

namespace _Project
{
    public class GameUIController : MonoBehaviour
    {
        private ColorPanel _colorPanel;

        public void SetupController(ColorPanel colorPanel)
        {
            _colorPanel = colorPanel;
        }

        public void ResetUI()
        {
            _colorPanel.RestartPanel();
        }
    }
}