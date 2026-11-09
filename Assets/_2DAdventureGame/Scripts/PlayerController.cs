using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public GameObject projectilePrefab;
    public GameObject hitEffectPrefab;

    public InputAction MoveAction;
    public InputAction LaunchAction;
    public InputAction TalkAction;

    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private Vector2 moveDirection = new Vector2(1, 0);
    private Vector2 currentInput;

    private int currentHealth;
    public int health => currentHealth;

    private bool isInvincible;
    private float damageCooldown;

    private NonPlayerCharacter lastNonPlayerCharacter;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        MoveAction.Enable();
        LaunchAction.Enable();
        TalkAction.Enable();
    }

    void Update()
    {
        // Отримання руху
        Vector2 move = MoveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        currentInput = move;

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // Таймер невразливості
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        // Запуск шестерні (C)
        if (LaunchAction.WasPressedThisFrame())
        {
            Launch();
        }

        // Перевірка NPC променем (Raycast)
        RaycastHit2D hit = Physics2D.Raycast(
            rigidbody2d.position + Vector2.up * 0.2f,
            moveDirection,
            1.5f,
            LayerMask.GetMask("NPC")
        );

        if (hit.collider != null)
        {
            NonPlayerCharacter npc = hit.collider.GetComponent<NonPlayerCharacter>();
            if (npc != null)
            {
                if (npc.dialogueBubble != null)
                {
                    npc.dialogueBubble.SetActive(true);
                }
                lastNonPlayerCharacter = npc;
            }
            FindFriend();
        }
        else
        {
            if (lastNonPlayerCharacter != null)
            {
                if (lastNonPlayerCharacter.dialogueBubble != null)
                {
                    lastNonPlayerCharacter.dialogueBubble.SetActive(false);
                }
                lastNonPlayerCharacter = null;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position += currentInput * speed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) return;
            isInvincible = true;
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (UIHandler.instance != null)
        {
            UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
        }
    }

    void Launch()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("ПОМИЛКА: Не перетягнуто Projectile Prefab у скрипт PlayerController!");
            return;
        }

        GameObject projectileObject = Instantiate(
            projectilePrefab,
            rigidbody2d.position + Vector2.up * 0.5f,
            Quaternion.identity
        );

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Launch(moveDirection, 300);
        }

        animator.SetTrigger("Launch");
    }

    void FindFriend()
    {
        if (TalkAction.WasPressedThisFrame())
        {
            if (UIHandler.instance != null)
            {
                UIHandler.instance.DisplayDialogue();
            }
        }
    }
}