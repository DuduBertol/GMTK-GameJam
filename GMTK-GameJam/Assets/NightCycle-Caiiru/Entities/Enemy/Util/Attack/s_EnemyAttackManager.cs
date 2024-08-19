using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_EnemyAttackManager : MonoBehaviour
{
    [Header("Attack settings")] 
    public bool isVisible;

  
    private float _nextFireTime;

    [Header("Pool Settings")]   
    public int projectileCount;
    public void Attack(Vector3 direction, float damage, float fireRate)
    {
        if (Time.time <= _nextFireTime)
            return;
        
        Debug.Log("Attack");
        _nextFireTime = Time.time + fireRate;
        var _projectile = s_enemyProjectilesController.Instance.GetProjectile();
        _projectile.SetActive(true);
        _projectile.transform.position = this.transform.position;
        _projectile.transform.rotation = this.transform.rotation;
        _projectile.transform.GetComponent<s_Projectile>().Create(direction,(int)damage);

        
    }
    void Start()
    {
         
    }
 

    // Update is called once per frame
    void Update()
    {
        
    }
}
