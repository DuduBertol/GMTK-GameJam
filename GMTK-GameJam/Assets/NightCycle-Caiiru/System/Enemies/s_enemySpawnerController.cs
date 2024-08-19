using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class s_enemySpawnerController : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    
    [Header("Spawn Enemies")]
    [SerializeField][Tooltip("How much to spawn")] 
    public EnemiesToSpawn[] enemiesToSpawn; 
    private int _enemiesCount;
    [SerializeField] private float _timeBetweenEachSpawn = 0.1f;
    [SerializeField] private int _spawnCount = 0;
    [SerializeField] private bool spawningEnemies = false;
    private float _lastSpawnTime;
    [Header("Enemy Pool")]
    public GameObject emptyEnemyPrefab;
    [SerializeField] private List<GameObject> enemiesPrefab;
    private GameObject[] _enemiesPool;

    [Header("Game Controller")]
    public GameController gameController;

    [Header("Editor only")] public bool poolActive;
    void Start()
    {
        if (!poolActive)
            return;
        _enemiesPool = new GameObject[50];
        for (int i = 0; i < 50; i++)
        {
            //var _instance = Instantiate(enemiesPrefab[UnityEngine.Random.Range(0, enemiesPrefab.Count)]);
            var _instance = Instantiate(emptyEnemyPrefab,transform);
             
            _enemiesPool[i] = _instance;
            _instance.gameObject.SetActive(false);
        }

        GameController.Instance.OnDayNightChanged += OnTimeChanged;
        
        //SpawnEnemies(_enemiesCount, _spawnPosition.position, GameController.Instance.GetPlayer().transform);

    }  
    IEnumerator ISpawnEnemy(Vector3 _position, s_EnemyStats stats){
        yield return new WaitForSeconds(_timeBetweenEachSpawn);
        
        var _enemy = GetEnemy();
        _enemy.transform.position = _position;
        _enemy.transform.GetComponent<StateMachine>().StartStateMachine(stats);
        _enemy.gameObject.SetActive(true);
    }
    public GameObject GetEnemy()
    {
        foreach (var _instance in _enemiesPool)
        {
            if (!_instance.gameObject.activeInHierarchy)
            {
                return _instance;
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawningEnemies || _spawnCount <= 0) return;

        if(Time.time < _lastSpawnTime) return;

        //SpawnEnemies(_enemiesCount, _spawnPosition.position, null);
        //SpawnEnemy(_spawnPosition.position);

        
    }

    void OnDisable(){
        GameController.Instance.OnDayNightChanged -= OnTimeChanged;

    }

    void OnTimeChanged(object sender, EventArgs args){
        Debug.Log($"Now its Day? {GameController.Instance.GetIsDay()}");
        if(GameController.Instance.GetIsDay()){
            //TODO all active enemies return to home
        }
        else{
            //Spawn Enemies based on day count
            int _enemiesToSpawn = 0;
            foreach(var _value in enemiesToSpawn){
                if(_value.dayIndex == GameController.Instance.GetDayNumber()){
                    foreach(var _enemies in _value.enemies){
                        _enemiesToSpawn += _enemies.count;
                        for(int i = 0; i< _enemies.count; i++){
                            StartCoroutine(ISpawnEnemy(_spawnPosition.position, _enemies.enemy));
                        }
                    }  

                }
                
            } 
             

        }
    }
}

[System.Serializable]
public struct EnemiesToSpawn{
    public string name;
    public int dayIndex; 
    public EnemyInfo[] enemies;
}

[System.Serializable]
public struct EnemyInfo{
    public string name;
    public s_EnemyStats enemy;
    public int count;
}
 