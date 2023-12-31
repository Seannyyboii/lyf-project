using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToOrigin : StateMachineBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;

    private Vector3 origin;
    private Rigidbody rigidbody;

    private BossSpawn bossSpawn;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossSpawn = FindObjectOfType<BossSpawn>();

        // Sets the Transform of the player GameObject
        origin = bossSpawn.originalPosition;

        // Sets the Rigidbody and Boss components from the boss GameObject
        rigidbody = animator.GetComponent<Rigidbody>();

        animator.speed = 1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Sets the target position as the player's position
        Vector3 target = new Vector3(origin.x, rigidbody.position.y, origin.z);

        // Sets the position where the boss moves towards the target position by a set speed
        Vector3 newPosition = Vector3.MoveTowards(rigidbody.position, target, speed * Time.deltaTime);
        rigidbody.MovePosition(newPosition);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // When the attack animation ends, reset the Attack trigger
        animator.ResetTrigger("Attack");
    }
}
