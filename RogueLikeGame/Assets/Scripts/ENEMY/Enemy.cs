using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int points = 10;
    [SerializeField]
    private int attackDamage = 20;
    [SerializeField]
    private int spellDamage = 45;
    [SerializeField]
    private Vector3 attackOffset = new Vector3(0,0,0);
    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private LayerMask attackMask = 7;
    [SerializeField]
    private float moveSpeed = 5f;



    public int MaxHealth { get => maxHealth; }
    public int Points { get => points; }
    public int AttackDamage { get => attackDamage; }

    public int SpellDamage { get => spellDamage; }
    public Vector3 AttackOffset { get => attackOffset; }
    public float AttackRange { get => attackRange; }
    public LayerMask AttackMask { get => attackMask; }
    public float MoveSpeed { get => moveSpeed; }
}
