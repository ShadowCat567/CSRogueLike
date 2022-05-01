using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    CharacterController cc;
    float moveSpeed = 5.0f;
    float gravity = 9.8f;
    PlayerInput pi;

    Vector3 moveDir = Vector3.zero;
    float moveX;
    float moveY;

    float dashSpeed = 6.0f;
    float maxDashtime = 0.2f;

    float weaponRayCastDist = 2.5f;
    bool attack = false;
    float activated = 0.2f;

    int curHealth;
    int maxHealth = 3;
    [SerializeField] GameObject respawn;
    [SerializeField] GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        pi = GetComponent<PlayerInput>();
        curHealth = maxHealth;
        healthBar.GetComponent<PlayerHealthBar>().SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        healthBar.GetComponent<PlayerHealthBar>().UpdateHealthBar(curHealth);

        if(curHealth <= 0)
        {
            StartCoroutine(ResapwnPlayer());
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
                hitObj.collider.gameObject.GetComponent<EnemyBeh>().curEneHealth -= 1;
                Debug.Log(hitObj.collider.gameObject.GetComponent<EnemyBeh>().curEneHealth);
            }

            if(hitObj.collider.gameObject.tag == "EnemyPace")
            {
                hitObj.collider.gameObject.GetComponent<PacingEnemyBeh>().curEneHealth -= 1;
                Debug.Log(hitObj.collider.gameObject.GetComponent<PacingEnemyBeh>().curEneHealth);
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
        StartCoroutine(Dashing());
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

    IEnumerator ResapwnPlayer()
    {
        pi.enabled = false;
        //displays some kind of "you died" scene
        yield return new WaitForSeconds(1.0f);
        //respawn and reset
        transform.position = respawn.transform.position;
        curHealth = maxHealth;
        pi.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPace")
        {
            curHealth -= 1;
        }
    }
}
