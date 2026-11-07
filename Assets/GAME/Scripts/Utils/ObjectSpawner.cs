using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MouseInput))]
[RequireComponent(typeof(PoolHandler))]
public class ObjectSpawner : MonoBehaviour
{
    public event Action<GameObject> GameObjectSpawned;

    private GameObject _prefab;
    private int _poolSize;

    private float _spawnHeight;
    private float _spawnRadius;
    private float _spawnDelay;

    private bool _isActive;

    private Coroutine _coroutine;
    private MouseInput _inputHandler;
    private PoolHandler _poolHandler;

    private void OnEnable()
    {
        _inputHandler.MouseClicked += StartStopCounter;
    }

    private void OnDisable()
    {
        _inputHandler.MouseClicked += StartStopCounter;
    }

    public void Init(GameObject prefab, int poolSize, float spawnHeight, float spawnRadius, float spawnDelay)
    {
        _prefab = prefab;
        _poolSize = poolSize;
        _spawnHeight = spawnHeight;
        _spawnRadius = spawnRadius;
        _spawnDelay = spawnDelay;

        if (TryGetComponent(out MouseInput inputHandler))
        {
            _inputHandler = inputHandler;
        }

        if (TryGetComponent(out PoolHandler poolHandler))
        {
            _poolHandler = poolHandler;
            _poolHandler.CreatePool(_prefab.gameObject, _poolSize);
        }
    }

    public void StartStopCounter()
    {
        _isActive = !_isActive;

        if (_isActive)
        {
            _coroutine = StartCoroutine(SpawnEnemies(_spawnDelay));
        }
        else
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator SpawnEnemies(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (_isActive)
        {
            yield return wait;

            float randomX = UnityEngine.Random.Range(-_spawnRadius, _spawnRadius);
            float randomZ = UnityEngine.Random.Range(-_spawnRadius, _spawnRadius);

            Vector3 spawnPosition = new Vector3(randomX, _spawnHeight, randomZ);

            float randomAngleX = UnityEngine.Random.Range(0, 360);
            float randomAngleY = UnityEngine.Random.Range(0, 360);
            float randomAngleZ = UnityEngine.Random.Range(0, 360);

            GameObject spawnedObject = _poolHandler.Get(spawnPosition, Quaternion.Euler(randomAngleX, randomAngleY, randomAngleZ));

            GameObjectSpawned?.Invoke(spawnedObject);
        }
    }

    public void ReturnToPool(GameObject gameObject)
    {
        _poolHandler.Return(gameObject);
    }
}