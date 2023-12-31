using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BossController : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [HideInInspector] public int currentHealth;

    [Header("Adjustments")]
    public int maxHealth = 500;
    public float attackInterval;
    private float attackTimer;
    public float stunProjectileRotation;
    public float leftStunProjectileRotation;
    public float rightStunProjectileRotation;

    [Header("Attack Projectile Shooting Positions")]
    public Transform headPosition;
    public Transform leftPosition;
    public Transform rightPosition;

    [Header("Stun Projectile Shooting Positions")]
    public Transform stunPosition;
    public Transform leftStunPosition;
    public Transform rightStunPosition;

    [Header("Projectiles Being Shot")]
    public GameObject Projectile;
    public GameObject StunProjectile;
    public GameObject ShockCollider;
    public GameObject Smoke;

    public GameObject bossBarObject;
    private Animator animator;
    private SwipeManager swipeManager;

    private BossBar bossBar;
    private BossSpawn bossSpawn;
    private GameObject projectile, rightProjectile, leftProjectile, middleProjectile, shockCollider, smoke;
    public float lastTapTime, playerVerticalPosition, playerHorizontalPosition;

    public bool stunPlayer, playerJump;
    private GameObject debugText;
    private GameObject stunText, dodgeText;
    private GameManager gameManager;

    private Tutorial tutorial;

    private Vector3 origin, playerFoot, headFoot, playerRight, playerLeft;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        debugText = GameObject.Find("debugText");
        stunText = GameObject.Find("stunText");
        dodgeText = GameObject.Find("dodgeText");
        swipeManager = FindObjectOfType<SwipeManager>();
        gameManager = FindObjectOfType<GameManager>();

        player = GameObject.FindGameObjectWithTag("MainCamera");

        tutorial = FindObjectOfType<Tutorial>();
        tutorial.monster = animator;
        tutorial.boss = this;

        // Sets the BossBar and Boss Text GameObjects as bossBarObject and bossText variables respectively
        bossBarObject = FindObjectOfType<BossSpawn>().bossBar; 

        // Sets the BossBar of the bossBarObject as a bossBar variable
        bossBar = bossBarObject.GetComponent<BossBar>();

        // Sets the maximum amount of health the boss has and updates the boss bar UI element
        currentHealth = maxHealth;
        bossBar.SetMaxBossHealth(maxHealth);

        bossSpawn = FindObjectOfType<BossSpawn>();

        // Sets the Transform of the player GameObject
        origin = bossSpawn.originalPosition;

        playerFoot = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        headFoot = new Vector3(headPosition.position.x, transform.position.y, headPosition.position.z);
        playerRight = new Vector3(leftPosition.position.x, transform.position.y, player.transform.position.z);
        playerLeft = new Vector3(rightPosition.position.x, transform.position.y, player.transform.position.z);

        stunText.SetActive(false);
        dodgeText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        CheckState();
        CheckOrigin();
        CheckDeflect();
        FacePlayer();

        if (shockCollider != null || rightProjectile != null || leftProjectile != null || middleProjectile != null)
        {
            CheckStun();
        } 
    }

    public void FacePlayer()
    {
        if (gameObject.name == "Monster_Boss(Clone)" || gameObject.name == "Monster_Boss" || gameObject.name == "Monster_Boss 1(Clone)" || gameObject.name == "Monster_Boss 1" || gameObject.name == "Monster_Boss 2(Clone)" || gameObject.name == "Monster_Boss 2" )
        {
            Vector3 position = -((player.transform.position - transform.position).normalized);
            transform.right = (new Vector3(position.x, 0, position.z));
        }
        else
        {
            Vector3 position = -((player.transform.position - transform.position).normalized);
            transform.forward = -(new Vector3(position.x, 0, position.z));
        }
    }

    public void Vibrate()
    {
        Vibration.Vibrate();
    }

    void CheckOrigin()
    {
        if (transform.position.x >= origin.x && transform.position.z >= origin.z)
        {
            animator.SetBool("BackAtOrigin", true);
        }
        else
        {
            animator.SetBool("BackAtOrigin", false);
        }
    }

    void CheckState()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Charge Attack 1"))
        {
            if (SwipeManager.swipeDirection.ToString() == "Up" && !stunPlayer)
            {
                animator.Play("Charge Damage 3");
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Charge Attack 2"))
        {
            if (SwipeManager.swipeDirection.ToString() == "UpLeft" && !stunPlayer)
            {
                animator.Play("Charge Damage 2");
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Charge Attack 3"))
        {
            if (SwipeManager.swipeDirection.ToString() == "UpRight" && !stunPlayer)
            {
                animator.Play("Charge Damage 1");
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("HeavyStun"))
        {
            if (SwipeManager.swipeDirection.ToString() == "Up" && !stunPlayer)// && playerJump)
            {
                playerVerticalPosition = 0.8f;
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("LeftStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("RightStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("LeftRightStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("RightLeftStun"))
        {
            if (SwipeManager.swipeDirection.ToString() == "Left" && !stunPlayer)
            {
                playerHorizontalPosition = -1f;
            }

            if (SwipeManager.swipeDirection.ToString() == "Right" && !stunPlayer)
            {
                playerHorizontalPosition = 1f;
            }

            if (SwipeManager.swipeDirection.ToString() == "DownLeft" && !stunPlayer)
            {
                playerHorizontalPosition = -1f;
                playerVerticalPosition = -1f;
            }

            if (SwipeManager.swipeDirection.ToString() == "DownRight" && !stunPlayer)
            {
                playerHorizontalPosition = 1f;
                playerVerticalPosition = -1f;
            }
        }

        if (playerHorizontalPosition < 0)
        {
            playerHorizontalPosition += Time.deltaTime;
        }

        if (playerHorizontalPosition > 0)
        {
            playerHorizontalPosition -= Time.deltaTime;
        }

        if(playerHorizontalPosition < 0.01 && playerHorizontalPosition > -0.01)
        {
            playerHorizontalPosition = 0;
        }

        if (playerVerticalPosition <= 0)
        {
            playerVerticalPosition += Time.deltaTime;
        }

        if (playerVerticalPosition > 0)
        {
            playerVerticalPosition -= Time.deltaTime;
        }

        if (playerVerticalPosition < 0.01 && playerVerticalPosition > -0.01)
        {
            playerVerticalPosition = 0;
        }
    }

    void CheckDeflect()
    {
        if (projectile != null)
        {
            if (!stunPlayer)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    if (SwipeManager.swipeDirection.ToString() == "Down" && Vector3.Distance(projectile.transform.position, headPosition.transform.position) <= Vector3.Distance(player.transform.position, headPosition.transform.position))
                    {
                        player.GetComponentInParent<PlayerSFX>().Deflect();
                        projectile.GetComponent<SphereCollider>().enabled = true;
                        projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        projectile.transform.LookAt(this.transform);

                        projectile.GetComponent<Rigidbody>().AddForce(player.transform.forward * 7.5f, ForceMode.Impulse);
                    }
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    if (SwipeManager.swipeDirection.ToString() == "DownRight" && Vector3.Distance(projectile.transform.position, headPosition.transform.position) <= Vector3.Distance(player.transform.position, headPosition.transform.position))
                    {
                        player.GetComponentInParent<PlayerSFX>().Deflect();
                        projectile.GetComponent<SphereCollider>().enabled = true;
                        projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        projectile.transform.LookAt(this.transform);

                        projectile.GetComponent<Rigidbody>().AddForce(player.transform.forward * 7.5f, ForceMode.Impulse);
                    }
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    if (SwipeManager.swipeDirection.ToString() == "DownLeft" && Vector3.Distance(projectile.transform.position, headPosition.transform.position) <= Vector3.Distance(player.transform.position, headPosition.transform.position))
                    {
                        player.GetComponentInParent<PlayerSFX>().Deflect();
                        projectile.GetComponent<SphereCollider>().enabled = true;
                        projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        projectile.transform.LookAt(this.transform);

                        projectile.GetComponent<Rigidbody>().AddForce(player.transform.forward * 7.5f, ForceMode.Impulse);
                    }
                }
            }

            if (Vector3.Distance(projectile.transform.position, headPosition.transform.position) >= Vector3.Distance(player.transform.position, headPosition.transform.position))
            {
                DamagePlayer();
            }
        }
    }

    public void DamagePlayer()
    {
        player.GetComponentInParent<PlayerSFX>().Damage();
        gameManager.DamagePlayer(10);
        Destroy(projectile);
    }

    public void DamageEnemy(int damage)
    {
        animator.speed = 1;
        StartCoroutine(DamageEffect());

        currentHealth -= damage;
        bossBar.SetBossHealth(currentHealth);

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            StartCoroutine(tutorial.WinPopUp());
        }
    }

    IEnumerator DamageEffect()
    {
        Renderer mat = GetComponentInChildren<Renderer>();
        mat.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        mat.material.color = Color.white;
    }

    public void Headbutt()
    {
        projectile = Instantiate(Projectile, headPosition.position, Projectile.transform.rotation);

        projectile.transform.LookAt(player.transform);
        CheckOrientation(projectile);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce((player.transform.position - headPosition.position), ForceMode.Impulse);
    }

    public void LeftAttack()
    {
        projectile = Instantiate(Projectile, leftPosition.position, Projectile.transform.rotation);

        projectile.transform.LookAt(player.transform.position);
        CheckOrientation(projectile);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce((player.transform.position - headPosition.position), ForceMode.Impulse);
    }

    public void RightAttack()
    {
        projectile = Instantiate(Projectile, rightPosition.position, Projectile.transform.rotation);

        projectile.transform.LookAt(player.transform.position);
        CheckOrientation(projectile);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce((player.transform.position - headPosition.position), ForceMode.Impulse);
    }

    public void MiddleStun()
    {
        middleProjectile = Instantiate(StunProjectile, stunPosition.position, StunProjectile.transform.rotation);

        middleProjectile.transform.LookAt(player.transform);
        CheckOrientation(middleProjectile);

        Rigidbody rb = middleProjectile.GetComponent<Rigidbody>();
        rb.AddForce((playerFoot - headFoot), ForceMode.Impulse);
    }

    public void LeftStun()
    {
        leftProjectile = Instantiate(StunProjectile, leftStunPosition.position, StunProjectile.transform.rotation);

        leftProjectile.transform.LookAt(player.transform);
        leftProjectile.transform.Rotate(0, 0, leftStunProjectileRotation);

        Rigidbody rb = leftProjectile.GetComponent<Rigidbody>();
        rb.AddForce((playerFoot - headFoot), ForceMode.Impulse);
    }

    public void RightStun()
    {
        rightProjectile = Instantiate(StunProjectile, rightStunPosition.position, StunProjectile.transform.rotation);

        rightProjectile.transform.LookAt(player.transform);
        rightProjectile.transform.Rotate(0, 0, rightStunProjectileRotation);

        Rigidbody rb = rightProjectile.GetComponent<Rigidbody>();
        rb.AddForce((playerFoot - headFoot), ForceMode.Impulse);
    }

    public IEnumerator HeavyStun()
    {
        smoke = Instantiate(Smoke, transform.position, Smoke.transform.rotation);
        shockCollider = Instantiate(ShockCollider, headFoot, ShockCollider.transform.rotation);

        yield return new WaitForSeconds(0.02f);

        Rigidbody rb = shockCollider.GetComponent<Rigidbody>();
        rb.AddForce((playerFoot - headFoot) * 1.25f, ForceMode.Impulse);

        yield return new WaitForSeconds(2f);

        Destroy(smoke);
        Destroy(shockCollider);
    }

    public void CheckStun()
    {
        if (shockCollider != null)
        {
            if (Mathf.Round(Vector3.Distance(headFoot, playerFoot)) == Mathf.Round(Vector3.Distance(headFoot, shockCollider.transform.position)) && playerVerticalPosition <= 0)
            {
                StartCoroutine(StunPlayer());
            }

            else if (Mathf.Round(Vector3.Distance(headFoot, playerFoot)) == Mathf.Round(Vector3.Distance(headFoot, shockCollider.transform.position)) && playerVerticalPosition > 0)
            {
                StartCoroutine(PlayerDodge());
            }
        }

        if (middleProjectile != null)
        {
            Debug.Log("middle");
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("HeavyStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                if (Mathf.Round(Vector3.Distance(headPosition.position, player.transform.position)) == Mathf.Round(Vector3.Distance(headPosition.position, middleProjectile.transform.position)) && playerHorizontalPosition == 0)
                {
                    StartCoroutine(StunPlayer());
                    Destroy(middleProjectile);
                }

                else if (Mathf.Round(Vector3.Distance(headPosition.position, player.transform.position)) == Mathf.Round(Vector3.Distance(headPosition.position, middleProjectile.transform.position)) && playerHorizontalPosition < 0 || Mathf.Round(Vector3.Distance(headPosition.position, player.transform.position)) == Mathf.Round(Vector3.Distance(headPosition.position, middleProjectile.transform.position)) && playerHorizontalPosition > 0)
                {
                    StartCoroutine(PlayerDodge());
                    Destroy(middleProjectile);
                }
            }
        }

        if (leftProjectile != null)
        {
            if(leftStunProjectileRotation != 45)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("LeftStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("LeftRightStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("RightLeftStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    if (Mathf.Round(Vector3.Distance(leftPosition.position, playerRight)) == Mathf.Round(Vector3.Distance(leftPosition.position, leftProjectile.transform.position)) && playerHorizontalPosition >= 0)
                    {
                        StartCoroutine(StunPlayer());
                        Destroy(leftProjectile);
                    }

                    else if (Mathf.Round(Vector3.Distance(leftPosition.position, playerRight)) == Mathf.Round(Vector3.Distance(leftPosition.position, leftProjectile.transform.position)) && playerHorizontalPosition < 0)
                    {
                        StartCoroutine(PlayerDodge());
                        Destroy(leftProjectile);
                    }
                }
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("LeftStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("LeftRightStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("RightLeftStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    if (Mathf.Round(Vector3.Distance(headPosition.position, player.transform.position)) == Mathf.Round(Vector3.Distance(headPosition.position, leftProjectile.transform.position)) && playerHorizontalPosition <= 0)
                    {
                        StartCoroutine(StunPlayer());
                        Destroy(leftProjectile);
                    }

                    else if (Mathf.Round(Vector3.Distance(headPosition.position, player.transform.position)) == Mathf.Round(Vector3.Distance(headPosition.position, leftProjectile.transform.position)) && playerHorizontalPosition > 0 && playerVerticalPosition < 0)
                    {
                        StartCoroutine(PlayerDodge());
                        Destroy(leftProjectile);
                    }
                }
            }
        }

        if(rightProjectile != null)
        {
            if(rightStunProjectileRotation != -45)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("RightStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("LeftRightStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("RightLeftStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    if (Mathf.Round(Vector3.Distance(rightPosition.position, playerLeft)) == Mathf.Round(Vector3.Distance(rightPosition.position, rightProjectile.transform.position)) && playerHorizontalPosition <= 0)
                    {
                        StartCoroutine(StunPlayer());
                        Destroy(rightProjectile);
                    }

                    else if (Mathf.Round(Vector3.Distance(rightPosition.position, playerLeft)) == Mathf.Round(Vector3.Distance(rightPosition.position, rightProjectile.transform.position)) && playerHorizontalPosition > 0)
                    {
                        StartCoroutine(PlayerDodge());
                        Destroy(rightProjectile);
                    }
                }
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("RightStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("LeftRightStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("RightLeftStun") || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    if (Mathf.Round(Vector3.Distance(headPosition.position, player.transform.position)) == Mathf.Round(Vector3.Distance(headPosition.position, rightProjectile.transform.position)) && playerHorizontalPosition >= 0)
                    {
                        StartCoroutine(StunPlayer());
                        Destroy(rightProjectile);
                    }

                    else if (Mathf.Round(Vector3.Distance(headPosition.position, player.transform.position)) == Mathf.Round(Vector3.Distance(headPosition.position, rightProjectile.transform.position)) && playerHorizontalPosition < 0 && playerVerticalPosition < 0)
                    {
                        StartCoroutine(PlayerDodge());
                        Destroy(rightProjectile);
                    }
                }
            }
        }
    }

    IEnumerator StunPlayer()
    {
        stunPlayer = true;
        stunText.SetActive(true);
        stunText.GetComponent<TextMeshProUGUI>().text = "STUNNED";

        yield return new WaitForSeconds(3f);

        stunPlayer = false;
        stunText.SetActive(false);
    }

    IEnumerator PlayerDodge()
    {
        dodgeText.SetActive(true);
        dodgeText.GetComponent<TextMeshProUGUI>().text = "DODGE";

        yield return new WaitForSeconds(2f);

        dodgeText.SetActive(false);
    }

    void CheckOrientation(GameObject projectile)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2"))
        {
            projectile.transform.Rotate(0,0,-45);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3"))
        {
            projectile.transform.Rotate(0, 0, 45);
        }
    }

    void Attack()
    {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0)
        {
            float attackNum = Random.value;

            Debug.Log(attackNum);

            if (attackNum <= 0.25f)
            {
                animator.SetTrigger("Charge");
            }
            else if(attackNum <= 0.5f)
            {
                animator.SetTrigger("Stomp");
                animator.SetInteger("StompNum", Random.Range(0, 5));
            }
            else
            {
                animator.SetTrigger("Attack");
                animator.SetInteger("AttackNum", Random.Range(0, 3));
            }

            attackTimer = attackInterval;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == Projectile.name + "(Clone)")
        {
            int damageNum = Random.Range(0, 3);

            switch (damageNum)
            {
                case 0:
                    animator.Play("Deflect Damage 1");
                    break;

                case 1:
                    animator.Play("Deflect Damage 2");
                    break;

                case 2:
                    animator.Play("Deflect Damage 3");
                    break;
            }

            Destroy(other.gameObject);
        }
    }
}
