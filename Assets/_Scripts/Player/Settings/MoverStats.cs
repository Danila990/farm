using System;
using UnityEngine;

namespace _Project
{
    [CreateAssetMenu(fileName = "MoverStats", menuName = "MySO/MoverStats")]
    public class MoverStats : ScriptableObject
    {
        [field: SerializeField, Range(0, 1),] public float rotateDuration { get; private set; } = 0.5f;
        [field: SerializeField, Range(0, 1)] public float jumpDuration { get; private set; } = 0.5f;
        [field: SerializeField, Range(0, 3)] public float jumpHeigh { get; private set; } = 1;
    }
}
