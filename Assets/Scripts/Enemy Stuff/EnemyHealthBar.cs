using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    //gets the scale of the health bar
    Vector3 localscale;
    //has access to the player
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //sets the local scale and the player
        localscale = transform.localScale;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //position healthBar towards player so it is visible to the player
        transform.LookAt(player.transform);
    }
    
    //updates the healthBar to match the enemy's current health
    public void UpdateEnemyHealth(float curHealth = 0.15f)
    {
        localscale.x = curHealth;
        transform.localScale = localscale;
    }
}
