using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_behaviourS2 : StateMachineBehaviour
{
    private Rigidbody rb;
    private bossScript boss;
    private Transform player;
    public float attackRange = 2f;
    private bool hasRun;

    public int attackVar; 
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        boss = animator.GetComponent<bossScript>();
        hasRun = false;
    }

// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("BasicAttack");
        }
        else
        {
            if (!hasRun)
            {
                Debug.Log("IS CALLEDED");
                attackVar = boss.DecidePattern();
                if (attackVar == 0)
                {
                    animator.SetTrigger("JumpAttack");
                    //boss.Sweep();
                }
                else if (attackVar == 1)
                {
                    animator.SetTrigger("ChargeAtt");
                    //boss.Maintain();
                }
                hasRun = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("JumpAttack");
        animator.ResetTrigger("ChargeAtt");
    }
    
}
