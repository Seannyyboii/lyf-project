using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    private bool canTap;
    public bool canSkip;

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    public GameObject darken;
    public GameObject[] popUp;
    public Animator chicken;
    public Animator monster;

    public int[] showPopUpIndex;
    public int[] hidePopUpIndex;

    [HideInInspector]
    public int index;

    public float lastTapTime;

    // Start is called before the first frame update
    void Start()
    {
        canTap = true;
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSkip)
        {
            CheckSkip();
        }
        
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && canTap)
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }

            CheckIndex();

            if (index == 11)
            {
                if(chicken != null)
                {
                    chicken.SetBool("Load", true);
                }
            }
        }
    }

    void CheckIndex()
    {
        for (int i = 0; i < showPopUpIndex.Length; i++)
        {
            if (index == showPopUpIndex[i])
            {
                StartCoroutine(ShowPopUp(popUp[i]));
            }

            if (index == hidePopUpIndex[i])
            {
                StartCoroutine(HidePopUp(popUp[i]));
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char character in lines[index].ToCharArray())
        {
            textComponent.text += character;

            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator ShowPopUp(GameObject popUp)
    {
        canTap = false;
        darken.SetActive(true);

        yield return new WaitForSeconds(2f);

        popUp.SetActive(true);
        canTap = true;
    }

    IEnumerator HidePopUp(GameObject popUp)
    {
        canTap = false;
        popUp.GetComponent<Animator>().SetBool("Close", true);
        darken.GetComponent<Animator>().SetBool("Disappear", true);

        yield return new WaitForSeconds(.46f);

        popUp.SetActive(false);

        yield return new WaitForSeconds(1);

        darken.SetActive(false);
        canTap = true;
    }

    void CheckSkip()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            float timeSinceLastTap = Time.time - lastTapTime;

            if (timeSinceLastTap <= 0.2f)
            {
                index = lines.Length - 1;
                if (darken.activeSelf)
                {
                    darken.SetActive(false);
                }

                for(int i = 0; i < popUp.Length; i++)
                {
                    if (popUp[i].activeSelf)
                    {
                        popUp[i].SetActive(false);
                    }
                }
            }
            lastTapTime = Time.time;
        }
    }
}
