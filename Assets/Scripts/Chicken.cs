using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chicken : MonoBehaviour
{
    private Animator animator;
    public Animator swipe;
    public GameObject dialogue;
    public float lastTapTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        FindObjectOfType<AudioManager>().Stop("Start");
        FindObjectOfType<AudioManager>().Play("Intro");
    }

    // Update is called once per frame
    void Update()
    {
        CheckSkip();

        if (swipe.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
        {
            animator.enabled = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            dialogue.SetActive(true);
        }
    }

    public IEnumerator LoadScanner()
    {
        swipe.SetBool("Fade", true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("QRCodeScanner");
    }

    void CheckSkip()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            float timeSinceLastTap = Time.time - lastTapTime;

            if (timeSinceLastTap <= 0.2f)
            {
                StartCoroutine(LoadScanner());
            }
            lastTapTime = Time.time;
        }
    }
}
