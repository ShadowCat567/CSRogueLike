using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoom : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject exit;

    // Start is called before the first frame update
    void Start()
    {
        exit.GetComponent<BoxCollider>().isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(boss.GetComponent<FinalBoss>().isAlive == false)
        {
            exit.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
