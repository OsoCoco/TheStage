using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Enemy : NetworkBehaviour
{
    [SyncVar]float health;
    public float movementSpeed;

    [SerializeField] float maxHealth;
    [SerializeField] float resitance;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackRange;
    [SerializeField] float attackDamage;
    [SerializeField] Slider healthBar;

    [SerializeField] bool isBoos;

    [SerializeField] GameObject playerWin;

   // NetworkAnimator animator;

    public bool isInvulnerable;

    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        
        anim = GetComponent<Animator>();
     //   animator = GetComponent<NetworkAnimator>();

        health = maxHealth;
        
        healthBar = GetComponentInChildren<Canvas>().GetComponentInChildren<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    private void Start()
    {
        //animator.animator = anim;
    }


    public void Attack()
    {
        CmdAttack();
    }

    [Command]
    void CmdAttack()
    {
        RpcAttack();
    }
    [ClientRpc]
    void RpcAttack()
    {
       // Debug.Log("Attack");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D collider in colliders)
        {

            if(collider.CompareTag("Player"))
            {
                collider.GetComponent<PlayerStats>().CmdTakeDamage(attackDamage);
            }
        }
    }
    
   

    [ClientRpc]
     public void RpcTakeDamage(float damage)
    {
        
            if (isInvulnerable)
                return;

            damage = DamageCalculation(damage);

            health -= damage;

            //anim.SetTrigger("isDamaged");

            if (health <= 0)
            {
                anim.SetBool("isDead", true);

                StartCoroutine(Deactivate());
                //GetComponent<Movement>().enabled = false;
                Debug.Log("Has Died");
            }
        
    }


    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    float DamageCalculation(float damage)
    {
        float multiplier = 100 / (100 - resitance);

        damage *= multiplier;

        return damage;
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

    private void OnDisable()
    {
        if(isBoos)
        {
            Time.timeScale = 0;
           /* if(!playerWin.activeSelf)
                playerWin.SetActive(true);*/
        }
    }
}

