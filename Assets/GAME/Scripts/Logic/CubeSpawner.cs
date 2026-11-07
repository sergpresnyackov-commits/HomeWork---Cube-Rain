using UnityEngine;

[RequireComponent(typeof(ObjectSpawner))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _poolSize;

    [SerializeField] private float _spawnHeight;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _spawnDelay;

    private ObjectSpawner _objectSpawner;

    private void Awake()
    {
        if (TryGetComponent(out ObjectSpawner objectSpawner))
        {
            _objectSpawner = objectSpawner;

            _objectSpawner.Init(_cubePrefab.gameObject, _poolSize, _spawnHeight, _spawnRadius, _spawnDelay);
        }
    }

    private void OnEnable()
    {
        _objectSpawner.GameObjectSpawned += SubscribeToNewCube;
    }

    private void OnDisable()
    {
        _objectSpawner.GameObjectSpawned -= SubscribeToNewCube;
    }

    private void SubscribeToNewCube(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Cube cube))
        {
            cube.Deleted += ReturnCubeToPool;
        }
    }

    private void ReturnCubeToPool(Cube cube)
    {
        cube.Deleted -= ReturnCubeToPool;
        _objectSpawner.ReturnToPool(cube.gameObject);
    }
}