using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Attack : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("Stomp", true);
        }
        else
        {
            animator.SetBool("Stomp", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetInteger("AttackNum", Random.Range(0,3));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            animator.SetTrigger("Death");
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            animator.SetTrigger("Damage");
            animator.SetInteger("DamageNum", Random.Range(0, 3));
        }
    }
}
