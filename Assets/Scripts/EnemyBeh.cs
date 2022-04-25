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
    float distToPlayer = 4.0f;
    [SerializeField] GameObject pacingObj1;
    [SerializeField] GameObject pacingObj2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        curEneHealth = enemyHealth;
        isChasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) <= distToPlayer)
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
            //pace back and forth, detect how close player is to start chasing
            //Vector3.Lerp(pacingObj1.transform.position, pacingObj2.transform.position, )
        }

        if(curEneHealth <= 0)
        {
            Enemy.SetActive(false);
            curEneHealth = enemyHealth;
        }
    }
}
