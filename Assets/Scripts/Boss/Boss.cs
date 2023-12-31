using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private GameObject player;
    private Transform playerPosition;
    [SerializeField] private Animator animator;
    private float speedTimer;
    public float speedInterval;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the player and blades GameObjects
        player = GameObject.FindGameObjectWithTag("MainCamera");

        // Sets the playerPosition as the player GameObject's transform
        playerPosition = player.transform;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Roar"))
        //{
        //    RandomSpeed();
        //}
        //else
        //{
        //    animator.speed = 1;
        //}
    }


    void RandomSpeed()
    {
        speedTimer -= Time.deltaTime;

        if (speedTimer <= 0)
        {
            int attackNum = Random.Range(0, 3);

            switch (attackNum)
            {
                case 0:
                    animator.speed = 1;
                    break;

                case 1:
                    animator.speed = 2;
                    break;

                case 2:
                    animator.speed = 3;
                    break;
            }

            speedTimer = speedInterval;
        }
    }
    public void LookAtPlayer()
    {
        transform.LookAt(playerPosition);
    }
}
