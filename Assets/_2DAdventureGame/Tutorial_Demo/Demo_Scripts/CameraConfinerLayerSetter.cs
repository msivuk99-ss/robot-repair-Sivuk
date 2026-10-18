using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Наш Ducko
    public float smoothSpeed = 5.0f;
    public Vector3 offset = new Vector3(0, 0, -10f); // Тримаємо Z координату камери (-10)

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}