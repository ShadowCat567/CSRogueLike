using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeh : MonoBehaviour
{
    GameObject player;
    float moveVelo = 4.0f;
    Rigidbody rb;
    float rotSpeed = 3.0f;

    int enemyHealth = 3;
    public int curEneHealth;
    [SerializeField] GameObject Enemy;

    bool isChasing = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        curEneHealth = enemyHealth;
       // isChasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), rotSpeed * Time.deltaTime);
            transform.position += transform.forward * Time.deltaTime * moveVelo;
        }

        else
        {
            //pace back and forth, detect how close player is to start chasing
        }

        if(curEneHealth <= 0)
        {
            Enemy.SetActive(false);
            curEneHealth = enemyHealth;
        }
    }
}
