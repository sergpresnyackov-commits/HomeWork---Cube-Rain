using UnityEngine;

public class CubeDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Cube cubePainter))
        {
            cubePainter.Detect();
        }
    }
}