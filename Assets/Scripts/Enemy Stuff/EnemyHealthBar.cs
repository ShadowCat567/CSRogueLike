using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    Vector3 localscale;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        localscale = transform.localScale;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
    }
    
    public void UpdateEnemyHealth(float curHealth = 0.15f)
    {
        localscale.x = curHealth;
        transform.localScale = localscale;
    }
}
