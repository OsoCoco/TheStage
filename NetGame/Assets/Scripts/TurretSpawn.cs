using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class TurretSpawn : StateMachineBehaviour
{
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //SpawnEnemy.Instance.minionsPrefab = enemySpawn;
        animator.GetComponent<Enemy>().isInvulnerable = true;
        //waitingTime = Random.Range(1.5f, 2f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<MinionSpawn>().Spawn(animator.GetComponent<Transform>().position);
        animator.GetComponent<Enemy>().isInvulnerable = false;
    }


  
    

   
}
