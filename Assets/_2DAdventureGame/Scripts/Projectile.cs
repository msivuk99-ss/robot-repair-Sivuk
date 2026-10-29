using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject hitEffectPrefab;
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        if (rigidbody2d != null)
        {
            rigidbody2d.AddForce(direction * force);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ігноруємо колізію з самим гравцем
        if (other.GetComponent<PlayerController>() != null)
            return;

        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Fix(); // Ремонтуємо робота
        }

        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}