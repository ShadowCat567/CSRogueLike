using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeh : MonoBehaviour
{
    //variables related to movement
    GameObject player;
    float moveVelo = 4.0f;
    Rigidbody rb;
    float rotSpeed = 3.0f;

    //variables related to enemy health
    float enemyHealth = 0.15f;
    public float curEneHealth;
    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject healthBar;

    //variables related to whether enemy is chasing player
    bool isChasing = true;
    float distToPlayer = 4.0f;


    //variables related to the direction the enemy is moving in
    enum direction { left, right, forwards, backwards }
    direction dirToMove;
    float velocity = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        curEneHealth = enemyHealth;
        isChasing = false;
        //chooses the direction it moves in
        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //updates the healthBar to match current enemy health
        healthBar.GetComponent<EnemyHealthBar>().UpdateEnemyHealth(curEneHealth);

        if (Vector3.Distance(transform.position, player.transform.position) <= distToPlayer)
        {
            //if the player is close to the enemy, start chasing player
            isChasing = true;
        }

        if (isChasing)
        {
            //look at player
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), rotSpeed * Time.deltaTime);
            //chase player
            transform.position += transform.forward * Time.deltaTime * moveVelo;
        }

        else
        {
            //move in the direction it randomly chooses until it gets close to the player
            MoveInDirection();
        }

        //health is at or below 0, kill the enemy
        if(curEneHealth <= 0)
        {
            Enemy.SetActive(false);
            curEneHealth = enemyHealth;
        }
    }


    void ChooseDirection()
    {
        //choose direction to move in
        dirToMove = (direction)Random.Range(0, 4);
    }

    void MoveInDirection()
    {
        //move in the selected direction
        switch(dirToMove)
        {
            case direction.left:
                transform.Translate(Vector3.left * Time.deltaTime * velocity);
                break;

            case direction.right:
                transform.Translate(Vector3.right * Time.deltaTime * velocity);
                break;

            case direction.forwards:
                transform.Translate(Vector3.forward * Time.deltaTime * velocity);
                break;

            case direction.backwards:
                transform.Translate(Vector3.back * Time.deltaTime * velocity);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if the enemy collides with another enemy or the wall, go in the opposite direction
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPace")
        {
            velocity = -velocity;
        }
    }
}
