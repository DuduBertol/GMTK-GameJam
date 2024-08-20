using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    [Header("Particles")]
    public ParticleSystem _hitParticle;
    public s_EnemyStats stats;
    public override void Create()
    {
        stats = this.GetComponent<StateMachine>().stats;
        this.maxHP = stats.maxHP;
        this.currentHP = stats.currentHP;  
        GetComponentInChildren<MeshFilter>().mesh = stats.mesh;
        //GetComponentInChildren<MeshRenderer>().materials[0] = stats.material;
        GetComponentInChildren<MeshRenderer>().SetMaterials(stats.materials);
        this.transform.name = stats.name;

    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        _hitParticle.Play();
        
    }

    public override void Die()
    {
        transform.GetComponent<StateMachine>().EndStateMachine();
        base.Die();
    }
}