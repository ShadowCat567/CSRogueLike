using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    //value to add to the player's current health
    public int healthValue = 3;
    [SerializeField] GameObject healthObject;

    private void OnCollisionEnter(Collision collision)
    {
        //deactivate when the player runs into it
        if (collision.gameObject.tag == "Player")
        {
            healthObject.SetActive(false);
        }
    }
}
