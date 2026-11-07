using System;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public event Action MouseClicked;

    [SerializeField] private MouseButtons _mouseBtn;

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)_mouseBtn))
        {
            MouseClicked?.Invoke();
        }
    }

    private enum MouseButtons
    {
        LeftMouseBtn = 0,
        RightMouseBtn = 1,
    }
}