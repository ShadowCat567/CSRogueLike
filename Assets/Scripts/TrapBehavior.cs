using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehavior : MonoBehaviour
{
    float activationDelay = 1.0f;
    float collisionTimer = 0.0f;
    bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //using this to help figure this out: https://forum.unity.com/threads/solved-detecting-a-constant-collision-over-x-amount-of-seconds.424701/

    // Update is called once per frame
    void Update()
    {
        if(isColliding == true)
        {
            activationDelay -= Time.deltaTime;
           // Debug.Log(activationDelay);

            if(activationDelay < 0)
            {
                activationDelay = 0;
            }
            Debug.Log("Player colliding");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPace" && isColliding == true)
        {
            if (activationDelay <= 0)
            {
                Debug.Log("player takes damage");
                if (collision.gameObject.tag == "Player")
                {
                    collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(2);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPace")
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPace")
        {
            isColliding = false;
        }
    }
}
