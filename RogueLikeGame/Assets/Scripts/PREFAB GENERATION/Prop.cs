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

    public bool IsInCorner { get => isInCorner; }
    public bool IsInMiddle { get => isInMiddle; }
    public Vector2 Size { get => size; }
}
