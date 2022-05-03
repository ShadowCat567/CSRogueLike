using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacingEnemyBeh : MonoBehaviour
{
    //variables related to movement
    GameObject player;
    float moveVelo = 4.0f;
    Rigidbody rb;
    float rotSpeed = 3.0f;

    //variables related to the enemy's health
    float enemyHealth = 0.15f;
    public float curEneHealth;
    [SerializeField] GameObject Enemy;

    //variables related to the enemy's ability to pace
    bool isChasing = true;
    float distToPlayer = 4.0f;
    [SerializeField] float amp = 4.0f;
    float elapsedTime = 0.0f;
    Vector3 startingPos;
    float multi = 1.0f;

    //tells what direction they should be moving in
    [SerializeField] bool movingInXDir;

    //makes sure enemy knows about their healthBar and the room it is in
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject room;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //finds the player
        player = GameObject.Find("Player");
        curEneHealth = enemyHealth;
        //not chasing player to start out with
        isChasing = false;
        //sets its starting position
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //updates the enemy's health bar
        healthBar.GetComponent<EnemyHealthBar>().UpdateEnemyHealth(curEneHealth);

        //if the enemy is close to the player, start chasing the player
        if (Vector3.Distance(transform.position, player.transform.position) <= distToPlayer)
        {
            isChasing = true;
        }

        //chase the player when the enemy is close to them
        if (isChasing)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), rotSpeed * Time.deltaTime);
            transform.position += transform.forward * Time.deltaTime * moveVelo;
        }

        //if the player is not nearby, pace back and forth
        else
        {
            elapsedTime += Time.deltaTime;

            //paces back and forth in the x direction
            if (movingInXDir)
            {
                Vector3 offset = new Vector3(amp * Mathf.Sin(elapsedTime) * multi, 0.0f, 0.0f);
                transform.position = startingPos + offset;
            }

            //paces back and forth in the y direction
            else
            {
                Vector3 offset = new Vector3(0.0f, 0.0f, amp * Mathf.Sin(elapsedTime) * multi);
                transform.position = startingPos + offset;
            }
        }

        //if enemy health is at or below 0, kill the enemy
        if (curEneHealth <= 0)
        {
            Enemy.SetActive(false);
            room.GetComponent<PositionsInRoom>().enemiesKilled += 1;
            curEneHealth = enemyHealth;
        }
    }
}
