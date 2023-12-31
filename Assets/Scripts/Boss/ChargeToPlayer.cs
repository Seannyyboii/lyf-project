using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChargeToPlayer : StateMachineBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;

    private Transform player;
    private Rigidbody rigidbody;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Sets the Transform of the player GameObject
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;

        // Sets the Rigidbody and Boss components from the boss GameObject
        rigidbody = animator.GetComponent<Rigidbody>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Sets the target position as the player's position
        Vector3 target = new Vector3(player.position.x, rigidbody.position.y, player.position.z);

        // Sets the position where the boss moves towards the target position by a set speed
        Vector3 newPosition = Vector3.MoveTowards(rigidbody.position, target, speed * Time.deltaTime);
        rigidbody.MovePosition(newPosition);

        // If the distance between the boss and the player is lesser than the attackRange, play a random attack animation
        if (Vector3.Distance(player.position, rigidbody.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
            animator.SetInteger("AttackNum", Random.Range(0, 3));
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // When the attack animation ends, reset the Attack trigger
        animator.ResetTrigger("Attack");
    }
}
