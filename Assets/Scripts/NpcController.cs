using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public NPC npc;
    public SpriteRenderer sprite;

    public new Rigidbody2D rigidbody;

    public GameObject target;
    private Vector2 movementDirection;

    public int currentHealth;
    public int maxHealth;
    public int damage;
    public int level;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        InitNPC();
    }

    public void Update()
    {

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
    }

    public void DoDamage()
    {
        target.GetComponent<CharackterController>().currentHealth -=  this.damage;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("NPC"))
        {
            Debug.Log("1");
            if (collision.transform.position.y < gameObject.transform.position.y)
            {
                Debug.Log("2");
                rigidbody.AddForce(Vector2.up * 100f);
            }
            else
            {
                rigidbody.AddForce(Vector2.down * 100f);
            }
            if (collision.transform.position.x < gameObject.transform.position.x)
            {
                rigidbody.AddForce(Vector2.right * 100f);
            }
            else
            {
                rigidbody.AddForce(Vector2.left * 100f);
            }
        }
    }


    public void DestroyNPC()
    {
        Destroy(gameObject);
    }


}
