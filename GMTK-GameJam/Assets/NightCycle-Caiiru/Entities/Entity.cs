using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("HP Settings")]
    public int maxHP;
    public int currentHP;

    public virtual void Create()
    {
        this.currentHP = maxHP;
    }
    public virtual void TakeDamage(int _damage)
    {
        this.currentHP -= _damage <= 0 ? 0 : currentHP - _damage;
    }
    
}
