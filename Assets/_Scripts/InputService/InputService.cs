using System;
using UnityEngine;

namespace _Project
{
    public abstract class InputService : MonoBehaviour, IInputService
    {
        public abstract event Action<DirectionType> DirectionChanged;
        public abstract event Action<ColorType> ColorChanged;

        public abstract void Tick();
    }
}