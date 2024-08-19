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
        
        var _popupDamage = s_popupManager.Instance.GetPopoup();
        _popupDamage.transform.position = this.transform.position + new Vector3(0,transform.localScale.y/2,0);
        _popupDamage.gameObject.SetActive(true);
        _popupDamage.GetComponent<s_popupController>().Create(_damage.ToString());
        
        if (currentHP == 0) Die();
    }

    public virtual void Die()
    {
        Debug.Log(gameObject.name + " died");
    }
    
}
