using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour 
{
    [SerializeField]
    private bool isInCorner = true;
    [SerializeField]
    private bool isInMiddle = true;
    [SerializeField]
    private Vector2 size;
    [SerializeField]
    [Min(1)]
    private int minAmount;
    private const int maxAmount = 10;

    public bool IsInCorner { get => isInCorner; }
    public bool IsInMiddle { get => isInMiddle; }
    public Vector2 Size { get => size; }
    public int MinAmount { get => minAmount; }
    public int MaxAmount { get => maxAmount; }

}
