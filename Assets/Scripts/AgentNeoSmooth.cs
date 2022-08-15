using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AgentNeoSmooth : MonoBehaviour
{
    //Sprite
    public GameObject agentSprite;

    //Attributs
    public int maxHealth;
    public int currentHealth;

    // Movement
    private Rigidbody2D _rigidbody;
    private Vector2 movementDirection;
    public float movementSpeed;

    // Animations
    private Animator animator;
    private readonly int isMoving = Animator.StringToHash("isMoving");
    private readonly int isAttacking = Animator.StringToHash("isAttacking");


    private Vector2 pointerInput, movementInput;

    private WeaponParent weaponParent;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    public void PerformAttack()
    {
        weaponParent.Attack();
    }

    private void Awake()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    public void FixedUpdate()
    {
        //pointerInput = GetPointerPosition();
        weaponParent.PointerPosition = pointerInput;

        Move();
        AnimateCharacter();

        // Delta Time = Time Since Last Function Call => Movement Not Affected By Function Interval
        _rigidbody.MovePosition(_rigidbody.position + Time.fixedDeltaTime * movementSpeed * movementDirection);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    weaponParent.PerformAnAttack();
        //}
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;

        // Flip Sprite Depending On Direction
        if (lookDirection.x > 0)
        {
            //rechts
            transform.rotation = Quaternion.Euler(0f, 0, 0f);
        }
        else if (lookDirection.x < 0)
        {
            //links
            transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        }

        // Toggle Movement Animation
        agentSprite.GetComponent<Animator>().SetBool(isMoving, movementDirection != Vector2.zero);
    }

    private void Move()
    {
        //movementDirection.x = Input.GetAxisRaw("Horizontal");
        //movementDirection.y = Input.GetAxisRaw("Vertical");

        //movementDirection.x = movement.action.ReadValue<Vector2>().x;
        //movementDirection.y = movement.action.ReadValue<Vector2>().y;

        // Normalized => Equal Velocity In All Directions
        movementDirection = movementDirection.normalized;
    }


    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            DestroyPlayer();
        }
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
