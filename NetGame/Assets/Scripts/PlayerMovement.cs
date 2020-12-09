using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class PlayerMovement : NetworkBehaviour
{
    PlayerStats stats;
    Joystick joystick;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sp;

    NetworkAnimator animator;
    Button attackButton;
    // Start is called before the first frame update
    void Awake()
    {
        attackButton = GameObject.FindGameObjectWithTag("Attack").GetComponent<Button>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //animator = GetComponent<NetworkAnimator>();
        stats = GetComponent<PlayerStats>();

        joystick = GameObject.FindObjectOfType<Joystick>();
    }

    private void Start()
    {
        //animator.animator = anim;
        attackButton.onClick.AddListener(Attack);
    }


    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        Vector2 move = new Vector2(x, y);

        if (move.x != 0 || move.y != 0)
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);

        
            if (move.x < 0)
                CmdFlip(true);
            else if (move.x > 0)
                CmdFlip(false);
        
        transform.Translate(move * stats.movementSpeed * Time.deltaTime);
        
    }

    [Command]
    void CmdFlip(bool flip)
    {
        RpcFlip(flip);
    }
    [ClientRpc]
    void RpcFlip(bool flip)
    {
        sp.flipX = flip;
    }

    //[ClientRpc]
    void Attack()
    {
        if (!isLocalPlayer) return;

        CmdAttack();

    }
    [ClientRpc]
    void RpcAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, stats.attackRange);

        anim.SetTrigger("Attack");
        foreach (Collider2D enemie in enemies)
        {
            if (enemie.CompareTag("Enemy"))
            {
                enemie.GetComponent<Enemy>().RpcTakeDamage(stats.attackDamage);
            }
        }
    }
    [Command]
    void CmdAttack()
    {
        RpcAttack();
    }
}
