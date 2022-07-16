using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public NPC npc;
    public SpriteRenderer sprite;

    public Animator animator;

    public new Rigidbody2D rigidbody;

    public GameObject target;
    private Vector2 movementDirection;

    private int currentHealth;
    public int maxHealth;
    public int damage;
    public int level;

    public Transform enemyGFX;

    

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        InitNPC();
    }

    public void Update()
    {

        if (gameObject.transform.position.x >= GameObject.FindGameObjectWithTag("Player").transform.position.x)
        {
            //face left
            transform.rotation = Quaternion.Euler(0, -180f, 0);
        }
        else
        {
            //face right
            transform.rotation = Quaternion.Euler(0, 0, 0f);
        }
    }

    public void InitNPC()
    {
        this.sprite = npc.sprite;
        this.maxHealth = npc.maxHealth;
        this.currentHealth = npc.maxHealth;
        this.damage = npc.damage;
        this.level = npc.level;

        target = null;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        // Hurt Anim
        animator.SetTrigger("Hurt");
        StartCoroutine(FlashRed());

        if(currentHealth <= 0)
        {
            Die();
        }
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
        GetComponent<Collider2D>().enabled = false;
        Invoke("DestroyNPC", 1.0f);
    }

    public void DoDamage()
    {
        target.GetComponent<CharackterController>().currentHealth -=  this.damage;
    }

    //mal nachschauen: how Boids work

    public void DestroyNPC()
    {
        Destroy(gameObject);
    }


}
