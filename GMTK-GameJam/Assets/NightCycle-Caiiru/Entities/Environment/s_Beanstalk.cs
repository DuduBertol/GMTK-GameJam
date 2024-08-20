using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_Beanstalk : Entity
{
    public int _level;
    public override void Create()
    {
        base.Create();
    }
    public override void Die()
    {
        base.Die();
        SceneManager.LoadScene("EndScene");
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        
    }
    public void IncreaseTreeLevel(){
        _level++;
        this.maxHP = 10 * _level;
        this.currentHP = maxHP;
    }
}
