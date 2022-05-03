using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    [SerializeField] GameObject boss;

    [SerializeField] GameObject player;
    float moveVelo = 1.0f;
    float rotSpeed = 2.0f;
    float distToPlayer = 10.0f;

    float dashSpeed = 7.0f;
    float maxDashtime = 0.2f;
    bool dashed;
    float shouldDashDist = 13.0f;
    float burstDamageDist = 0.5f;
    int burst = 5;
    float postDashingTimer = 2.0f;
    bool postDashTimeRunning = false;

    public int curHealth;
    int maxHealth = 80;
    int healthThreshold = 30;
    [SerializeField] GameObject damageField;
    [SerializeField] GameObject healthBar;

    public bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        isAlive = true;
        damageField.SetActive(false);
        healthBar.SetActive(false);
        healthBar.GetComponent<PlayerHealthBar>().SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.GetComponent<PlayerHealthBar>().UpdateHealthBar(curHealth);

        if(Vector3.Distance(transform.position, player.transform.position) <= distToPlayer)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), rotSpeed * Time.deltaTime);
            transform.position += transform.forward * Time.deltaTime * moveVelo;
        }

        if(Vector3.Distance(transform.position, player.transform.position) >= shouldDashDist)
        {
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), rotSpeed * Time.deltaTime);
            Vector3 nextToPlayer = new Vector3(player.transform.position.x - 2, player.transform.position.y, player.transform.position.z - 2);
            Vector3.Lerp(transform.position, nextToPlayer, dashSpeed);
            postDashTimeRunning = true;
           // StartCoroutine(Dash());
        }

        if(postDashTimeRunning)
        {
            AfterDash();
        }

        if(Vector3.Distance(transform.position, player.transform.position) <= burstDamageDist && dashed)
        {
            player.GetComponent<PlayerMovement>().TakeDamage(burst);
        }

        if(curHealth <= healthThreshold && curHealth > 0)
        {
            damageField.SetActive(true);
        }

        if(curHealth <= 0)
        {
            isAlive = false;
            healthBar.SetActive(false);
            boss.SetActive(false);
        }

        if(boss.activeSelf)
        {
            healthBar.SetActive(true);
        }
    }

    void AfterDash()
    {
        dashed = true;

        if(postDashTimeRunning)
        {
            if(postDashingTimer > 0)
            {
                postDashingTimer -= Time.deltaTime;
            }

            else
            {
                dashed = false;
                postDashingTimer = 0.0f;
                postDashTimeRunning = false;
                postDashingTimer = 2.0f;
            }
        }
    }

    IEnumerator Dash()
    {
        moveVelo += dashSpeed;
        yield return new WaitForSeconds(maxDashtime);
        moveVelo -= dashSpeed;
        //float dashtime = Time.time;
        
        //while(Time.time < dashtime + maxDashtime)
        //{
        //    transform.Translate(transform.forward * dashSpeed * Time.deltaTime);
        //    yield return null;
       // }
    }
}
