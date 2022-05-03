using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    CharacterController cc;
    float moveSpeed = 5.0f;
    float gravity = 9.8f;

    Vector3 moveDir = Vector3.zero;
    float moveX;
    float moveY;

    float dashSpeed = 6.0f;
    float maxDashtime = 0.2f;
    bool isInvincible = false;
    bool InvincTimerRunning = false;
    float invincTimer = 0.3f;

    float weaponRayCastDist = 2.5f;
    bool attack = false;
    float activated = 0.2f;

    int curHealth;
    public int maxHealth = 10;
    [SerializeField] GameObject healthBar;

    float hitTimer = 0.2f;
    Renderer rend;
    Color damageColor = new Color(0.83f, 0.02f, 0.02f);
    Color normalColor = new Color(0.96f, 0.96f, 0.96f);
    Color invincibleColor = new Color(0.59f, 0.94f, 1.0f);

    [SerializeField] GameObject weapon;
    Color normalWeaponColor = new Color(1.0f, 1.0f, 1.0f);
    Color attackWeaponColor = new Color(0.37f, 0.37f, 0.37f);

    public bool followPlayer;

    [SerializeField] GameObject exitPanel;

    AudioSource sounds;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip dashSound;
    [SerializeField] AudioClip EnemyHitSound;
    [SerializeField] AudioClip buffpickupSound;
    [SerializeField] AudioClip PotionPickupSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip damageFieldSound;

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponent<AudioSource>();
        exitPanel.SetActive(false);
        rend = GetComponent<Renderer>();
        cc = GetComponent<CharacterController>();
        curHealth = maxHealth;
        healthBar.GetComponent<PlayerHealthBar>().SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        healthBar.GetComponent<PlayerHealthBar>().UpdateHealthBar(curHealth);

        if(curHealth <= 0)
        {
            sounds.PlayOneShot(deathSound);
            SceneManager.LoadScene(1);   
        }

        if(InvincTimerRunning)
        {
            InvincibleTimer();
        }
    }

    void CheckEnemyHit()
    {
        //used this to help figure this out: https://answers.unity.com/questions/294285/casting-ray-forward-from-transform-position.html
        RaycastHit hitObj;

        //Debug.DrawRay(transform.position, transform.forward * weaponRayCastDist, Color.red);
        
        if(Physics.Raycast(transform.position, transform.forward, out hitObj, weaponRayCastDist))
        {
            if(hitObj.collider.gameObject.tag == "Enemy")
            {
                sounds.PlayOneShot(attackSound);
                hitObj.collider.gameObject.GetComponent<EnemyBeh>().curEneHealth -= 0.051f;
            }

            if(hitObj.collider.gameObject.tag == "EnemyPace")
            {
                sounds.PlayOneShot(attackSound);
                hitObj.collider.gameObject.GetComponent<PacingEnemyBeh>().curEneHealth -= 0.051f;
            }

            if(hitObj.collider.gameObject.tag == "Boss")
            {
                sounds.PlayOneShot(attackSound);
                hitObj.collider.gameObject.GetComponent<FinalBoss>().curHealth -= 1;
            }
        }
    }

    public void OnMove(InputValue input)
    {
        Vector2 inputVector = input.Get<Vector2>();

        moveX = inputVector.x;
        moveY = inputVector.y;
    }

    public void OnNormalAttack()
    {
        attack = true;
        StartCoroutine(WeaponFlash(activated));
    }

    public void OnDash()
    {
        InvincTimerRunning = true;
        sounds.PlayOneShot(dashSound);
        StartCoroutine(Dashing());
    }

    public void OnActivatePanel()
    {
        Time.timeScale = 0;
        exitPanel.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(HitFlash(hitTimer));
        curHealth -= damage;
    }

    void InvincibleTimer()
    {
        //this helped with parts this: https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
        isInvincible = true;
        rend.material.color = invincibleColor;

        if (InvincTimerRunning)
        {
            if (invincTimer > 0)
            {
                invincTimer -= Time.deltaTime;
            }
            else
            {
                isInvincible = false;
                rend.material.color = normalColor;
                invincTimer = 0.0f;
                InvincTimerRunning = false;
                invincTimer = 0.3f;
            }
        }
    }

    private void FixedUpdate()
    {
        if(attack)
        {
            CheckEnemyHit();
            StartCoroutine(TurnOffWeapon(activated));
        }

        if(cc.isGrounded)
        {
            moveDir = new Vector3(moveX, 0, moveY);

            moveDir *= moveSpeed;

            if(moveDir.magnitude > float.Epsilon)
            {
                transform.rotation = Quaternion.LookRotation(moveDir);
            }
        }

        moveDir.y -= gravity * Time.fixedDeltaTime;
        cc.Move(moveDir * Time.fixedDeltaTime);
    }

    IEnumerator Dashing()
    {
        //took inspiration from the answer to this question: https://forum.unity.com/threads/adding-a-dash-in-character-controller.878986/
        //gets the time at the frame when dashing starts
        float startDashTime = Time.time;

        //only allows the dash to happen for the time specified by maxDashTime
        while(Time.time < startDashTime + maxDashtime)
        {
            cc.Move(moveDir * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator TurnOffWeapon(float weaponActive)
    {
        yield return new WaitForSeconds(weaponActive);
        attack = false;
    }

    IEnumerator HitFlash(float hitTime)
    {
        rend.material.color = damageColor;
        yield return new WaitForSeconds(hitTime);
        rend.material.color = normalColor;
    }

    IEnumerator WeaponFlash(float hitTime)
    {
        weapon.GetComponent<Renderer>().material.color = attackWeaponColor;
        yield return new WaitForSeconds(hitTime);
        weapon.GetComponent<Renderer>().material.color = normalWeaponColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPace") && isInvincible == false)
        {
            StartCoroutine(HitFlash(hitTimer));
            sounds.PlayOneShot(EnemyHitSound);
            curHealth -= 1;
        }

        if(collision.gameObject.tag == "Boss" && isInvincible == false)
        {
            StartCoroutine(HitFlash(hitTimer));
            sounds.PlayOneShot(EnemyHitSound);
            curHealth -= 5;
        }

        if(collision.gameObject.tag == "DamageField" && isInvincible == false)
        {
            sounds.PlayOneShot(damageFieldSound);
            StartCoroutine(HitFlash(hitTimer));
            curHealth -= 3;
        }

        if (collision.gameObject.tag == "HealthBuff")
        {
            sounds.PlayOneShot(buffpickupSound);
            curHealth += collision.gameObject.GetComponent<HealthBuffs>().buffValue;
            maxHealth += collision.gameObject.GetComponent<HealthBuffs>().buffValue;
        }

        if(collision.gameObject.tag == "HealthPotion")
        {
            if (curHealth < maxHealth)
            {
                sounds.PlayOneShot(PotionPickupSound);
                curHealth += collision.gameObject.GetComponent<HealthPotion>().healthValue;
            }
        }
    }
}
