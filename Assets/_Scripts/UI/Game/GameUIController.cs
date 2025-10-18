using UnityEngine;
using UnityServiceLocator;

namespace _Project
{
    public class GameUIController : MonoBehaviour
    {
        public void Configure()
        {
            ServiceLocator.Locator.Register(this);
        }
    }
}