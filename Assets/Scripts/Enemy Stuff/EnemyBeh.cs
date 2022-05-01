using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeh : MonoBehaviour
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

   // float hitTimer = 0.2f;
    Renderer eneRend;
    Color hitColor = new Color(0.71f, 0.02f, 0.22f);
    Color baseColor = new Color(0.96f, 0.96f, 0.96f);
    [SerializeField] GameObject healthBar;

    enum direction { left, right, forwards, backwards }
    direction dirToMove;
    float velocity = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        eneRend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        curEneHealth = enemyHealth;
        isChasing = false;
        ChooseDirection();
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
            MoveInDirection();
        }

        if(curEneHealth <= 0)
        {
            Enemy.SetActive(false);
            curEneHealth = enemyHealth;
        }
    }


    void ChooseDirection()
    {
        dirToMove = (direction)Random.Range(0, 4);
    }

    void MoveInDirection()
    {
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
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPace")
        {
            velocity = -velocity;
        }
    }

    IEnumerator HitPlayer(float hitTimer)
    {
        eneRend.material.color = hitColor;
        yield return new WaitForSeconds(hitTimer);
        eneRend.material.color = baseColor;
    }
}
