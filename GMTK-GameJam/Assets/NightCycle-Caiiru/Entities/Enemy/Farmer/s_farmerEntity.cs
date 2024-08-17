using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_farmerEntity : Entity
{
    [Header("Take Damage")]
    public ParticleSystem _hitParticle;

    [Header("Debug")] public bool takeDamageButton = false;
    public override void Create()
    {
        base.Create();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        _hitParticle.Play();
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
