using System;

namespace _Project
{
    public interface IUserInput
    {
        public event Action<DirectionType> OnDirectionInput;
        public void Tick();
    }
}