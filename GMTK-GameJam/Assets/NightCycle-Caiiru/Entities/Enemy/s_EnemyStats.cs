using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/EnemyStats", fileName = "New Enemy Stats")]
public class s_EnemyStats : ScriptableObject
{
    [Header("Life Settings")]
    public int currentHP;
    public int maxHP;

    [Space(5)] [Header("Vision and Movement Settings")]
    public float movementSpeed;
    public float movementRate;
    public float stoppingDistance;

    [Space(5)] [Header("Attack Settings")] 
    public float attackRange;
    public float attackRate;
    public float attackDamage;
}
