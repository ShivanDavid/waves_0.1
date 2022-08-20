using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public Transform pfDamagePopUp;
    public TMP_Text dmgPopUpPlayer;

    public int factor = -50;
    bool execute = true;

    //public int critBonus; //critbonus für den der angreift

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

    public bool CritCalculater(int rawDmgAmount, int dmgAmountMax, int critBonus)
    {
        int halfAmount = dmgAmountMax / 2;

        bool isCriticalHit = Random.Range(0, rawDmgAmount + critBonus) >= halfAmount && (rawDmgAmount > halfAmount);

        return isCriticalHit;
    }


    public int DamageCalculater(bool isCriticalHit, int rawAmountMax)
    {
        if (isCriticalHit)
        {
            rawAmountMax *= 2;
        }
       
        return rawAmountMax;
    }

    public void CreateDamagePopUps(int editDmgAmount, bool isCriticalHit)
    {
        Vector2 lookDirection = gameObject.GetComponent<Agent>().GetLookDirection();

        factor = 0;
        factor += Random.Range(-60, 60);

        if (gameObject.CompareTag("Enemy"))
        {
            DamagePopUp.Create(healthbar.transform.position, editDmgAmount, pfDamagePopUp, isCriticalHit, lookDirection);
        }
        else if (gameObject.CompareTag("Player"))
        {
            DamagePopUp.CreateForPlayer(healthbar.transform.position, editDmgAmount, dmgPopUpPlayer, isCriticalHit, factor);
        }
        else
        {
            //DamagePopUp.Create(healthbar.transform.position, editDmgAmount, pfDamagePopUp, isCriticalHit, lookDirection);
            Debug.Log("another npc");
        }
    }

    public void GetHit(int dmgAmountMax, GameObject sender, int critBonus)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        //
        int rawDmgAmount = Random.Range(1, dmgAmountMax);

        //Dmg + crit calculation
        bool isCriticalHit = CritCalculater(rawDmgAmount, dmgAmountMax, critBonus);
        int editDmgAmount = DamageCalculater(isCriticalHit, rawDmgAmount);
        
        //
        CreateDamagePopUps(editDmgAmount, isCriticalHit);

        ////for DamagePopUp
        //Vector2 lookDirection = gameObject.GetComponent<Agent>().GetLookDirection();

        //int halfAmount = amountMax / 2;

        //int amount = Random.Range(1, amountMax);
        //bool isCriticalHit = Random.Range(0, amount + critBonus)  >= halfAmount && (amount > halfAmount);
        //if (isCriticalHit )
        //{
        //    Debug.Log(amount);
        //    amount *= 2;
        //}
        //currentHealth -= amount; //DamageAmount on Object
        //if (gameObject.CompareTag("Enemy"))
        //{
        //    DamagePopUp.Create(healthbar.transform.position, amount, pfDamagePopUp, isCriticalHit, lookDirection);
        //}
        //else if (gameObject.CompareTag("Player"))
        //{
        //    DamagePopUp.CreateForPlayer(healthbar.transform.position, amount, dmgPopUpPlayer, isCriticalHit);
        //}

        currentHealth -= editDmgAmount; //DamageAmount on Object

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
