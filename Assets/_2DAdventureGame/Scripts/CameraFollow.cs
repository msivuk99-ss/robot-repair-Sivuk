using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // Об'єкт, за яким стежить камера (гравець)
    public float smoothSpeed = 0.125f; // Плавність руху
    public Vector3 offset = new Vector3(0, 0, -10f); // Зсув камери по Z

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}