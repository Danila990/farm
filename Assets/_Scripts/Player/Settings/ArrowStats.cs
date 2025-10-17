using System;
using UnityEngine;

namespace _Project
{
    [CreateAssetMenu(fileName = "ArrowStats", menuName = "MySO/ArrowStats")]
    public class ArrowStats : ScriptableObject
    {
        [field: SerializeField] public float modelOffsetY { get; private set; } = 5;
        [field: SerializeField, Range(0, 1),] public float rotateDuration { get; private set; } = 0.2f;
    }
}
