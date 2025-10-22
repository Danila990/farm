using System;
using UnityEngine;

namespace _Project
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "MySO/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField] public PlayerInfo playerInfo { get; private set; }
        [field: SerializeField] public ArrowInfo arrowInfo { get; private set; }
    }

    [Serializable]
    public class PlayerInfo
    {
        [field: SerializeField] public PlayerView prefab { get; private set; }
        [field: SerializeField] public MoverStats stats { get; private set; }
        [field: SerializeField] public ColorType startColor { get; private set; }
    }

    [Serializable]
    public class MoverStats
    {
        [field: SerializeField, Range(0, 1),] public float rotateDuration { get; private set; } = 0.5f;
        [field: SerializeField, Range(0, 1)] public float jumpDuration { get; private set; } = 0.5f;
        [field: SerializeField, Range(0, 3)] public float jumpHeigh { get; private set; } = 1;
    }

    [Serializable]
    public class ArrowInfo
    {
        [field: SerializeField] public PlayerArrow prefab { get; private set; }
        [field: SerializeField] public ArrowStats stats { get; private set; }
    }

    [Serializable]
    public class ArrowStats
    {
        [field: SerializeField] public float modelOffsetY { get; private set; } = 5;
        [field: SerializeField, Range(0, 1),] public float rotateDuration { get; private set; } = 0.2f;
    }
}