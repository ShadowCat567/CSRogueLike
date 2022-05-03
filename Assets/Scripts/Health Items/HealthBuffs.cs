using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuffs : MonoBehaviour
{
    //value to increase player's maxHealth and currentHealth by
    public int buffValue = 5;
    [SerializeField] GameObject buffObject;
    //this value is used to tell whether the player can leave their current room
    public bool collectedBuff = false;

    // Start is called before the first frame update
    void Start()
    {
        buffObject.SetActive(false);   
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if the player runs into it, reactivate
        if (collision.gameObject.tag == "Player")
        {
            collectedBuff = true;
            buffObject.SetActive(false);
        }
    }
}
