using System;

namespace _Project
{
    public interface IInputService
    {
        public event Action<DirectionType> DirectionChanged;
        public event Action<ColorType> ColorChanged;
    }
}