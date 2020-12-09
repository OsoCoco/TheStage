using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class BossRun : StateMachineBehaviour
{
    GameObject[] players;
    Transform target;
    Rigidbody2D rb;

    [SerializeField] float speed;
    [SerializeField] float attackRange;

    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        rb = animator.GetComponent<Rigidbody2D>();

        if(players != null)
        {
            int i = Random.Range(0, players.Length );
            //Debug.Log(i);

            target = players[i].transform;     
        }

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RpcMove(animator);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

   

    [ClientRpc]
    void RpcFlip(Transform player,Transform enemy)
    {
        if (player.position.x < enemy.position.x)
            enemy.GetComponent<SpriteRenderer>().flipX = true;
        else
            enemy.GetComponent<SpriteRenderer>().flipX = false;
    }

   

    [ClientRpc]
    void RpcMove(Animator animator)
    {
        if (target != null)
        {

            RpcFlip(target, animator.GetComponent<Transform>());
            Vector2 targetVector = target.position;
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetVector, speed * Time.fixedDeltaTime);


            rb.MovePosition(newPos);

            if (Vector2.Distance(target.position, rb.position) <= attackRange)
            {
                animator.SetTrigger("Attack");
            }

        }
    }
}
