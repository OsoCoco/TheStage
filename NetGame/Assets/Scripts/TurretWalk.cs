using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class TurretWalk : StateMachineBehaviour
{
    string[] states = { "Attack", "Spawn"};
    string actualState;

    Vector2 target;

    [SerializeField] float minX;
    [SerializeField] float maxX;

    [SerializeField] float minY;
    [SerializeField] float maxY;

    [SerializeField] float speed;


    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int r = Random.Range(0, states.Length);
        actualState = states[r];

        float x = Random.Range(minX,maxX);
        float y = Random.Range(minY,maxY);

        target = new Vector2(x, y);

        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RpcMove(animator);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(actualState);
    }

    

    [ClientRpc]
    void RpcFlip(Vector2 player, Transform enemy)
    {
        if (player.x < enemy.position.x)
            enemy.GetComponent<SpriteRenderer>().flipX = true;
        else
            enemy.GetComponent<SpriteRenderer>().flipX = false;
    }

  

    [ClientRpc]
    void RpcMove(Animator animator)
    {

            RpcFlip(target, animator.GetComponent<Transform>());

            Vector2 move = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

            rb.MovePosition(move);

            if (rb.position == target)
            {
                animator.SetTrigger(actualState);
            }
        
    }

}
