using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour
{
    [SyncVar]float health;
    public float movementSpeed;

    [SerializeField] Slider healthBar; 
    
    [SerializeField] float maxHealth;
    [SerializeField] float resitance;

    public float attackSpeed;
    public float attackDamage;
    public float attackRange;

    public float nextAttackTime;

    public bool isDead;

    Animator anim;

    private void Awake()
    {
        healthBar = GetComponentInChildren<Canvas>().GetComponentInChildren<Slider>();
        anim = GetComponent<Animator>();
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    
    [Command]
    public void CmdTakeDamage(float damage)
    {
        RpcTakeDamage(damage);
    }

    [ClientRpc]
     void RpcTakeDamage(float damage)
    {
        
       damage = DamageCalculation(damage);

       health -= damage;

      //anim.SetTrigger("isDamaged");

       if (health <= 0)
          {
            anim.SetBool("isDead", true);
            isDead = true;
            StartCoroutine(Deactivate());
           }
        
    }
    IEnumerator Deactivate()
    {
       //GetComponent<Movement>().enabled = false;

        yield return new WaitForSeconds(2);

        gameObject.SetActive(false);
    }
    float DamageCalculation(float damage)
    {
        float multiplier = 100 / (100 - resitance);

        damage *= multiplier;

        return damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void UpdateUI()
    {
        healthBar.value = health;

        if (health <= maxHealth / 2)
            healthBar.fillRect.GetComponent<Image>().color = new Color(255, 120, 0);
        
        if (health <= maxHealth / 4)
            healthBar.fillRect.GetComponent<Image>().color = Color.red;

    }

    private void LateUpdate()
    {
        UpdateUI();
    }
}
