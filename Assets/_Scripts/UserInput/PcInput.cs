using UnityEngine;

namespace _Project
{
    public class PcInput : BaseUserInput
    {
        protected override void UpdateDirection()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                _currentDirection = DirectionType.Up;
                return;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                _currentDirection = DirectionType.Down;
                return;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _currentDirection = DirectionType.Left;
                return;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _currentDirection = DirectionType.Right;
                return;
            }
        }
    }
}