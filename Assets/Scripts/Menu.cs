using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator transitionAnimator;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            StartCoroutine(LoadIntro());
        }
    }

    IEnumerator LoadIntro()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("Teleport");
        transitionAnimator.SetBool("Transition", true);

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Intro");
    }
}
