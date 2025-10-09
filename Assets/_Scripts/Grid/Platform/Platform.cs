using UnityEngine;

namespace _Project
{
    public class Platform : MonoBehaviour
    {
        [field: SerializeField] public PlatformType Type { get; private set; }
        [field: SerializeField] public Vector2Int Index { get; private set; }
        [field: SerializeField] public bool CanMove { get; private set; } = true;

        public void SetIndex(Vector2Int index) => Index = index;

    }
}