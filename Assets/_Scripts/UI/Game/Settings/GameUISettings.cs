using UnityEngine;

namespace _Project
{
    [CreateAssetMenu(fileName = "GameUISettings", menuName = "MySO/GameUISettings")]
    public class GameUISettings : ScriptableObject
    {
        [field: SerializeField] public PlayerColorPanel colorPanel { get; private set; }
    }
}