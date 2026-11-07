using System.Collections.Generic;
using UnityEngine;

public class PoolHandler : MonoBehaviour
{
    private Queue<GameObject> _poolQueue;
    private GameObject _prefab;

    public void CreatePool(GameObject prefab, int poolSize)
    {
        _poolQueue = new Queue<GameObject>();

        _prefab = prefab;

        for (int i = 0; i < poolSize; i++)
        {
            var gameObject = Instantiate(_prefab, this.transform);
            gameObject.SetActive(false);
            _poolQueue.Enqueue(gameObject);
        }
    }

    public GameObject Get(Vector3 position, Quaternion rotation)
    {
        GameObject gameObject;

        if (_poolQueue.Count > 0)
        {
            gameObject = _poolQueue.Dequeue();
        }
        else
        {
            gameObject = Instantiate(_prefab, this.transform);
        }

        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.SetActive(true);

        return gameObject;
    }

    public void Return(GameObject gameObject)
    {
        gameObject.SetActive(false);
        _poolQueue.Enqueue(gameObject);
    }
}