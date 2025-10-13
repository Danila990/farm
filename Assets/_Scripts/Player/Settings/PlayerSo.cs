using System;
using UnityEngine;

namespace _Project
{
    [CreateAssetMenu(fileName = "PlayerSo", menuName = "MySO/PlayerSo")]
    public class PlayerSo : ScriptableObject
    {
        [field: SerializeField] public PlayerInfo playerInfo { get; private set; }
        [field: SerializeField] public ArrowInfo arrowInfo { get; private set; }
        [field: SerializeField] public Player playerPrefab { get; private set; }
        [field: SerializeField] public PlayerArrow arrowPrefab { get; private set; }
    }

    [Serializable]
    public class PlayerInfo
    {
        [field: SerializeField, Range(0, 1),] public float rotateDuration { get; private set; } = 0.5f;
        [field: SerializeField, Range(0, 1)] public float jumpDuration { get; private set; } = 0.5f;
        [field: SerializeField, Range(0, 3)] public float jumpHeigh { get; private set; } = 1;
    }

    [Serializable]
    public class ArrowInfo
    {
        [field: SerializeField] public float modelOffsetY { get; private set; } = 5;
        [field: SerializeField, Range(0, 1),] public float rotateDuration { get; private set; } = 0.2f;
    }
}
