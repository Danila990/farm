using System;

namespace _Project
{
    public class MobileInputService : InputService
    {
        public override event Action<DirectionType> DirectionChanged;
        public override event Action<ColorType> ColorChanged;

        public override void Tick()
        {
            throw new NotImplementedException();
        }
    }
}