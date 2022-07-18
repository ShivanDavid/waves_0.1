using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCombat : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    #endregion

    #region Private Variables
    public RaycastHit2D hit;
    public GameObject target;
    public Animator animator;
    public float distance;
    public bool attackMode;
    public bool inRange;
    public bool cooling;
    public float intTimer;
    #endregion


    public GameObject triggerArea;
    public GameObject hitbox;
    public GameObject enemyCollider;
    public GameObject rayCastObj;

    public void Awake()
    {
        intTimer = timer;
        animator = GetComponent<Animator>();

    }

    public void Update()
    {
        if(inRange && gameObject.transform.rotation.y == 180f)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, rayCastMask);
            RayCastDebugger();
        }
        else if(inRange && gameObject.transform.rotation.y == 0f)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.right, rayCastLength, rayCastMask);
            RayCastDebugger();
        }

        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange = false;
        }

        if(inRange == false)
        {
            gameObject.GetComponent<Animator>().SetBool("Run", false);
            StopAttack();

            //animator.SetBool("Run", false);
        }
    }

    public void StopAttack()
    {
        cooling = false;
        attackMode = false;
        gameObject.GetComponent<Animator>().SetBool("Attack", false);
    }

    public void Attack()
    {
        timer = intTimer;
        attackMode = true;

        if (target != null)
            target.GetComponent<CharackterController>().TakeDamage(target.GetComponent<PlayerCombat>().attackDamage);

        gameObject.GetComponent<Animator>().SetBool("Run", false);
        gameObject.GetComponent<Animator>().SetBool("Attack", true);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<CharackterController>().CompareTag("Player"))
        {

            target = collision.gameObject.GetComponentInParent<CharackterController>().gameObject;
            inRange = true;
        }
    }

    public void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackDistance)
        {
            gameObject.GetComponent<NpcController>().Move();
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        if(cooling)
        {
            Cooldown();
            gameObject.GetComponent<Animator>().SetBool("Attack", false);
        }
    }

    public void RayCastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        }
        else if(attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }

    public void TriggerCool()
    {
        cooling = true;
    }

    public void Cooldown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }
}
