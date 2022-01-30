using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    private Transform player;
    public float walkSpeed = 2.5f;
    private Rigidbody rb;
    private bossScript boss;
    public float attackRange = 5f;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        boss = animator.GetComponent<bossScript>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookTowardsPlayer();
        
        Vector3 target = new Vector3(player.position.x, 0, player.position.z);
        Vector3 newPos = Vector3.MoveTowards(rb.position, target, walkSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector3.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("BasicAttack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("BasicAttack");
    }
}
