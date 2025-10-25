using System;
using UnityEngine;

namespace _Project
{
    public class PCInputService : InputService
    {
        public override event Action<DirectionType> DirectionChanged;
        public override event Action<ColorType> ColorChanged;

        public override void Tick()
        {
            UpdateDirection();
            UpdateColor();
        }

        private void UpdateColor()
        {
            if(Input.GetKey(KeyCode.Alpha1))
                ColorChanged?.Invoke(ColorType.Red);
            else if(Input.GetKey(KeyCode.Alpha2))
                ColorChanged?.Invoke(ColorType.Green);
            else if(Input.GetKey(KeyCode.Alpha3))
                ColorChanged?.Invoke(ColorType.Blue);
        }

        private void UpdateDirection()
        {
            DirectionType direction = DirectionType.None;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                direction = DirectionType.Up;
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                direction = DirectionType.Down;
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                direction = DirectionType.Left;
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                direction = DirectionType.Right;

            if (direction == DirectionType.None) return;

            DirectionChanged?.Invoke(direction);
        }
    }
}