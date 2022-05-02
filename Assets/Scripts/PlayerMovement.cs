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

    float weaponRayCastDist = 3.0f;
    bool attack = false;
    float activated = 0.2f;

    int curHealth;
    public int maxHealth = 18;
    [SerializeField] GameObject healthBar;

    float hitTimer = 0.2f;
    Renderer rend;
    Color damageColor = new Color(0.83f, 0.02f, 0.02f);
    Color normalColor = new Color(0.96f, 0.96f, 0.96f);

    // Start is called before the first frame update
    void Start()
    {
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
            SceneManager.LoadScene(0);   
        }
    }

    void CheckEnemyHit()
    {
        //used this to help figure this out: https://answers.unity.com/questions/294285/casting-ray-forward-from-transform-position.html
        RaycastHit hitObj;

        Debug.DrawRay(transform.position, transform.forward * weaponRayCastDist, Color.red);
        
        if(Physics.Raycast(transform.position, transform.forward, out hitObj, weaponRayCastDist))
        {
            if(hitObj.collider.gameObject.tag == "Enemy")
            {
                hitObj.collider.gameObject.GetComponent<EnemyBeh>().curEneHealth -= 0.051f;
               // Debug.Log(hitObj.collider.gameObject.GetComponent<EnemyBeh>().curEneHealth);
            }

            if(hitObj.collider.gameObject.tag == "EnemyPace")
            {
                hitObj.collider.gameObject.GetComponent<PacingEnemyBeh>().curEneHealth -= 0.051f;
               // Debug.Log(hitObj.collider.gameObject.GetComponent<PacingEnemyBeh>().curEneHealth);
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
    }

    public void OnDash()
    {
        InvincibleTimer();
        StartCoroutine(Dashing());
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(HitFlash(hitTimer));
        curHealth -= damage;
    }

    void InvincibleTimer()
    {
        //look into this: https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
        float timer = 1.5f;

        if(timer > 0)
        {
            Debug.Log("invincible");
            timer -= Time.deltaTime;
            isInvincible = true;
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

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPace") && isInvincible == false)
        {
            StartCoroutine(HitFlash(hitTimer));
            curHealth -= 1;
        }

        if(collision.gameObject.tag == "HealthBuff")
        {
            curHealth += collision.gameObject.GetComponent<HealthBuffs>().buffValue;
            maxHealth += collision.gameObject.GetComponent<HealthBuffs>().buffValue;
        }

        if(collision.gameObject.tag == "HealthPotion")
        {
            if (curHealth < maxHealth)
            {
                curHealth += collision.gameObject.GetComponent<HealthPotion>().healthValue;
            }
        }
    }
}
