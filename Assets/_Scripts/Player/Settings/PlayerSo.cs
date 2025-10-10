using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSo", menuName ="MySO")]
public class PlayerSo : ScriptableObject
{
    [field: SerializeField] public PlayerStats Stats { get; private set; }
}

public class PlayerStats
{
    [field: SerializeField] public float ModelOffsetY = 0;

    [Header("Rotate")]
    [field: SerializeField, Range(0, 1), ] public float RotateDuration = 0.5f;

    [Header("Jump")]
    [field: SerializeField, Range(0, 1)] public float JumpDuration = 0.5f;
    [field: SerializeField,
        Range(0, 3)] public float JumpHeigh = 1;

    [Header("Arrow")]
    [field: SerializeField, Range(0, 1), ] public float ArrowRotateDuration = 0.2f;
}
