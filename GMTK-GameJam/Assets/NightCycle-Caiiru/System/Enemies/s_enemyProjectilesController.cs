using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_enemyProjectilesController : MonoBehaviour
{
    [Header("Pool Settings")]
    public GameObject projectilePrefab;
    public GameObject[] projectilePool;
    public int projectilesCount;

    public static s_enemyProjectilesController Instance;

    void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this.gameObject);
        }
        Instance = this;
    }
    void Start()
    {
        if(projectilePrefab==null) return;
        projectilePool = new GameObject[projectilesCount];
        for(int i=0; i<projectilesCount;i++){
            var _instance = Instantiate(projectilePrefab, this.transform);
            projectilePool[i] = _instance;
            _instance.SetActive(false);
        }
    }

    public GameObject GetProjectile(){
        foreach(var _instance in projectilePool){
            if(!_instance.activeInHierarchy){
                return _instance;
            }
        }
        return null;
    }
 
}
