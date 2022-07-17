using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class NpcController : MonoBehaviour
{
    public NPC npc;

    public Animator animator;

    public new Rigidbody2D rigidbody;

    private Vector2 movementDirection;

    private int currentHealth;
    public int maxHealth;
    public int damage;
    public int level;

    public Transform enemyGFX;

    public bool isHit;

    

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        InitNPC();
        
    }

    public void Update()
    {

        if (gameObject.transform.position.x >= GameObject.FindGameObjectWithTag("Player").transform.position.x && !isHit)
        {
            //face left
            transform.rotation = Quaternion.Euler(0, -180f, 0);
        }
        else if(gameObject.transform.position.x <= GameObject.FindGameObjectWithTag("Player").transform.position.x && !isHit)
        {
            //face right
            transform.rotation = Quaternion.Euler(0, 0, 0f);
        }
    }

    public void Move()
    {
        animator.SetBool("Run", true);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            gameObject.GetComponent<AIPath>().enabled = true;
        }
    }



    public void InitNPC()
    {
        this.maxHealth = npc.maxHealth;
        this.currentHealth = npc.maxHealth;
        this.damage = npc.damage;
        this.level = npc.level;

    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        isHit = true;

        // Hurt Anim
        animator.SetTrigger("Hurt");
        StartCoroutine(FlashRed());
        Invoke(nameof(WaitForAnimation), 1.0f);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void WaitForAnimation()
    {
        isHit = false;
    }

    public IEnumerator FlashRed()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    private void Die()
    {

        // Die Anim
        animator.SetBool("IsDead", true);

        //Disable enemy
        this.enabled = false;
        GetComponent<AIPath>().enabled = false;
        Invoke(nameof(DestroyNPC), 1.0f);
    }

    //mal nachschauen: how Boids work

    public void DestroyNPC()
    {
        Destroy(gameObject);
    }


}
