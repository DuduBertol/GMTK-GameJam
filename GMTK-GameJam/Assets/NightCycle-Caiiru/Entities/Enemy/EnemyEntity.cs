using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyEntity : Entity
{
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
        transform.GetComponent<StateMachine>().EndStateMachine();
        this.gameObject.SetActive(false);
        base.Die();
    }
}