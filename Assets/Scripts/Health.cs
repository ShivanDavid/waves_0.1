using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public Transform pfDamagePopUp;

    [SerializeField]
    private int currentHealth, maxHealth;

    public Healthbar healthbar;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    public bool isDead = false;

    private AgentAnimations agentAnimations;

    public void Start()
    {
        InitializeHealth(maxHealth);
    }

    private void Awake()
    {
        agentAnimations = GetComponentInChildren<AgentAnimations>();
    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;

        healthbar.SetMaxHealth(maxHealth);

        isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        //DamagePopUp
        Vector2 lookDirection = gameObject.GetComponent<Agent>().GetLookDirection();
        amount = Random.Range(1, amount);
        bool isCriticalHit = Random.Range(0, amount) > (amount / 2);
        if (isCriticalHit)
        {
            amount *= 2;
        }
        currentHealth -= amount; //DamageAmount on Object
        if (!gameObject.CompareTag("Player"))
        {
            DamagePopUp.Create(healthbar.transform.position, amount, pfDamagePopUp, isCriticalHit, lookDirection);
        }

        healthbar.SetHealth(currentHealth);
        StartCoroutine(Flash());

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            agentAnimations.DieAnimation(isDead);
        }
    }

    private IEnumerator Flash()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
