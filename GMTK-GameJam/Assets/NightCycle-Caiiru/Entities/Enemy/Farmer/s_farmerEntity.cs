using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_farmerEntity : EnemyEntity
{
    [Header("Take Damage")]

    [Header("Debug")] public bool takeDamageButton = false;

    private void OnEnable()
    {
        Create();
    }

    public override void Create()
    {
        base.Create();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
       
        
    }

    public override void Die()
    {
        base.Die();
        //this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (takeDamageButton)
        {
            takeDamageButton = false;
            TakeDamage(1);
        }
    }
} 