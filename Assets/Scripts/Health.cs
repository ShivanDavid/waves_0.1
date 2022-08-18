using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public Transform pfDamagePopUp;
    public TMP_Text dmgPopUpPlayer;

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
        if (gameObject.CompareTag("Enemy"))
        {
            DamagePopUp.Create(healthbar.transform.position, amount, pfDamagePopUp, isCriticalHit, lookDirection);
        }
        else if (gameObject.CompareTag("Player"))
        {
            DamagePopUp.CreateForPlayer(healthbar.transform.position, amount, dmgPopUpPlayer, isCriticalHit, lookDirection);
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

        ////Debug.Log(GameObject.FindGameObjectWithTag("pfDamagePopUp").GetComponent<DamagePopUp>().GetDmgAmount().ToString());
        //if(dmgPopUpPlayer)
        //{
        //    Debug.Log("nice");

        //    string dmgAmount = GameObject.FindGameObjectWithTag("pfDamagePopUp").
        //        GetComponent<DamagePopUp>().GetDmgAmount().ToString();

        //    dmgPopUpPlayer.SetText("-" + dmgAmount);
        //}
        //else
        //{
        //    Debug.Log("wrong");
        //}
        
    }

    private IEnumerator Flash()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
