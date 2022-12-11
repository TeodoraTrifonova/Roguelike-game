using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enemy
{
    [SerializeField]
    private static int maxHealth = 100;
    [SerializeField]
    private static int points = 10;
    [SerializeField]
    private static int attackDamage = 20;
    [SerializeField]
    private static Vector3 attackOffset;
    [SerializeField]
    private static float attackRange = 1f;
    [SerializeField]
    private static LayerMask attackMask = 7;
    [SerializeField]
    private static float moveSpeed = 5f;

    public static int MaxHealth { get => maxHealth; }
    public static int Points { get => points; }
    public static int AttackDamage { get => attackDamage; }
    public static Vector3 AttackOffset { get => attackOffset; }
    public static float AttackRange { get => attackRange; }
    public static LayerMask AttackMask { get => attackMask; }
    public static float MoveSpeed { get => moveSpeed; }
}
