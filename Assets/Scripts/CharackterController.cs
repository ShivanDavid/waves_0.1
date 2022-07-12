using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharackterController : MonoBehaviour
{
    //Attributs
    public int maxHealth;
    public int currentHealth;

    // Movement
    private Rigidbody2D rigidbody;
    private Vector2 movementDirection;
    public float movementSpeed;

    // Animations
    private Animator animator;
    private readonly int isMoving = Animator.StringToHash("isMoving");
    private readonly int isAttacking = Animator.StringToHash("isAttacking");

    private SpriteRenderer renderer;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        Move();
    }

    private void Move()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");

        // Normalized => Equal Velocity In All Directions
        movementDirection = movementDirection.normalized;

        // Toggle Movement Animation
        animator.SetBool(isMoving, movementDirection != Vector2.zero);

        // Flip Sprite Depending On Direction
        renderer.flipX = movementDirection.x switch
        {
            < 0 => false,
            > 0 => true,
            _ => renderer.flipX
        };
    }

    public void FixedUpdate()
    {
        // Delta Time = Time Since Last Function Call => Movement Not Affected By Function Interval
        rigidbody.MovePosition(rigidbody.position + Time.fixedDeltaTime * movementSpeed * movementDirection);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
