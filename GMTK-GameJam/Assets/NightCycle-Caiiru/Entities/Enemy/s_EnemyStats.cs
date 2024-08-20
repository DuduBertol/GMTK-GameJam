using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/EnemyStats", fileName = "New Enemy Stats")]
public class s_EnemyStats : ScriptableObject
{ 
    public string name; 
    [Header("Life Settings")]
    public int currentHP;
    public int maxHP;

    [Space(5)] [Header("Vision and Movement Settings")]
    public float movementSpeed;
    public float movementRate;
    public float stoppingDistance;

    public float visionRange;

    [Space(5)] [Header("Attack Settings")] 
    public float attackRange;
    public float attackRate;
    public float attackDamage;
 
    
    [Space(5)]
    [Header("Enemy Mesh Info")]
    public Mesh mesh;
    public List<Material> materials;
    public GameObject meshHolder;
}
