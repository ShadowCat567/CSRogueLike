using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsInRoom : MonoBehaviour
{
    [SerializeField] GameObject room;
    [SerializeField] GameObject playerPos;
    GameObject player;
    bool wasActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(room.activeSelf == true && wasActivated == false)
        {
            ChangePosition();
        }
    }

    void ChangePosition()
    {
        player.transform.position = playerPos.transform.position;
        wasActivated = true;
    }
}
