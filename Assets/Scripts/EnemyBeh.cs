using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeh : MonoBehaviour
{
    [SerializeField] GameObject player;
    float moveVelo = 4.0f;
    Rigidbody rb;
    float rotSpeed = 3.0f;

    int enemyHealth = 3;
    public int curEneHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        curEneHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), rotSpeed * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * moveVelo;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //collides with player's weapon, loses health
    }
}
