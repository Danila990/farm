using System.Collections.Generic;
using UnityEngine;

namespace _Project
{
    public class PlayerColor : MonoBehaviour
    {
        private readonly Dictionary<ColorType, Color> _colors = new Dictionary<ColorType, Color>()
        {
            {ColorType.While, Color.white},
            {ColorType.Red, Color.red},
            {ColorType.Green, Color.green},
            {ColorType.Blue, Color.blue }
        };

        [SerializeField] private MeshRenderer _meshrenderer;

        private ColorType _startColor;
        
        public ColorType currentColor {  get; private set; }

        public void SetupStartColor(ColorType color)
        {
            _startColor = color;
            ChangeeColor(_startColor);
        }

        public void ResetColor()
        {
            ChangeeColor(_startColor);
        }

        public void ChangeeColor(ColorType setColor)
        {
            if (_colors.TryGetValue(setColor, out Color playerColor))
            {
                _meshrenderer.material.color = playerColor;
                currentColor = setColor;
            }
        }
    }
}