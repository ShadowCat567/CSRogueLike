using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacingEnemyBeh : MonoBehaviour
{
    GameObject player;
    float moveVelo = 4.0f;
    Rigidbody rb;
    float rotSpeed = 3.0f;

    float enemyHealth = 0.15f;
    public float curEneHealth;
    [SerializeField] GameObject Enemy;

    bool isChasing = true;
    float distToPlayer = 4.0f;
    [SerializeField] float amp = 4.0f;
    float elapsedTime = 0.0f;
    Vector3 startingPos;
    float multi = 1.0f;

    [SerializeField] bool movingInXDir;

    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject room;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        curEneHealth = enemyHealth;
        isChasing = false;
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.GetComponent<EnemyHealthBar>().UpdateEnemyHealth(curEneHealth);

        if (Vector3.Distance(transform.position, player.transform.position) <= distToPlayer)
        {
            isChasing = true;
        }

        if (isChasing)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), rotSpeed * Time.deltaTime);
            transform.position += transform.forward * Time.deltaTime * moveVelo;
        }

        else
        {
            elapsedTime += Time.deltaTime;

            if (movingInXDir)
            {
                Vector3 offset = new Vector3(amp * Mathf.Sin(elapsedTime) * multi, 0.0f, 0.0f);
                transform.position = startingPos + offset;
            }

            else
            {
                Vector3 offset = new Vector3(0.0f, 0.0f, amp * Mathf.Sin(elapsedTime) * multi);
                transform.position = startingPos + offset;
            }
        }

        if (curEneHealth <= 0)
        {
            Enemy.SetActive(false);
            room.GetComponent<PositionsInRoom>().enemiesKilled += 1;
            curEneHealth = enemyHealth;
        }
    }
}
