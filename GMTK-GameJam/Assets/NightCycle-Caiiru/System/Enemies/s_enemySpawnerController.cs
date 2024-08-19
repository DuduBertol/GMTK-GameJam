using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class s_enemySpawnerController : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    
    [Header("Spawn Enemies")]
    [SerializeField][Tooltip("How much to spawn")] 
    private int _enemiesCount;
    [SerializeField] private float _timeBetweenEachSpawn = 0.1f;
    [SerializeField] private int _spawnCount = 0;
    [SerializeField] private bool spawningEnemies = false;
    private float _lastSpawnTime;
    [Header("Enemy Pool")]
    [SerializeField] private List<GameObject> enemiesPrefab;
    private GameObject[] _enemiesPool;

    [Header("Editor only")] public bool poolActive;
    void Start()
    {
        if (!poolActive)
            return;
        _enemiesPool = new GameObject[50];
        for (int i = 0; i < 50; i++)
        {
            var _instance = Instantiate(enemiesPrefab[Random.Range(0, enemiesPrefab.Count)]);
            _instance.transform.SetParent(this.transform);
            _enemiesPool[i] = _instance;
            _instance.gameObject.SetActive(false);
        }
        
        //SpawnEnemies(_enemiesCount, _spawnPosition.position, GameController.Instance.GetPlayer().transform);

    } 

    public void SpawnEnemies(int _value, Vector3 _position, [CanBeNull] Transform targetTransform )
    {
        if (targetTransform == null)
            targetTransform = GameController.Instance.GetPlayer().transform;
        if (_value <= 0)
        {
            Debug.LogWarning("value incorrect");
            return;
        }
        
        for(int i = 0; i<_value; i++)
        {
            var _enemy = GetEnemy();
            _enemy.transform.position = _position;
            _enemy.gameObject.SetActive(true);
            //_enemy.GetComponent<s_enemyController>().SetDestination(targetTransform);
        }
    }

    public void SpawnEnemy(Vector3 _position){
        _spawnCount -= 1;
        spawningEnemies = _spawnCount <= 0 ? false : true;
        _lastSpawnTime = Time.time + _timeBetweenEachSpawn;

        var _enemy = GetEnemy();
        _enemy.transform.position = _position;
        _enemy.transform.GetComponent<StateMachine>().StartStateMachine();
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
        SpawnEnemy(_spawnPosition.position);

        
    }
}
