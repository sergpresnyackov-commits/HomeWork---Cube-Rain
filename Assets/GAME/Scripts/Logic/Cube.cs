using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> Deleted;

    [SerializeField] private float _minDestroyDelay;
    [SerializeField] private float _maxDestroyDelay;

    private bool _isDetected;

    private Cube _thisCube;
    private Color _baseColor;

    private void Start()
    {
        _thisCube = GetComponent<Cube>();

        if (TryGetComponent(out Renderer renderer))
        {
            _baseColor = renderer.material.color;
        }
    }

    public void Detect()
    {
        if (_isDetected)
        {
            return;
        }

        _isDetected = true;

        ChangeColor();

        StartCoroutine(Delete());
    }

    private void ChangeColor()
    {
        TryGetComponent(out Renderer renderer);
        renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    private IEnumerator Delete()
    {
        float destroyDelay = UnityEngine.Random.Range(_minDestroyDelay, _maxDestroyDelay);

        yield return new WaitForSeconds(destroyDelay);

        ResetCondition();

        Deleted?.Invoke(_thisCube);
    }

    private void ResetCondition()
    {
        if (TryGetComponent(out Renderer renderer))
        {
            renderer.material.color = _baseColor;
        }

        _isDetected = false;
    }
}