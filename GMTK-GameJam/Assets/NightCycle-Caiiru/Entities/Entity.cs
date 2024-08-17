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
        this.currentHP = currentHP - _damage <= 0 ? 0 : currentHP - _damage;
        if (currentHP == 0) Die();
    }

    public virtual void Die()
    {
        Debug.Log(gameObject.name + " died");
    }
    
}
