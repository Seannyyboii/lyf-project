using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //public float playerHealth;
    public int currentHealth;
    public int maxHealth = 100;

    public GameObject player;
    public GameObject playerBarObject;
    private PlayerBar playerBar;

    public bool canRegenerate;
    public TextMeshProUGUI debugInfo;

    public GameObject damage;

    public Tutorial tutorial;

    public string battleMusic;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play(battleMusic);

        currentHealth = maxHealth;
        playerBar = playerBarObject.GetComponent<PlayerBar>();
        playerBar.SetMaxPlayerHealth(maxHealth);
    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;
        playerBar.SetPlayerHealth(currentHealth);
        StartCoroutine(DamageEffect());

        Debug.Log("Damage taken");

        if (currentHealth <= 0)
        {
            StartCoroutine(tutorial.LosePopUp());
        }
    }

    IEnumerator DamageEffect()
    {
        damage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        damage.SetActive(false);
    }
}
