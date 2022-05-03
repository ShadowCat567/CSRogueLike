using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgZone : MonoBehaviour
{
    [SerializeField] GameObject boss;

    // Update is called once per frame
    void Update()
    {
        //attaches the damage zone to the boss and makes sure they move together
        transform.position = new Vector3(boss.transform.position.x, boss.transform.position.y - 1.0f, boss.transform.position.z);
    }
}
