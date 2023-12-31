using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject healthBars;
    public GameObject tutorialDialogue;
    public GameObject hints;

    public GameObject winPopUp;
    public GameObject winDarken;

    public GameObject losePopUp;
    public GameObject loseDarken;

    public GameObject pausePopUp;
    public GameObject pauseDarken;
    
    public Animator monster;
    public BossController boss;
    public GameManager player;

    private bool paused;
    public GameObject fadeIn;
    public GameObject fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HideFade());
    }

    // Update is called once per frame
    void Update()
    {
        if (monster.GetCurrentAnimatorStateInfo(0).IsName("Idle") && tutorialDialogue.GetComponent<Dialogue>().index != tutorialDialogue.GetComponent<Dialogue>().lines.Length - 1)
        {
            tutorialDialogue.SetActive(true);
            monster.enabled = false;
        }
        
        else if (tutorialDialogue.GetComponent<Dialogue>().index == tutorialDialogue.GetComponent<Dialogue>().lines.Length - 1)
        {
            healthBars.SetActive(true);
            tutorialDialogue.SetActive(false);
            monster.enabled = true;
        }

        if(hints.GetComponent<Dialogue>().index == hints.GetComponent<Dialogue>().lines.Length - 1)
        {
            healthBars.SetActive(true);
            hints.SetActive(false);
            monster.enabled = true;
        }

        if(player.currentHealth <= 0)
        {
            monster.enabled = false;
        }

        if (hints.activeSelf)
        {
            monster.enabled = false;
            healthBars.SetActive(false);
        }

        if (paused)
        {
            monster.enabled = false;
        }
    }

    public IEnumerator WinPopUp()
    {
        yield return new WaitForSeconds(2f);

        winDarken.SetActive(true);

        yield return new WaitForSeconds(2f);

        winPopUp.SetActive(true);

        AddPoints(250);
    }

    public IEnumerator LosePopUp()
    {
        loseDarken.SetActive(true);

        yield return new WaitForSeconds(2f);

        losePopUp.SetActive(true);

        AddPoints(50);
    }

    public IEnumerator PausePopUp()
    {
        monster.enabled = false;
        pauseDarken.SetActive(true);

        yield return new WaitForSeconds(1f);

        pausePopUp.SetActive(true);
    }

    public IEnumerator HidePausePopUp()
    {
        pausePopUp.GetComponent<Animator>().SetBool("Close", true);
        pauseDarken.GetComponent<Animator>().SetBool("Disappear", true);

        yield return new WaitForSeconds(.46f);

        pausePopUp.SetActive(false);

        yield return new WaitForSeconds(1f);

        pauseDarken.SetActive(false);

        monster.enabled = true;
    }

    public void PauseGame()
    {
        paused = true;
        StartCoroutine(PausePopUp());
    }

    public void ResumeGame()
    {
        paused = false;
        StartCoroutine(HidePausePopUp());
    }

    public void Hint()
    {
        healthBars.SetActive(false);
        hints.SetActive(true);

        hints.GetComponent<Dialogue>().index = 0;
    }

    public void RestartGame()
    {
        StartCoroutine(Restart());
    }

    public void MainMenu()
    {
        StartCoroutine(Menu());
    }

    public IEnumerator ShowHint()
    {
        healthBars.SetActive(false);
        hints.SetActive(true);

        yield return new WaitForSeconds(.5f);

        hints.GetComponent<Dialogue>().index = 0;
    }

    public IEnumerator HideFade()
    {
        yield return new WaitForSeconds(2f);
        fadeIn.SetActive(false);
    }

    public IEnumerator Restart()
    {
        fadeOut.SetActive(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator Menu()
    {
        fadeOut.SetActive(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("QRCodeScanner");
    }

    public void AddPoints(int point)
    {
        // Increments the scoreScriptableObject's score value by the number of points passed into the method and sets the points text to the scoreScriptableObject value
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + point);
    }
}