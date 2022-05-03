using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    //makes sure the boss knows about themself
    [SerializeField] GameObject boss;

    //variables related to movement
    [SerializeField] GameObject player;
    float moveVelo = 1.0f;
    float rotSpeed = 2.0f;
    float distToPlayer = 10.0f;

    //variables related to dashing
    float dashSpeed = 7.0f;
    float maxDashtime = 0.2f;
    bool dashed;
    float shouldDashDist = 13.0f;
    float burstDamageDist = 0.5f;
    int burst = 5;
    float postDashingTimer = 2.0f;
    bool postDashTimeRunning = false;

    //variables related to health
    public int curHealth;
    int maxHealth = 80;
    //threshold for when the damage field appears
    int healthThreshold = 30;
    [SerializeField] GameObject damageField;
    [SerializeField] GameObject healthBar;

    //is the boss alive?
    public bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        //sets the boss's current health
        curHealth = maxHealth;
        isAlive = true;
        damageField.SetActive(false);
        healthBar.SetActive(false);
        //sets up the health bar
        healthBar.GetComponent<PlayerHealthBar>().SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //updates the health bar
        healthBar.GetComponent<PlayerHealthBar>().UpdateHealthBar(curHealth);

        //if the player is nearby, follow it
        if(Vector3.Distance(transform.position, player.transform.position) <= distToPlayer)
        {
            //turns to look in the direction of the player, then follows the player
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), rotSpeed * Time.deltaTime);
            transform.position += transform.forward * Time.deltaTime * moveVelo;
        }

        //checks if the player is in dash range and dashes to the player...I was not able to get this working very well
        if(Vector3.Distance(transform.position, player.transform.position) >= shouldDashDist)
        {
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), rotSpeed * Time.deltaTime);
            Vector3 nextToPlayer = new Vector3(player.transform.position.x - 2, player.transform.position.y, player.transform.position.z - 2);
            Vector3.Lerp(transform.position, nextToPlayer, dashSpeed);
            postDashTimeRunning = true;
           // StartCoroutine(Dash());
        }

        //period of time after dashing when stuff related to the dash occurs
        if(postDashTimeRunning)
        {
            AfterDash();
        }

        //once the boss makes it to the player after dashing, deals a burst of damage
        if(Vector3.Distance(transform.position, player.transform.position) <= burstDamageDist && dashed)
        {
            player.GetComponent<PlayerMovement>().TakeDamage(burst);
        }

        //if boss is low health, sets the damage field to active
        if(curHealth <= healthThreshold && curHealth > 0)
        {
            damageField.SetActive(true);
        }

        //kills the boss if they are at or below 0 hp
        if(curHealth <= 0)
        {
            isAlive = false;
            healthBar.SetActive(false);
            boss.SetActive(false);
        }

        //if the boss if active, set their health bar to active as well 
        if(boss.activeSelf)
        {
            healthBar.SetActive(true);
        }
    }

    void AfterDash()
    {
        //countdown timer related to the dashed variable that would make stuff happen after the boss dashed to the player
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

    //this was originally going to be the dash, based off the player's dash
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
