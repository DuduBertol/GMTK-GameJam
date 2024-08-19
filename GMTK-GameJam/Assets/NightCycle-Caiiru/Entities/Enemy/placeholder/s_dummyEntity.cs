using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_dummyEntity : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
