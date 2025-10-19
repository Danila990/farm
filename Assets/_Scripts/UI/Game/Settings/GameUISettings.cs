using UnityEngine;

namespace _Project
{
    [CreateAssetMenu(fileName = "GameUISettings", menuName = "MySO/GameUISettings")]
    public class GameUISettings : ScriptableObject
    {
        [field: SerializeField] public ColorPanel colorPanel { get; private set; }
    }
}