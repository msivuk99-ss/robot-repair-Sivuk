using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4.0f;
    Rigidbody2D rigidbody2d;
    Vector2 move;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        move = new Vector2(horizontal, vertical).normalized;
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position + move * speed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }
}