using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "new NPC")]
public class NPC : ScriptableObject
{
    public SpriteRenderer sprite;

    public int maxHealth;
    public int damage;
    public int level;
 

    //Setter
    public void SetHealth(int health)
    {
        this.maxHealth = health;
    }
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    public void SetLevel(int level)
    {
        this.level = level;
    }

    //Getter
    public int GetHealth()
    {
        return this.maxHealth;
    }
    public int GetDamage()
    {
        return this.damage;
    }
    public int GetLevel()
    {
        return this.level;
    }
}
