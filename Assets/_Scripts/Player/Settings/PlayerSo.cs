using System;
using UnityEngine;

namespace _Project
{
    [CreateAssetMenu(fileName = "PlayerSo", menuName = "MySO/PlayerSo")]
    public class PlayerSo : ScriptableObject
    {
        [field: SerializeField] public PlayerData playerData { get; private set; }
        [field: SerializeField] public ArrowData arrowData { get; private set; }
    }

    [Serializable]
    public struct PlayerData
    {
        [field: SerializeField] public Player prefab { get; private set; }
        [field: SerializeField] public PlayerStats stats { get; private set; }
    }

    [Serializable]
    public struct ArrowData
    {
        [field: SerializeField] public PlayerArrow prefab { get; private set; }
        [field: SerializeField] public ArrowStats stats { get; private set; }
    }

    [Serializable]
    public class PlayerStats
    {
        [field: SerializeField, Range(0, 1),] public float rotateDuration { get; private set; } = 0.5f;
        [field: SerializeField, Range(0, 1)] public float jumpDuration { get; private set; } = 0.5f;
        [field: SerializeField, Range(0, 3)] public float jumpHeigh { get; private set; } = 1;
    }

    [Serializable]
    public class ArrowStats
    {
        [field: SerializeField] public float modelOffsetY { get; private set; } = 5;
        [field: SerializeField, Range(0, 1),] public float rotateDuration { get; private set; } = 0.2f;
    }
}
