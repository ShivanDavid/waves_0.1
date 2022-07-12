using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public NPC npc;
    public SpriteRenderer sprite;

    public GameObject target;
    private Vector2 movementDirection;

    public int currentHealth;
    public int maxHealth;
    public int damage;
    public int level;

    private void Start()
    {
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

    public void DestroyNPC()
    {
        Destroy(gameObject);
    }


}
