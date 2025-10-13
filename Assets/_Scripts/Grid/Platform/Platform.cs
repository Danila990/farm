using UnityEngine;

namespace _Project
{
    public class Platform : MonoBehaviour
    {
        [field: SerializeField] public PlatformType platformType { get; private set; }
        [field: SerializeField] public bool canMove { get; private set; } = true;

        [field: SerializeField, HideInInspector] public Vector2Int platformIndex { get; private set; }

        public void SetIndex(Vector2Int index) => platformIndex = index;

    }
}